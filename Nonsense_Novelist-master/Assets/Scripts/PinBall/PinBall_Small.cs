using UnityEngine;

public class PinBall_Small : PinBall
{
    public PinBall_Small(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    
    protected override void OnCollision()
    {
        // 不做任何特殊处理，继承默认反弹逻辑
    }
}
