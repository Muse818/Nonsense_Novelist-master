public class PinBall_Dead : PinBall
{
    public override void Update(float deltaSec)
    {
        base.Update(deltaSec);

        if (IsDead()) // 判断词条持有者是否死亡
        {
            TriggerDeadEffect();
        }
    }

    private void TriggerDeadEffect()
    {
    }

    private bool IsDead()
    {
        //检测死亡状态。
        return false;
    }
}