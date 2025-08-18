using UnityEngine;

public class PinBall_Activate : PinBall
{
    public int WallHitCount { get; private set; } = 0; // 墙壁碰撞计数

    protected override void OnCollision()
    {
        base.OnCollision();

        // 检测是否碰撞的是墙壁层
        RaycastHit2D hit = Physics2D.Raycast(mBall.mTransform.position, mBall.mVelocity.normalized,
            mBall.mVelocity.magnitude * Time.deltaTime, mBall.mCollisionLayer);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            WallHitCount++;
            Debug.Log($"PinBall_Activate hit wall. Current count: {WallHitCount}");
        }
    }
}