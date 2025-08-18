using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    None = 0,
    Split = 1,            // 分裂
    Activate = 2,         // 激活
    spread = 3,           // 传播
    Alpha = 4,            // 穿透
    Start = 5,            // 起兴
    Servants = 6,         // 连及
    Dead = 7,             // 歇后
    Add = 8,              // 递进
    Small = 9,            // 委婉
    Big = 10,             // 直白
    Mirror = 11,          // 对仗
    Expect = 12,          // 衬托
    Copy = 13,            // 比喻
    SameChara = 14,       // 顶针
    ReTrigger = 15,       // 回环
}

public enum HitTargetType
{
    None = 0,
    
}

public class PreLineInfo
{
    public List<Vector3> colPos = new List<Vector3>();

}

public abstract class PinBall : BattleObject
{
    public class Ball
    {
        public WordTable.Data mWordData = null;
        public Transform mTransform = null; //小球本体
        public float mShootAngle = 0; //小球发射角度
        public Vector2 mVelocity = Vector2.zero; //小球实际速度
        
        public LayerMask mCollisionLayer = LayerMask.GetMask("wall", "WordCollision", "Character");
        public float mRadius = 0.5f;
        public float mFriction = 0.1f;
        public float mEnergyLoss = 0.2f;
    }

    public static int mWallLayer = LayerMask.NameToLayer("wall");
    public static int mWordLayer = LayerMask.NameToLayer("WordCollision");
    public static int mCharacterLayer = LayerMask.NameToLayer("Character");

    public Ball mBall;
    public PreLineInfo mSimulateInfo = new PreLineInfo();

    protected bool mSimulate = false;

    public void ShootOut()
    {
        mSimulate = false;
        IsTickEnable = true;
        SetSizeToRadius();
    }
    
    public override void LateFixedUpdate(float deltaSec)
    {
        TickMovement(deltaSec);
    }
    
    public virtual void TickSimulation(int times, float deltaSec)
    {
        mSimulate = true;
        
        var cachePos = mBall.mTransform.position;
        var cacheRotation = mBall.mTransform.rotation;
        var cacheVelocity = mBall.mVelocity;
        
        mSimulateInfo.colPos.Clear();
        for (int i = 0; i < times; i++)
        {
            TickMovement(deltaSec);
        }
        
        mSimulateInfo.colPos.Add(mBall.mTransform.position);
        
        mBall.mTransform.position = cachePos;
        mBall.mTransform.rotation = cacheRotation;
        mBall.mVelocity = cacheVelocity;
    }

    public void SetSizeToRadius()
    {
        if (mBall.mTransform == null) 
            return;
        
        SpriteRenderer ballSprite;
        mBall.mTransform.TryGetComponent<SpriteRenderer>(out ballSprite);
        if (ballSprite == null) 
            return;
        
        Vector2 spriteSize = ballSprite.sprite.bounds.size;
        float scale = (mBall.mRadius * 2) / Mathf.Max(spriteSize.x, spriteSize.y);
        mBall.mTransform.localScale = new Vector3(scale, scale, 1f);
    }

    public void TickMovement(float deltaSec)
    {
        ApplyFriction(deltaSec);
        mBall.mTransform.position += (Vector3)mBall.mVelocity * deltaSec;

        RaycastHit2D hit = Physics2D.CircleCast(
            mBall.mTransform.position,
            mBall.mRadius,
            mBall.mVelocity.normalized,
            mBall.mVelocity.magnitude * deltaSec,
            mBall.mCollisionLayer
        );

        if (hit.collider != null)
        {
            HandleCollision(hit);
        }
    }

    private void ApplyFriction(float deltaSec)
    {
        float frictionLoss = mBall.mFriction * mBall.mVelocity.magnitude * deltaSec;
        mBall.mVelocity *= Mathf.Max(1f - frictionLoss / mBall.mVelocity.magnitude, 0f);
    }

    protected virtual void HandleCollision(RaycastHit2D hit)
    {
        int layer = hit.collider.gameObject.layer;
        if (mSimulate)
        {
            if (layer == mWallLayer)
            {
                OnPreHitWall(hit);
            }
        }
        else
        {
            if (layer == mWordLayer)
            {
                OnHitWord(hit);
            }
            else if (layer == mCharacterLayer)
            {
                OnHitUnit(hit);
            }
            else if (layer == mWallLayer)
            {
                OnHitWall(hit);
            }
        }
    }

    protected virtual void OnPreHitWall(RaycastHit2D hit)
    {
        var wall = Battle.ObjectManager.Find<WallObject>(hit.collider);
        if (wall == null)
            return;
        
        mBall.mTransform.position =
            wall.ApplyBounceEffectToPos(mBall.mRadius, hit.point, (Vector2)mBall.mTransform.position);
        mBall.mVelocity = wall.ApplyBounceEffectToVel(ref mBall.mVelocity, hit.normal);
        
        mSimulateInfo.colPos.Add(hit.point);
        OnPreCollision();
    }

    protected virtual void OnHitWall(RaycastHit2D hit)
    {
        var wall = Battle.ObjectManager.Find<WallObject>(hit.collider);
        if (wall == null)
            return;

        mBall.mTransform.position =
            wall.ApplyBounceEffectToPos(mBall.mRadius, hit.point, (Vector2)mBall.mTransform.position);
        mBall.mVelocity = wall.ApplyBounceEffectToVel(ref mBall.mVelocity, hit.normal);
        
        OnCollision();
    }

    protected virtual void OnHitWord(RaycastHit2D hit)
    {
        
    }
    
    protected virtual void OnHitUnit(RaycastHit2D hit)
    {
        var role = hit.collider.transform.parent.GetComponentInChildren<UnitViewBase>().Role;
        if (!role.IsValid())
            return;

        role.WordComponent.AddWord(mBall.mWordData.mKind);
        Expired();
    }

    protected virtual void OnPreCollision()
    {

    }


    protected virtual void OnCollision()
    {
        PlayEffect(mBall.mTransform.position);
    }

    private void PlayEffect(Vector2 position)
    {
    }

    protected virtual void Expired()
    {
        if (mBall == null)
            return;
        
        MarkPendingKill();

        if (mBall.mTransform != null)
        {
            Object.Destroy(mBall.mTransform.gameObject);
        }
        
        mBall.mTransform = null;
    }
}
