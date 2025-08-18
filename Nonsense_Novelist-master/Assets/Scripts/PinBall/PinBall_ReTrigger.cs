using UnityEngine;
public class PinBall_ReTrigger : PinBall
{
    private float retriggerDuration = 5f;
    private float retriggerEndTime;
    public PinBall_ReTrigger(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    protected override void OnCollision()
    {
        retriggerEndTime = Time.time + retriggerDuration;
        base.OnCollision();
    }

    public override void Update(float deltaSec)
    {
        base.Update(deltaSec);

        if (Time.time <= retriggerEndTime)
        {
            // 回环效果：再次生效。
            Debug.Log("PinBall_ReTrigger effect triggered!");
        }
    }
}