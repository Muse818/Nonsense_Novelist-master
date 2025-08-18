public class PinBall_Servants : PinBall
{
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