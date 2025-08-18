using UnityEngine;

public class PinBall_Spread : PinBall
{
    public PinBall_Spread(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    
    protected override void OnCollision()
    {
        // 不做任何特殊处理，继承默认反弹逻辑
    }
}
