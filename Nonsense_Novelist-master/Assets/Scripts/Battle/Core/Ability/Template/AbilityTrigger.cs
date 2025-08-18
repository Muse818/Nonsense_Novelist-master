
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class AbilityTrigger : AbilityModule
{
    public enum Type
    {
        Undefined = 0,
        Direct = 1,                                // 直接触发
        Tick = 2,                                  // 每隔多少秒
        DirectCall = 3,                            // 外部驱动

        /* 伤害类 */
        AutoAttackTimes = 30,                       // 每隔几次自动攻击
        BeAutoAttackTimes = 31,                     // 每受到几次自动攻击
        TakeDamageByValue = 32,                     // 每受到几点伤害

        /* 死亡类 */
        AnyDeath = 100,                              // 任意单位死亡时
        SelfDeath = 101,                             // 自己死亡时
        AllyDeath = 102,                             // 友军死亡时
        EnemyDeath = 103,                            // 敌人死亡时
        ServantDeath = 104,                          // 随从死亡时

        AccOursideBuffTypeThreshold=127,              //己方到当前累计每拥有多少某buff
        NowOursideBuffTypeThreshold = 128,            //己方当前时刻每拥有多少某buff
        OursideServantThreshold = 129,                //己方每拥有多少随从
        SelfAttributeThreshold = 130,                 //自身属性达到多少
        SelfNounTreshold = 131,                       //自身拥有多少名词时
        SelfHealTreshold = 132,                       //自身治疗累计多少时
        SelfServantTreshold = 133,                    //自身随从达到多少时
        SelfReincarnationTreshold = 134,              //自身轮回多少次时
        SelfDebuffThreshold = 135,                    //施加多少次负面状态后
        AccDamageThreshold = 136,                     //累计造成多少伤害
        AccStealNounTreshold = 137,                   //累计偷取名词（老鼠）
    }

    public static AbilityTrigger Create(Type type)
    {
        AbilityTrigger trigger = null;

        switch (type)
        {
            case Type.Direct:
                trigger = new AMTDirect();
                break;

            case Type.Tick:
                trigger = new AMTTick();
                break;

            case Type.DirectCall:
                trigger = new AMTDirectCall();
                break;

            case Type.AutoAttackTimes:
                trigger = new AMTAttackTimes();
                break;

            case Type.BeAutoAttackTimes:
                trigger = new AMTBeAutoAttackTimes();
                break;

            case Type.TakeDamageByValue:
                trigger = new AMTTakeDamageByValue();
                break;

            case Type.AnyDeath:
                trigger = new AMTAnyDeath();
                break;

            case Type.SelfDeath:
                trigger = new AMTSelfDeath();
                break;

            case Type.AllyDeath:
                trigger = new AMTAllyDeath();
                break;

            case Type.EnemyDeath:
                trigger = new AMTEnemyDeath();
                break;

            case Type.ServantDeath:
                trigger = new AMTServantDeath();
                break;

            case Type.NowOursideBuffTypeThreshold:
                trigger = new AMTNowOursideBuffTypeThreshold();
                break;

            case Type.OursideServantThreshold:
                trigger = new AMTOursideServantThreshold();
                break;

            case Type.SelfAttributeThreshold:
                trigger = new AMTSelfAttributeThreshold();
                break;
            case Type.SelfNounTreshold:
                trigger = new AMTSelfNounTreshold();
                break;
            case Type.SelfHealTreshold:
                trigger = new AMTSelfHealTreshold();
                break;
            case Type.SelfServantTreshold:
                trigger = new AMTSelfServantTreshold();
                break;
            case Type.SelfReincarnationTreshold:
                trigger = new AMTSelfReincarnationTreshold();
                break;
            case Type.SelfDebuffThreshold:
                trigger = new AMTSelfDebuffThreshold();
                break;
            case Type.AccDamageThreshold:
                trigger = new AMTAccDamageThreshold();
                break;
            default:
                break;
        }

        if (trigger != null)
            trigger.mType = type;

        return trigger;
    }


    public Type mType = Type.Undefined;

    public AbilityTriggerTable.Data mData = null;

    protected int mMaxTriggerTimes = int.MaxValue;
    protected float mPossibility = 1.0f;
    protected float mCoolDownDuration = 0f;


    protected int mTriggerTimes = 0;
    protected float mLastTriggerTime = 0f;

    public bool CoolDown
    {
        get
        {
            if (mTriggerTimes <= 0 || mCoolDownDuration < 0.01f)
                return true;
            else
                return mOwner.ElapsedSec - mLastTriggerTime >= mCoolDownDuration;
        }
    }

    public bool TriggerTimesReachLimit => mMaxTriggerTimes > 0 && mTriggerTimes >= mMaxTriggerTimes;

    public virtual bool Setup(AbilityTriggerTable.Data data)
    {
        mData = data;

        mMaxTriggerTimes = mData.mMaxTriggerTimes;
        mPossibility = mData.mPossibility;
        mCoolDownDuration = mData.mCoolDownDuration;

        mCustomParams = data.mCustomParams;

        return ParseParams();
    }

    protected bool TryTrigger(object triggerData)
    {
        if (ShouldTrigger())
        {
            mTriggerTimes++;
            mLastTriggerTime = mOwner.ElapsedSec;

            mOwner.TriggeredBy(triggerData);
            return true;
        }

        return false;
    }

    public virtual bool ShouldTrigger()
    {
        if (!CoolDown)
            return false;

        if (TriggerTimesReachLimit)
            return false;

        if (mPossibility < 1 && UnityEngine.Random.Range(0f, 1.0f) >= mPossibility)
            return false;

        return true;
    }

    public virtual bool CanTriggerByOther() { return false; }

    public virtual void TriggerDirect() { }

    public virtual void OnPreDealDamageCalc(DealDamageCalc dmgCalc) { }

    public virtual void OnPreDealDamageCalcOtherAbility(DealDamageCalc dmgCalc) { }

    public virtual void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }

    public virtual void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }


    public virtual void OnPreDealDamage(DamageReport report) { }

    public virtual void OnPreDealDamageOtherAbility(DamageReport report) { }

    public virtual void OnPostDealDamage(DamageReport report) { }

    public virtual void OnPostDealDamageOtherAbility(DamageReport report) { }

    public virtual void OnAllyDealDamage(DamageReport report) { }

    public virtual void OnEnemyDealDamage(DamageReport report) { }


    public virtual void OnPreTakeDamage(DamageReport report) { }

    public virtual void OnAllyPreTakeDamage(DamageReport report) { }

    public virtual void OnPostTakeDamage(DamageReport report) { }

    public virtual void OnAllyTakeDamage(DamageReport report) { }

    public virtual void OnEnemyTakeDamage(DamageReport report) { }

    public virtual void OnPawnDeath(BattleUnit deceased, DamageReport report) { }

    public virtual void OnSelfDeath(DamageReport report) { }

    public virtual void OnSelfApplyEffect(BattleEffect be) { }

    public virtual void OnSelfApplyHealEffect(BattleEffect be) { }
}

public class AMTDirect : AbilityTrigger
{
    public override void OnInit()
    {
        TryTrigger(null);
    }
}

public class AMTTick : AbilityTrigger
{
    public override void Update(float deltaTime)
    {
        TryTrigger(null);
    }
}


public class AMTDirectCall : AbilityTrigger
{
    public override bool CanTriggerByOther()
    {
        return true;
    }

    public override void TriggerDirect()
    {
        TryTrigger(null);
    }
}


public class AMTAttackTimes : AbilityTrigger
{
    protected Formula mAttackTimes = new Formula("attack_times");
    protected int mCurrentAttackTimes = 0;
    
    public override void AddParams()
    {
        mParams.Add(mAttackTimes);
    }

    public override void OnPreDealDamage(DamageReport report)
    {
        if (report.mMeta.mAbility != null && report.mMeta.mAbility.Data.mType == AbilityType.AutoAttack)
        {
            ++mCurrentAttackTimes;
            if (mCurrentAttackTimes >= mAttackTimes.EvaluateInt(mOwner))
            {
                mCurrentAttackTimes = 0;
                TryTrigger(report);
            }
        }
    }
}
public class AMTBeAutoAttackTimes : AbilityTrigger
{
    protected Formula mBeAttackTimes = new Formula("attack_times");
    protected int mCurrentBeAttackTimes = 0;

    public override void AddParams()
    {
        mParams.Add(mBeAttackTimes);
    }

    public override void OnPostTakeDamage(DamageReport report)
    {
        if (report.mMeta.mAbility != null && report.mMeta.mAbility.Data.mType == AbilityType.AutoAttack)
        {
            ++mCurrentBeAttackTimes;
            if (mCurrentBeAttackTimes >= mBeAttackTimes.EvaluateInt(mOwner))
            {
                mCurrentBeAttackTimes = 0;
                TryTrigger(report);
            }
        }
    }
}
public class AMTTakeDamageByValue : AbilityTrigger
{
    protected Formula mDamageValue = new Formula("damage_value");

    public override void AddParams()
    {
        mParams.Add(mDamageValue);
    }
    public override void OnPostTakeDamage(DamageReport report)
    {
        if (report.mMeta.mAbility != null && report.mResult.mDamage>=mDamageValue.Evaluate(mOwner))
        {
           TryTrigger(report);
        }
    }
}

public class AMTAnyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTSelfDeath : AbilityTrigger
{
    public override void OnSelfDeath(DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTAllyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp == mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTEnemyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp != mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTServantDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.ServantOwner == mOwner.Unit)
        {
            TryTrigger(report);
        }
    }
}
public class AMTNowOursideBuffTypeThreshold : AbilityTrigger
{
    protected Formula mBuffType = new Formula("buff_type");
    protected Formula mBuffThreshold = new Formula("buff_threshold");
    protected int mBuffNum = 0;
    public override void AddParams()
    {
        mParams.AddRange(new[] { mBuffType, mBuffThreshold });
    }
    public override void Update(float deltaTime)
    {
        if (GetBuffNum(mBuffNum) >= mBuffThreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
        }
    }
    public int GetBuffNum(int num)
    {
        var mSelfCurrentBuffNum = 0;
        var mAlliesCurrentBuffNum = 0;
        if (mOwner.Unit.EffectAgent.Effects != null)
        {
            foreach (var effect in mOwner.Unit.EffectAgent.Effects)
            {
                if (effect.mType == (EffectType)mBuffType.EvaluateInt(mOwner))
                {
                    ++mSelfCurrentBuffNum;
                }
            }
        }
        if (mOwner.Unit.Allies != null)
        {
            foreach (var unit in mOwner.Unit.Allies)
            {
                if (unit.EffectAgent.Effects != null)
                {
                    foreach (var effect in unit.EffectAgent.Effects)
                    {
                        if (effect.mType == (EffectType)mBuffType.EvaluateInt(mOwner))
                        {
                            ++mAlliesCurrentBuffNum;
                        }
                    }
                }
            }
        }
        return num=mSelfCurrentBuffNum + mAlliesCurrentBuffNum; 
    }
    public override void OnSelfApplyEffect(BattleEffect be)
    {
        if(be.mType== (EffectType)mBuffType.EvaluateInt(mOwner))
        {
            
        }
    }
}

public class AMTOursideServantThreshold : AbilityTrigger
{
    protected Formula mServantThreshold = new Formula("servant_threshold");
    protected int mCurrentServant;
    public override void AddParams()
    {
        mParams.Add(mServantThreshold);
    }
    public override void Update(float deltaTime)
    {
        mCurrentServant=mOwner.Unit.ServantsAgent.Servants.Count;
        if (mOwner.Unit.Allies != null)
        {
            foreach (var unit in mOwner.Unit.Allies)
            {
                mCurrentServant += unit.ServantsAgent.Servants.Count;
            }
        }
        if (mCurrentServant >= mServantThreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
        }
    }
}
public class AMTSelfAttributeThreshold : AbilityTrigger
{
    protected Formula mTargetAttribute = new Formula("attribute_type");
    protected Formula mComparisonOp = new Formula("comparison_op");
    protected Formula mThreshold = new Formula("attr_threshold");
    public override void AddParams()
    {
       mParams.AddRange(new[] { mTargetAttribute, mComparisonOp, mThreshold });
    }
    public override void Update(float deltaTime)
    {
        if (CheckCondition())
        {
            TryTrigger(null);
        }
    }
    private bool CheckCondition()
    {
        var attributeType = (AttributeType)mTargetAttribute.EvaluateInt(mOwner);
        var currentValue = mOwner.Unit.Hp;
        var threshold = mThreshold.Evaluate(mOwner)*mOwner.Unit.MaxHp;

        return CompareValues(
            currentValue,
            threshold,
            (ComparisonOperator)mComparisonOp.EvaluateInt(mOwner)
        );
    }
    private bool CompareValues(float a, float b, ComparisonOperator op)
    {
        return op switch
        {
            ComparisonOperator.LessThan => a < b,
            ComparisonOperator.LessOrEqual => a <= b,
            ComparisonOperator.Equal => Mathf.Approximately(a, b),
            ComparisonOperator.GreaterOrEqual => a >= b,
            ComparisonOperator.GreaterThan => a > b,
            _ => false
        };
    }
}
public class AMTSelfNounTreshold : AbilityTrigger
{
    protected Formula mNounTreshold = new Formula("noun_treshold");
    protected int mCurrentNounNum;
    public override void OnInit()
    {
        mCurrentNounNum=mOwner.Unit.WordComponent.GetWordsByType(WordType.Noun).Count;
    }
    public override void AddParams()
    {
        mParams.Add(mNounTreshold);
    }
    public override void Update(float deltaTime)
    {
        mCurrentNounNum = mOwner.Unit.WordComponent.GetWordsByType(WordType.Noun).Count;
        if (mCurrentNounNum>=mNounTreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
        }
    }
}
public class AMTSelfHealTreshold : AbilityTrigger
{
    protected Formula mHealTreshold = new Formula("heal_treshold");
    protected int mCurrentHealing = 0;
    public override void AddParams()
    {
        mParams.Add(mHealTreshold);
    }
    public override void Update(float deltaTime)
    {
        if (mCurrentHealing >= mHealTreshold.EvaluateInt(mOwner))
        {
            mCurrentHealing -= mHealTreshold.EvaluateInt(mOwner);
            TryTrigger(null);
        }
    }
    public override void OnSelfApplyHealEffect(BattleEffect be)
    {
        if (be.mType == EffectType.MaxHpUp&&mOwner.Unit.Hp<mOwner.Unit.MaxHp)
        {
            mCurrentHealing += be.mInputValueInt;
        }
    }
}
public class AMTSelfServantTreshold : AbilityTrigger
{
    protected Formula mServantTreshold = new Formula("servant_treshold");
    protected int mCurrentServantNum ;
    public override void AddParams()
    {
        mParams.Add(mServantTreshold);
    }
    public override void Update(float deltaTime)
    {
        mCurrentServantNum=mOwner.Unit.ServantsAgent.Servants.Count;
        if (mCurrentServantNum >= mServantTreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
        }
    }
}
public class AMTSelfReincarnationTreshold : AbilityTrigger
{
    protected Formula mSelfReincarnationTreshold = new Formula("reincarnation_treshold");
    protected int mCurrentReincarnationNum=0;
    public override void AddParams()
    {
        mParams.Add(mSelfReincarnationTreshold);
    }
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.ServantOwner == mOwner.Unit)
        {
            if (deceased.Hp == deceased.MaxHp * 0.3)
            {
                ++mCurrentReincarnationNum;
                if (mCurrentReincarnationNum >= mSelfReincarnationTreshold.EvaluateInt(mOwner))
                {
                    TryTrigger(null);
                    mCurrentReincarnationNum = 0;
                }
            }
        }
    }
}
public class AMTSelfDebuffThreshold : AbilityTrigger
{
    protected Formula mSelfDebuffThreshold = new Formula("debuff_treshold");
    protected int mCurrentDebuffNum=0;
    public override void AddParams()
    {
        mParams.Add(mSelfDebuffThreshold);
    }
    public override void Update(float deltaTime)
    {
        if (mOwner.Unit.EffectAgent.Effects != null) 
        {
            foreach (var effect in mOwner.Unit.EffectAgent.Effects)
            {
                if (effect.mType.ToString().Substring(effect.mType.ToString().Length-4)== "Down")
                {
                    ++mCurrentDebuffNum;
                }
            }
        }
        if (mCurrentDebuffNum >= mSelfDebuffThreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
            mCurrentDebuffNum = 0;
        }
        else
        {
            mCurrentDebuffNum = 0;
        }
    }
}
public class AMTAccDamageThreshold : AbilityTrigger
{
    protected Formula mAccDamageThreshold = new Formula("damage_treshold");
    protected Formula mFixDamage = new Formula("fix_damage");
    protected Formula mPsyDamage = new Formula("psy_damage");
    protected Formula mMagicDamage = new Formula("magic_damage");
    protected List<DamageType> mDamageData = new List<DamageType>();
    protected float mCurrentAccDamage=0f;
    public override void AddParams()
    {
        mParams.AddRange(new[] { mAccDamageThreshold,mFixDamage,mPsyDamage,mMagicDamage });
    }
    public override void OnInit()
    {
        List<Formula> formulas = new List<Formula>() { mFixDamage,mPsyDamage,mMagicDamage};
        foreach (var item in formulas)
        {
            if (item.EvaluateInt(mOwner) != 0)
            {
                if(item.mKey.ToString() == "fix_damage")
                {
                    mDamageData.Add(DamageType.Fix);
                }else if(item.mKey.ToString() == "psy_damage")
                {
                    mDamageData.Add(DamageType.Psy);
                }
                else
                {
                    mDamageData.Add(DamageType.Magic);
                }
            }
        }
    }
    public override void OnPostDealDamage(DamageReport report)
    {
        foreach (var type in mDamageData)
        {
            if (report.mMeta.mDamageType==type)
            {
                mCurrentAccDamage += report.mResult.mDamage;
                if (mCurrentAccDamage >= mAccDamageThreshold.Evaluate(mOwner))
                {
                    TryTrigger(null);
                    mCurrentAccDamage = 0;
                }
            }
        }
    } 
}