using UnityEngine;

public class PinBall_none : PinBall
{
    public PinBall_none(WordTable.Data data)
    {
        mBall=new Ball();
        mPreInfo=new PreLineInfo();
        mBall.wordData=data;
        
    }
    
    protected override void OnCollision()
    {
        // 不做任何特殊处理，继承默认反弹逻辑
    }
}
