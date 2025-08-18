
using System.Collections.Generic;
using System.Linq;



public class AbilityTemplate : AbilityBase
{
    protected AbilityTrigger mTrigger = null;
    protected AbilityTargetSelector mSelector = null;
    protected List<AbilityEffectApplier> mAppliers = new List<AbilityEffectApplier>();

    public override bool IsUltra => mTrigger.CanTriggerByOther() && !AnimName.Equals("None");

    public AbilityTemplate(AbilityTrigger trigger, AbilityTargetSelector selector, List<AbilityEffectApplier> appliers)
    {
        mTrigger = trigger;
        mSelector = selector;
        mAppliers.AddRange(appliers);

        mTrigger.Bind(this);
        mSelector.Bind(this);

        foreach (var applier in mAppliers)
        {
            applier.Bind(this);
        }

        // mStackLimit = InitStackLimit(appliers);
    }

    public override void AddParams()
    {
        base.AddParams();
    }

    public void TriggeredBy(object triggerData)
    {
        List<BattleUnit> targets = null;

        if (mSelector != null)
            targets = mSelector.Pick(triggerData);

        if (targets is { Count: > 0 })
        {
            foreach (var applier in mAppliers)
            {
                applier.AddTask(targets, triggerData);
            }
        }
    }

    public override bool CanActivate()
    {
        if (!mTrigger.CanTriggerByOther())
            return false;
        
        return mTrigger.ShouldTrigger();
    }

    protected override void OnActivate()
    {
        if (!IsUltra)
        {
            mTrigger.TriggerDirect();
            
            TryDeactivate();
        }
    }

    public override BattleUnit PickTarget()
    {
        if (!mTrigger.CanTriggerByOther())
            return null;
        
        var tgts = mSelector.Pick(null);
        return tgts.Count > 0 ? tgts[0] : null;
    }

    public override void OnAnimTrigger()
    {
        if (!mTrigger.CanTriggerByOther())
            return;
        
        mTrigger.TriggerDirect();
    }

    protected override void Tick(float deltaTime)
    {
        mTrigger?.Update(deltaTime);
    }

    protected override void TickEvenWhenDead(float deltaTime)
    {
        for (int i = 0; i < mAppliers.Count; ++i)
        {
            mAppliers[i].Update(deltaTime);
        }
    }
    
    #region TriggerEvents

    protected override void OnInit()
    {
        mTrigger.OnInit();

        foreach (var applier in mAppliers)
        {
            applier.OnInit();
        }
    }

    public override void OnPreDealDamageCalc(DealDamageCalc dmgCalc)
    {
        mTrigger.OnPreDealDamageCalc(dmgCalc);
    }

    public override void OnPreDealDamageCalcOtherAbility(DealDamageCalc dmgCalc)
    {
        mTrigger.OnPreDealDamageCalcOtherAbility(dmgCalc);
    }

    public override void OnPreDealDamage(DamageReport report)
    {
        mTrigger.OnPreDealDamage(report);
    }

    public override void OnPreDealDamageOtherAbility(DamageReport report)
    {
        mTrigger.OnPreDealDamageOtherAbility(report);
    }

    public override void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        mTrigger.OnPreTakeDamageCalc(dmgCalc);
    }

    public override void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        mTrigger.OnAllyPreTakeDamageCalc(dmgCalc);
    }

    public override void OnPreTakeDamage(DamageReport report)
    {
        mTrigger.OnPreTakeDamage(report);
    }

    public override void OnPostDealDamage(DamageReport dmg)
    {
        mTrigger.OnPostDealDamage(dmg);
    }

    public override void OnPostDealDamageOtherAbility(DamageReport dmg)
    {
        mTrigger.OnPostDealDamageOtherAbility(dmg);
    }

    public override void OnPostTakeDamage(DamageReport dmg)
    {
        mTrigger.OnPostTakeDamage(dmg);
    }

    public override void OnAllyDealDamage(DamageReport dmg)
    {
        mTrigger.OnAllyDealDamage(dmg);
    }

    public override void OnPawnDeath(BattleUnit deceased, DamageReport dmg)
    {
        mTrigger.OnPawnDeath(deceased, dmg);
    }

    public override void OnSelfDeath(DamageReport dmg)
    {
        mTrigger.OnSelfDeath(dmg);
    }

    #endregion

    #region EffectProcess
    
    public override void OnSelfApplyEffect(BattleEffect be)
    {
        mTrigger.OnSelfApplyEffect(be);
    }
    public override void OnSelfApplyHealEffect(BattleEffect be)
    {
        mTrigger.OnSelfApplyHealEffect(be);
    }
    #endregion
}

public class AbilityModule
{
    protected AbilityTemplate mOwner = null;

    protected int mParseIndex = 0;
    
    protected List<Formula> mParams = new List<Formula>();
    protected Dictionary<string, CustomParam> mCustomParams = null;

    public void Bind(AbilityTemplate owner)
    {
        mOwner = owner;
    }

    public virtual void AddParams() {}
    
    public bool ParseParams()
    {
        AddParams();

        foreach (var param in mParams)
        {
            if (!ReadParam(param))
            {
                return false;
            }
        }

        return true;
    }
    
    protected bool ReadParam(Formula param)
    {
        if (mCustomParams.TryGetValue(param.mKey, out var data))
        {
            param.mValues = data.mValues;
            return true;
        }
        
        return false;
    }
    
    public virtual void OnInit() {}

    public virtual void Update(float deltaTime)
    {
        Tick(deltaTime);
    }

    protected virtual void Tick(float deltaTime) {}
}