using System.Collections.Generic;
using UnityEngine;

public class PinBall_Alpha : PinBall
{
    public float LifeTime = 5f; // 存活时间（秒）
    private float mElapsedTime = 0f;
    private List<Collider2D> mPenetratedObjects = new List<Collider2D>(); // 记录穿透的物体


   public PinBall_Alpha(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    
    public override void Start()
    {
        base.Start();
        mElapsedTime = 0f;
    }

    public override void Update(float deltaSec)
    {
        base.Update(deltaSec);

        mElapsedTime += deltaSec;
        if (mElapsedTime >= LifeTime)
        {
            MarkPendingKill(); 
        }
    }

    protected override void OnCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(mBall.transform.position, mBall.velocity.normalized, 
            mBall.velocity.magnitude * Time.deltaTime, mBall.collisionLayer);

        if (hit.collider != null && !mPenetratedObjects.Contains(hit.collider))
        {
            mPenetratedObjects.Add(hit.collider);
        }


    }
}
