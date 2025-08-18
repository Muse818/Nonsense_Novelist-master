

using System.Collections.Generic;
using UnityEngine;

public class EffectAgent : UnitComponent
{
    protected List<BattleEffect> mEffects = new List<BattleEffect>();
    public List<BattleEffect> Effects => mEffects;
        
    private List<BattleEffect> mRemovedEffect = new List<BattleEffect>();

    public static BattleEffect ApplyEffectToTarget(BattleUnit target, BattleEffectSpec spec)
    {
        return target.EffectAgent?.ApplyEffect(spec);
    }

    protected BattleEffect ApplyEffect(BattleEffectSpec spec)
    {
        if (!CanApplyEffect(spec))
            return null;

        BattleEffect mergedBe = TryMergeEffect(spec);
        if (mergedBe != null)
            return mergedBe;

        if (spec.mDurationRule == EffectDurationRule.Instant)
        {
            
        }
        else
        {
            BattleEffect newBe = new BattleEffect(spec);

            newBe.mApplyTime = Owner.Battle.Now;
            
            if (newBe.mDurationRule == EffectDurationRule.HasDuration)
            {
                newBe.mExpiredTime = Owner.Battle.Now + spec.mDuration;
                if (BattleHelper.IsNegativeEffect(spec))
                {
                    newBe.mExpiredTime = Owner.Battle.Now + spec.mDuration*(1+ spec.mInstigator.GetAttributeValue(AttributeType.DebuffUp));
                }
            }
            
            mEffects.Add(newBe);

            mOwner.OnSelfApplyEffect(newBe);
            if (newBe.mType == EffectType.Heal)
            {
                mOwner.OnSelfApplyHealEffect(newBe);
            }
            
            mOwner.UnitView.OnApplyEffect(newBe);
            
            EventManager.Invoke(EventEnum.ApplyEffect, newBe);

            return newBe;
        }
        
        return null;
    }

    public bool CanApplyEffect(BattleEffectSpec spec)
    {
        if (spec.mCanBePurged)
        {
            if (BattleHelper.IsPositiveEffect(spec))
            {
                if (Owner.Status.InStatus(Status.BlockPositive))
                {
                    return false;
                }
            }
            else
            {
                if (Owner.Status.InStatus(Status.BlockNegative))
                {
                    return false;
                }
            }
        }
        
        //todo: consider other condition

        return true;
    }

    public BattleEffect TryMergeEffect(BattleEffectSpec spec)
    {
        if (spec.mStackRule == EffectStackRule.None)
            return null;

        BattleEffect mergedBe = null;
        foreach (var effect in mEffects)
        {
            if (effect.mExpired)
                continue;

            if (effect.mType != spec.mType || effect.mAbility != spec.mAbility)
                continue;

            if (spec.mStackRule != effect.mStackRule || effect.mStackDurationRule != spec.mStackDurationRule)
                continue;

            if (spec.mStackRule == EffectStackRule.Source && spec.mInstigator != effect.mInstigator)
                continue;
            
            if (spec.mStackRule == EffectStackRule.Target && spec.mTarget != effect.mTarget)
                continue;

            mergedBe = effect;
            
            if (spec.mStackDurationRule == EffectStackDurationRule.Refresh)
            {
                mergedBe.mExpiredTime = Owner.Battle.Now + spec.mDuration;
            }
            
            if (mergedBe.mMaxStackCount == 0 || mergedBe.mStackCount < mergedBe.mMaxStackCount)
            {
                if (spec.mMergeInputValue)
                {
                    mergedBe.mInputValue = BattleHelper.MergerEffectValue(spec.mType, spec.mInputValue, effect.mInputValue);
                }

                if (mergedBe.mMaxStackCount > 0)
                {
                    mergedBe.mStackCount = Mathf.Clamp(mergedBe.mStackCount + spec.mStackCount, 0, mergedBe.mMaxStackCount);
                }
                else
                {
                    ++mergedBe.mStackCount;
                }
            }
            
            break;
        }
        
        return mergedBe;
    }

    public void ClearEffects()
    {
        foreach (var effect in mEffects)
        {
            mOwner.UnitView.OnRemoveEffect(effect);

            EventManager.Invoke(EventEnum.RemoveEffect, effect);

            effect.Dispose();
        }

        mEffects.Clear();
    }

    public override void LateUpdate(float deltaTime)
    {
        mRemovedEffect.Clear();
        
        foreach (var effect in mEffects)
        {
            if (effect.mExpired)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.mDurationRule == EffectDurationRule.HasDuration && Owner.Battle.Now > effect.mExpiredTime)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.mDurationRule == EffectDurationRule.Script && !effect.mAbility.IsValid())
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.mIsRemoveOnCombatEnd && Owner.Battle.BattlePhase.IsCombat == false)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            var attrType = BattleHelper.ToAttrType(effect.mType);
            if (attrType != AttributeType.None)
            {
                if (effect.mInputValueBool)
                {
                    Owner.AddPercentMod(attrType, effect.mInputValue);
                }
                else
                {
                    Owner.AddMod(attrType, effect.mInputValue);
                }
            }
            else
            {

                switch (effect.mType)
                {
                    case EffectType.Stun:
                        Owner.Status.AddStatus(Status.Stun);
                        break;

                    case EffectType.Damage:
                    {
                        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(effect.mInstigator);
                        dmg.mTarget = mOwner;
                        dmg.mAbility = effect.mAbility;
                        dmg.mAbility.EffectType=effect.mType;//
                        dmg.mMinAttack = effect.mInputValue;
                        dmg.mMaxAttack = dmg.mMinAttack;
                        dmg.mMagic = true;
                        dmg.mFlag |= DealDamageFlag.Fixed;

                        DamageHelper.ProcessDamage(dmg);
                    }
                        break;

                    default:
                        break;
                }
            }
        }

        if (mRemovedEffect.Count > 0)
        {
            foreach (var effect in mRemovedEffect)
            {
                mOwner.UnitView.OnRemoveEffect(effect);
                
                EventManager.Invoke(EventEnum.RemoveEffect, effect);
                
                mEffects.Remove(effect);
                
                effect.Dispose();
            }
        
            mRemovedEffect.Clear();
        }
    }
    
    public override void OnSelfDeath(DamageReport report)
    {
        ClearEffects();
    }

    public override void OnExitCombatPhase()
    {
        // 清除buff
        ClearEffects();
    }
}