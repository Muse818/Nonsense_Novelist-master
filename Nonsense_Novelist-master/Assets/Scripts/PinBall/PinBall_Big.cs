using UnityEngine;

public class PinBall_Big : PinBall
{
    public PinBall_Big(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    
    protected override void OnCollision()
    {
        // 不做任何特殊处理，继承默认反弹逻辑
    }
}
