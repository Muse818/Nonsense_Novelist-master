
using UnityEngine;

public enum WallcolType
{
    None = 0,
    Bounce=1,
}


public class WallPhysics
{
    public float energyLoss;// 法线方向:动能损耗
    public float friction;// 切线方向:摩擦损耗
        
}
public abstract class WallObject : BattleObject
{
    public class Wall
    {
        public Collider2D collider;
        public Transform transform;
        public WallcolType wallType;
        public WallPhysics wallPhysics;
    }
    public Wall mWall;
    public WallObject()
    {
        mWall=new Wall();
        mWall.wallType=WallcolType.None;

        mWall.wallPhysics=new WallPhysics();
        mWall.wallPhysics.energyLoss=0.5f;
        mWall.wallPhysics.friction=0.02f;
    }

    public virtual Vector2 ApplyBounceEffectToPos(float radius,Vector2 hitPoint,Vector2 pos)
    {
        Vector2 penetrationDepth = (hitPoint - pos).normalized * radius;
        pos = hitPoint - penetrationDepth;
        return pos;
    }
    public virtual Vector2 ApplyBounceEffectToVel(ref Vector2 velocity, Vector2 normal)
    {
    
        Vector2 normalVelocity = Vector2.Dot(velocity, normal) * normal;
        Vector2 tangentVelocity = velocity - normalVelocity;

        normalVelocity *= (1f - mWall.wallPhysics.energyLoss); // 法线方向:动能损耗
        tangentVelocity *= (1f - mWall.wallPhysics.friction); // 切线方向:摩擦损耗

        return (-normalVelocity + tangentVelocity);
    }
  
    public override void Start()
    {
        base.Start();

    }
}


public class BounceWall:WallObject
{
     public BounceWall():base()
    {
        mWall.wallType=WallcolType.Bounce;
        mWall.wallPhysics.energyLoss=0.5f;
        mWall.wallPhysics.friction=0.02f;
    }
}
public class NormarWall:WallObject
{
     public NormarWall():base()
    {
        mWall.wallType=WallcolType.Bounce;
        mWall.wallPhysics.energyLoss=0.5f;
        mWall.wallPhysics.friction=0.02f;
    }
}