using System;
using UnityEngine;
using Object = System.Object;


public class NnProjectile : MonoBehaviour
{
    public enum State
    {
        None = 0,
        Route,
        Hit,
        Expired,
    }

    protected State mState = State.None;
    
    protected EmitMeta mMeta;
    protected EmitSO mAsset;
    
    
    protected Vector3 mEmitPos;
    protected Vector3 mTargetPos;
    protected float mTimer = 0;
    protected float mHitTime = 0;
    protected float mExpiredTime = 0;
    protected bool mExpired = false;

    public void Setup(EmitMeta meta, EmitSO asset)
    {
        mMeta = meta;
        mAsset = asset;
        
        var projRoot = GameObject.Find("ProjectileRoot");
        if (projRoot == null)
        {
            projRoot = new GameObject("ProjectileRoot");
        }

        transform.SetParent(projRoot.transform);
    }
    
    public void Emit()
    {
        if (mMeta.mData.mType == EmitType.EnemyToSelf)
        {
            mEmitPos = mMeta.mTarget.ViewPos;
            mTargetPos = mMeta.mInstigator.ViewPos;
        }
        else
        {
            mEmitPos = mMeta.mInstigator.UnitView.ModelLayout.GetWeaponPos();
            mTargetPos = mMeta.mTarget.ViewPos;
        }
        
        Vector3 offset = mTargetPos - mEmitPos;
        var transform1 = transform;
        transform1.position = mEmitPos;
        transform1.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(offset.y, offset.x));
        
        if (mAsset.emitterFx != null)
        {
            Instantiate(mAsset.emitterFx, transform1.position, transform1.rotation);
        }

        mHitTime = offset.magnitude / mMeta.mData.mSpeed;
        
        mExpiredTime = mHitTime + 1;
        
        if (mMeta.mData.mType == EmitType.EnemyToSelf)
        {
            mMeta.OnHitTarget();
        }
        
        mState = State.Route;
    }

    private void Update()
    {
        mTimer += Time.deltaTime;
        
        switch (mState)
        {
            case State.Route:
            {
                float percent = mTimer / mHitTime;
                transform.position = (1 - percent) * mEmitPos + percent * mTargetPos;

                if (percent >= 1.0f)
                {
                    mState = State.Hit;
                    OnEnterHit();
                }
                
                break;
            }

            default:
                break;
        }

        if (mTimer > mExpiredTime)
        {
            mState = State.Expired;
            Expired();
        }
    }

    protected void OnEnterHit()
    {
        if (mMeta.mTarget != null)
        {
            if (mMeta.mData.mType == EmitType.SelfToEnemy)
            {
                mMeta.OnHitTarget();
            }
            
            // play hit effect
            if (mAsset.arriverFx != null)
            {
                Instantiate(mAsset.arriverFx, mTargetPos, Quaternion.Euler(0, 0, 0));
            }

            Expired();
        }
    }
    
    protected void Expired()
    {
        if (mExpired)
            return;
        
        mExpired = true;
        Destroy(gameObject);
    }
}