
public class BattleObject : CoreEntity
{
    protected int mID = 0;
    public int ID
    {
        set => mID = value;
        get => mID;
    }

    protected bool mRegistered = false;
    public bool IsRegistered
    {
        set => mRegistered = value;
        get => mRegistered;
    }

    protected bool mTickEnable = true;

    public BattleBase Battle { get; set; }

    public bool IsTickEnable
    {
        set => mTickEnable = value;
        get => mTickEnable && !mPendingKill;
    }
    
    protected bool mPendingKill = false;
    
    public bool IsPendingKill => mPendingKill;

    public void MarkPendingKill()
    {
        mPendingKill = true;
    }

    public virtual void Start() {}
    public virtual void Update(float deltaSec) {}
    public virtual void LateUpdate(float deltaSec) {}
    public virtual void LateFixedUpdate(float deltaSec) {}
    public virtual void OnEnterCombatPhase() {}
    public virtual void OnExitCombatPhase() {}
    public virtual void OnEnterRestPhase() {}
    public virtual void OnExitRestPhase() {}
}