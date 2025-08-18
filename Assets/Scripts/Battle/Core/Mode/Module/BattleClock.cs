
public class BattleClock : BattleModule
{
    protected float mElapsedTime = 0;
    protected int mFrame = 0;

    public float ElapsedSec => mElapsedTime;
    public int Frame => mFrame;

    protected TimerManager mTimerManager = new TimerManager();
    public TimerManager TimerManager => mTimerManager;

    public override void Init()
    {
        mTimerManager.Init();
    }

    public override void Update(float deltaSec)
    {
        mElapsedTime += deltaSec;
        ++mFrame;
        
        mTimerManager.Update(deltaSec);
    }
}