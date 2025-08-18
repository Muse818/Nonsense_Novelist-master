

using System.Collections.Generic;
using UnityEngine;

public class AbilityEffectApplier : AbilityModule
{
    public enum Type
    {
        None = 0,
        Test = 1,
        AttrMod = 2,
        Damage = 3,
        AttrDamage = 4,
        Reincarnation=5,
        AttrHeal=6,
    }

    public static AbilityEffectApplier Create(Type type)
    {
        AbilityEffectApplier applier = null;

        switch (type)
        {
            case Type.Test:
                applier = new AMEATest();
                break;
            
            case Type.AttrMod:
                applier = new AMEAAttrMod();
                break;
            
            case Type.Damage:
                applier = new AMEADamage();
                break;
            
            case Type.AttrDamage:
                applier = new AMEAAttrDamage();
                break;

            case Type.Reincarnation:
                applier = new AMEAReincarnation();
                break;

            case Type.AttrHeal:
                applier = new AMEAAttrHeal();
                break;

            default:
                break;
        }

        if (applier != null)
            applier.mType = type;

        return applier;
    }
    
    struct ScheduleData
    {
        public List<BattleUnit> mTargets;
        public object mTriggerData;

        public ScheduleData(List<BattleUnit> targets, object triggerData)
        {
            mTargets = targets;
            mTriggerData = triggerData;
        }
    }
    
    List<ScheduleData> mScheduleList = new List<ScheduleData>();
    const int MaxSchedulePerFrame = 64;

    public Type mType = Type.None;
    
    public AbilityEffectApplierTable.Data mData = null;
    
    protected bool mCanBePurgedOrExpelled = true;

    protected bool mHasDuration = false;

    protected float mDuration = 0;

    protected int mStackLimit = 0;
    public int StackLimit => mStackLimit;
    
    protected virtual bool DelayApply => true;

    public override void OnInit() { }
    
    public virtual bool Setup(AbilityEffectApplierTable.Data data)
    {
        mData = data;
        mCustomParams = data.mCustomParams;
        
        mCanBePurgedOrExpelled = mData.mCanBePurgedOrExpelled;
        mStackLimit = mData.mStackLimit;
        mDuration = mData.mDuration;
        mHasDuration = mDuration > 0;
        
        return ParseParams();
    }

    public void AddTask(List<BattleUnit> targets, object triggerData)
    {
        if(DelayApply)
        {
            if(mScheduleList.Count < MaxSchedulePerFrame)
            {
                mScheduleList.Add(new ScheduleData(targets, triggerData));
            }
            else
            {
                Debug.Log($"Ability kind {mOwner.Data.mKind} with applier {mType} has reached schedule limit.");
            }
        }
        else
        {
            Apply(targets, triggerData);
        }
    }

    public virtual void Apply(List<BattleUnit> targets, object triggerData)
    {
        if (mOwner.Data.mProjKind > 0)
        {
            foreach (var tgt in targets)
            {
                EmitMeta meta = new EmitMeta();
                meta.mProjKind = mOwner.Data.mProjKind;
                meta.mInstigator = mOwner.Unit;
                meta.mTarget = tgt;
                meta.mAbility = mOwner;
                meta.mHitCallBack = (tgt1) => Apply(tgt1, triggerData);

                BattleObjectFactory.StartEmit(meta);
            }
        }
        else
        {
            foreach (var tgt in targets)
            {
                Apply(tgt, triggerData);
            }
        }
    }
    
    public virtual void Apply(BattleUnit target, object triggerData) { }

    public override void Update(float deltaTime)
    {
        if(mScheduleList.Count > 0)
        {
            for(int i = 0; i < mScheduleList.Count; ++i)
            {
                Apply(mScheduleList[i].mTargets, mScheduleList[i].mTriggerData);
            }
            
            mScheduleList.Clear();
        }
        
        Tick(deltaTime);
    }
}

public class AMEATest : AbilityEffectApplier
{
    public override void Apply(BattleUnit target, object triggerData)
    {
        Debug.Log($"Apply to target {target.Data.mName}");
    }
}

public class AMEAAttrMod : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    protected Formula mModifyPercent = new Formula("is_percent");

    public override void AddParams()
    {
        mParams.Add(mAttrType);
        mParams.Add(mValue);
        mParams.Add(mModifyPercent);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        BattleEffectSpec spec = new BattleEffectSpec();
        spec.mType = BattleHelper.ToEffectType((AttributeType)mAttrType.EvaluateInt(mOwner), mValue.Evaluate(mOwner) > 0);
        spec.mInstigator = mOwner.Unit;
        spec.mTarget = target;
        spec.mInputValueBool = mModifyPercent.EvaluateBool(mOwner);
        spec.mInputValue = mValue.Evaluate(mOwner);
        spec.mDuration = mDuration;
        spec.mDurationRule = mHasDuration ? EffectDurationRule.HasDuration : EffectDurationRule.Script;
        spec.mStackRule = EffectStackRule.Target;
        spec.mStackDurationRule = EffectStackDurationRule.Refresh;
        spec.mAbility = mOwner;
        spec.mMaxStackCount = mStackLimit;

        EffectAgent.ApplyEffectToTarget(target, spec);
    }
}

public class AMEADamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageValue = new Formula("damage_value");
    
    public override void AddParams()
    {
        mParams.Add(mDamageType);
        mParams.Add(mDamageTimes);
        mParams.Add(mDamageValue);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
        dmg.mTarget = target;
        dmg.mAbility = mOwner;
        dmg.mMinAttack = mDamageValue.Evaluate(mOwner);
        dmg.mMaxAttack = dmg.mMinAttack;

        switch ((DamageType)mDamageType.EvaluateInt(mOwner))
        {
            case DamageType.Fix:
                dmg.mFlag |= DealDamageFlag.Fixed;
                break;

            case DamageType.Magic:
                dmg.mMagic = true;
                break;

            case DamageType.Psy:
                dmg.mMagic = false;
                break;
        }

        for (int i = 0; i < mDamageTimes.EvaluateInt(mOwner); i++)
        {
            DamageHelper.ProcessDamage(dmg);
        }
    }
}

public class AMEAAttrDamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageRatio = new Formula("damage_ratio");
    protected Formula mAttrType = new Formula("attr_type");
    
    public override void AddParams()
    {
        mParams.Add(mDamageType);
        mParams.Add(mDamageTimes);
        mParams.Add(mDamageRatio);
        mParams.Add(mAttrType);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
        dmg.mDamageSource = DamageSource.Ability;
        dmg.mTarget = target;
        dmg.mAbility = mOwner;
        dmg.mMinAttack = mOwner.Unit.GetAttributeValue((AttributeType)mAttrType.EvaluateInt(mOwner)) * mDamageRatio.Evaluate(mOwner);
        dmg.mMaxAttack = dmg.mMinAttack;

        switch ((DamageType)mDamageType.EvaluateInt(mOwner))
        {
            case DamageType.Fix:
                dmg.mFlag |= DealDamageFlag.Fixed;
                break;

            case DamageType.Magic:
                dmg.mMagic = true;
                break;

            case DamageType.Psy:
                dmg.mMagic = false;
                break;
        }

        for (int i = 0; i < mDamageTimes.EvaluateInt(mOwner); i++)
        {
            DamageHelper.ProcessDamage(dmg);
        }
    }
}
public class AMEAReincarnation : AbilityEffectApplier
{
    public override void Apply(BattleUnit target, object triggerData)
    {
        target.ServantOwner.ServantsAgent.RegisterServants(target.ID);
        target.ModifyBase(AttributeType.MaxHp, -0.7f, true);
    }
}
public class AMEAAttrHeal : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    protected Formula mModifyPercent = new Formula("is_percent");
    protected float mCurrentHeal=0;

    public override void AddParams()
    {
        mParams.Add(mAttrType);
        mParams.Add(mValue);
        mParams.Add(mModifyPercent);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        if(mModifyPercent.EvaluateBool(mOwner))
        {
            mCurrentHeal=mOwner.Unit.GetAttributeValue((AttributeType)mAttrType.EvaluateInt(mOwner))*mValue.Evaluate(mOwner);
        }
        else
        {
            mCurrentHeal=mValue.Evaluate(mOwner);
        }
        target.ApplyHeal(mCurrentHeal);
    }
}