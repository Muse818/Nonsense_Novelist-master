
public class BattleClock : BattleModule
{
    protected float mElapsedTime = 0;
    protected int mFrame = 0;

    public float ElapsedSec => mElapsedTime;
    public int Frame => mFrame;

    public override void Update(float deltaSec)
    {
        mElapsedTime += deltaSec;
        ++mFrame;
    }
}