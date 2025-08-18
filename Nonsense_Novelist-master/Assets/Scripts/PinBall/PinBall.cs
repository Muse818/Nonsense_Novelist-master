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

public abstract class PinBall : BattleObject
{
    public class Ball
    {
        public Transform transform; //小球本体
        public Transform preTransform; //预测小球位置
        public Vector2 velocity; //小球实际速度
        public Vector2 preVelocity; //预测小球速度
        public LayerMask collisionLayer = LayerMask.GetMask("wall", "WordCollision", "Character");
        public WordTable.Data wordData;
        public float radius = 0.5f;
        public float friction = 0.1f;
        public float energyLoss = 0.2f;
        public bool hasShoot = false;

    }

    public static int mWallLayer = LayerMask.NameToLayer("wall");
    public static int mWordLayer = LayerMask.NameToLayer("WordCollision");
    public static int mCharacterLayer = LayerMask.NameToLayer("Character");


    public class PreLineInfo
    {
        public List<Vector3> colPos = new List<Vector3>();

    }

    public Ball mBall;
    public PreLineInfo mPreInfo;
    private int preMoveTimes = 30;

    public void ShootOut()
    {
        SyncPreAndReal();
        mBall.hasShoot = true;
        SetSizeToRadius();
    }

    public void SyncPreAndReal()
    {
        if (mBall.hasShoot)
        {
            mBall.transform.position = mBall.preTransform.position;
            mBall.transform.rotation = mBall.preTransform.rotation;
            mBall.velocity = mBall.preVelocity;
        }
        else
        {
            if (mBall.preTransform == null)
            {
                var obk = new GameObject();
                mBall.preTransform = obk.transform;
            }

            mBall.preTransform.position = mBall.transform.position;
            mBall.preTransform.rotation = mBall.transform.rotation;
            mBall.preVelocity = mBall.velocity;
        }
    }



    public override void LateFixedUpdate(float deltaSec)
    {
        if (!IsTickEnable) return;

        SyncPreAndReal();
        if (mBall.hasShoot)
        {
            PreUpdate(deltaSec);
        }
        else
        {
            mPreInfo.colPos.Clear();
            for (int i = 0; i < preMoveTimes; i++)
            {
                PreUpdate(deltaSec);
            }

        }

    }

    public void SetSizeToRadius()
    {
        if (mBall.transform == null) 
            return;
        
        SpriteRenderer ballSprite;
        mBall.transform.TryGetComponent<SpriteRenderer>(out ballSprite);
        if (ballSprite == null) return;
        Vector2 spriteSize = ballSprite.sprite.bounds.size;
        float scale = (mBall.radius * 2) / Mathf.Max(spriteSize.x, spriteSize.y);
        mBall.transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void PreUpdate(float deltaSec)
    {
        ApplyFriction(deltaSec);
        mBall.preTransform.position += (Vector3)mBall.preVelocity * deltaSec;

        RaycastHit2D hit = Physics2D.CircleCast(
            mBall.preTransform.position,
            mBall.radius,
            mBall.preVelocity.normalized,
            mBall.preVelocity.magnitude * deltaSec,
            mBall.collisionLayer
        );

        if (hit.collider != null)
        {
            HandleCollision(hit);
        }
    }

    private void ApplyFriction(float deltaSec)
    {
        float frictionLoss = mBall.friction * mBall.preVelocity.magnitude * deltaSec;
        mBall.preVelocity *= Mathf.Max(1f - frictionLoss / mBall.preVelocity.magnitude, 0f);
    }

    protected virtual void HandleCollision(RaycastHit2D hit)
    {
        Debug.Log(hit.collider.gameObject.name);

        int layer = hit.collider.gameObject.layer;

        if (mBall.hasShoot)
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
        else
        {
            if (layer == mWallLayer)
            {
                OnPreHitWall(hit);
            }
        }
    }

    protected virtual void OnPreHitWall(RaycastHit2D hit)
    {
        var wall = Battle.ObjectManager.Find<WallObject>(hit.collider);
        if (wall == null)
            return;
        
        mBall.preTransform.position =
            wall.ApplyBounceEffectToPos(mBall.radius, hit.point, (Vector2)mBall.preTransform.position);
        mBall.preVelocity = wall.ApplyBounceEffectToVel(ref mBall.preVelocity, hit.normal);
        
        mPreInfo.colPos.Add(hit.point);
        OnPreCollision();
    }

    protected virtual void OnHitWall(RaycastHit2D hit)
    {
        var wall = Battle.ObjectManager.Find<WallObject>(hit.collider);
        if (wall == null)
            return;

        mBall.preTransform.position =
            wall.ApplyBounceEffectToPos(mBall.radius, hit.point, (Vector2)mBall.preTransform.position);
        mBall.preVelocity = wall.ApplyBounceEffectToVel(ref mBall.preVelocity, hit.normal);
        
        OnCollision();
    }

    protected virtual void OnHitWord(RaycastHit2D hit)
    {
        
    }
    
    protected virtual void OnHitUnit(RaycastHit2D hit)
    {
        var role = hit.collider.GetComponentInChildren<UnitViewBase>().Role;
        if (!role.IsValid())
            return;

        role.WordComponent.AddWord(mBall.wordData.mKind);
        Expired();
    }

    protected virtual void OnPreCollision()
    {

    }


    protected virtual void OnCollision()
    {
        PlayEffect(mBall.transform.position);
    }

    private void PlayEffect(Vector2 position)
    {
    }

    protected virtual void Expired()
    {
        if (mBall == null)
            return;
        
        MarkPendingKill();

        if (mBall.transform != null)
        {
            Object.Destroy(mBall.transform.gameObject);
        }
        mBall.transform = null;
    }
}
