public class PinBall_Servants : PinBall
{
    public PinBall_Servants(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    protected override void OnCollision()
    {
        // 连及效果：同时生效于角色和随从。
        ApplyEffectToServants();
        base.OnCollision();
    }

    private void ApplyEffectToServants()
    {
    
    }
}