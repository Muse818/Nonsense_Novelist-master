


using UnityEngine;

public enum EffectType
{
    None = 0,
    Stun,
    Damage,
    
    AttackUp,
    AttackDown,
    DefUp,
    DefDown,
    SanUp,
    SanDown,
    PsyUp,
    PsyDown,
    MaxHpUp,
    MaxHpDown,
    Heal,
}
    
public enum EffectDurationRule
{
    Instant = 0,
    HasDuration,
    Script,
}
    
public enum EffectStackRule
{
    None = 0,
    Source,
    Target,
}

public enum EffectStackDurationRule
{
    None = 0,
    Refresh,
}

public class BattleEffectSpec
{
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    
    public EffectType mType;
    public EffectDurationRule mDurationRule;
    public EffectStackRule mStackRule;
    public EffectStackDurationRule mStackDurationRule;

    public bool mInputValueBool;
    public float mInputValue;
    public int mInputValueInt;
    
    public int mStackCount = 1;
    public int mMaxStackCount;
    public float mDuration;
    
    public bool mCanBePurged = true;
    public bool mMergeInputValue = false;
    public bool mIsRemoveOnCombatEnd = true;
}

public class BattleEffect : CoreEntity
{
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    
    public EffectType mType;
    public EffectDurationRule mDurationRule;
    public EffectStackRule mStackRule;
    public EffectStackDurationRule mStackDurationRule;
    
    public bool mInputValueBool;
    public float mInputValue;
    public int mInputValueInt;
    
    public int mStackCount;
    public int mMaxStackCount;
    public float mDuration;
    public float mApplyTime;
    public float mExpiredTime;
    
    public bool mCanBePurged;
    public bool mIgnored;
    public bool mExpired;
    public bool mIsRemoveOnCombatEnd;

    public BattleEffect() {}
    
    public BattleEffect(BattleEffectSpec spec)
    {
        mInstigator = spec.mInstigator;
        mTarget = spec.mTarget;
        mAbility = spec.mAbility;
        mType = spec.mType;
        mDurationRule = spec.mDurationRule;
        mStackDurationRule = spec.mStackDurationRule;
        mDuration = spec.mDuration;
        mInputValue = spec.mInputValue;
        mInputValueInt = spec.mInputValueInt;
        mInputValueBool = spec.mInputValueBool;
        mCanBePurged = spec.mCanBePurged;
        mIsRemoveOnCombatEnd = spec.mIsRemoveOnCombatEnd;
        
        mMaxStackCount = spec.mMaxStackCount;
        mStackCount = 1;
            
        mIgnored = false;
        mExpired = false;
    }

    public void MarkInvalid()
    {
        mExpired = true;
    }
}