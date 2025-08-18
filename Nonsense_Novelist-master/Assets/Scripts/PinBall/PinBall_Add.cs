using UnityEngine;

public class PinBall_Add : PinBall
{
    private int effectLevel = 1;
    public PinBall_Add(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    protected override void OnHitWall(RaycastHit2D hit)
    {
        // 递进效果：每次碰撞增强一次。
        effectLevel++;

        base.OnHitWall(hit);
    }
}