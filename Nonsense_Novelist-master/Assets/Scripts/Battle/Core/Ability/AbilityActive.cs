
public class AbilityActive : AbilityBase
{
    protected float mLastTriggerTime = 0f;
    
    public bool CoolDown => ElapsedSec - mLastTriggerTime >= CD;

    protected override void OnInit()
    {
        mLastTriggerTime = -CD;
    }

    public override bool CanActivate()
    {
        if (!CoolDown)
            return false;
        
        return true;
    }

    protected override void OnActivate()
    {
        mLastTriggerTime = ElapsedSec;
    }
}