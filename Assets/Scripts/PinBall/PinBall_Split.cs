using UnityEngine;

public class PinBall_Split : PinBall
{
    public static int SplitCount = 3;
    public static float SplitAngleOffset = 15f;
    
    protected override void OnCollision()
    {
        SplitBall();
    }

    private void SplitBall()
    {
        if (SplitCount < 2) return;

        float totalOffset = 0;
        float baseAngle = Mathf.Atan2(mBall.mVelocity.y, mBall.mVelocity.x) * Mathf.Rad2Deg;

        for (int i = 0; i < SplitCount; i++)
        {
            float offset = CalculateSplitAngle(i, SplitCount);
            totalOffset += offset;

            float newAngle = baseAngle + offset;
            Vector2 newDirection = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

            CreateSplitBall(newDirection);
        }

    }

    private float CalculateSplitAngle(int index, int total)
    {
        return (index - (total - 1) / 2f) * SplitAngleOffset;
    }

    private void CreateSplitBall(Vector2 direction)
    {
        
    }
}
