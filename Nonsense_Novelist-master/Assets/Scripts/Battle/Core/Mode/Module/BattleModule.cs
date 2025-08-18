
public class BattleModule : CoreEntity
{
    private BattleBase _mBattle;
    public BattleBase Battle
    {
        get => _mBattle;
        set => _mBattle = value;
    }

    public virtual void Init() {}
    
    public virtual void Begin() {}
    
    public virtual void PostBegin() {}
    
    public virtual void Update(float deltaSec) {}
    
    public virtual void LateUpdate(float deltaSec) {}

    public virtual void LateFixedUpdate(float deltaSec) {}
    
    public virtual void OnEnterCombatPhase() {}
    public virtual void OnExitCombatPhase() {}
    
    public virtual void OnEnterResetPhase() {}
    public virtual void OnExitRestPhase() {}
}