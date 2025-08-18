
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageHelper
{
    public static void ProcessDamage(DealDamageCalc damageCalc)
    {
        if (damageCalc == null || damageCalc.mTarget == null)
            return;

        var instigator = damageCalc.mInstigator;
        var target = damageCalc.mTarget;

        DamageMeta meta = new DamageMeta();
        DamageResult result = new DamageResult();
        DamageReport report = new DamageReport();
        report.mMeta = meta;
        report.mResult = result;

        meta.mDamageSource = damageCalc.mDamageSource;
        meta.mInstigator = instigator;
        meta.mTarget = target;
        meta.mAbility = damageCalc.mAbility;
        meta.mWordType = meta.mAbility.WordType;//
        meta.mEffectType = meta.mAbility.EffectType;
        // 计算伤害数值
        if ((damageCalc.mFlag & DealDamageFlag.Fixed) > 0)
        {
            meta.mAttack = Random.Range(damageCalc.mMinAttack, damageCalc.mMaxAttack);
        }
        else
        {
            if (instigator != null)
            {
                instigator.PreDealDamageCalc(damageCalc);
            }

            TakeDamageCalc takeDamageCalc = target.PreTakingDamageCalc(instigator, damageCalc);

            meta.mFlag = damageCalc.mFlag | takeDamageCalc.mFlag;
            
            // 预留闪避处理
            // 预留暴击处理
            
            meta.mAttack = Random.Range(damageCalc.mMinAttack, damageCalc.mMaxAttack);
            meta.mAttack = MathF.Max(meta.mAttack, 0f);

            meta.mDefense = damageCalc.mMagic ? takeDamageCalc.mResistance : takeDamageCalc.mDefense;

            float hitValue = CalcBaseDamage(meta.mAttack, meta.mDefense);

            float damageUp = damageCalc.mDealDamageUp + takeDamageCalc.mTakeDamageUp;
            float damageDown = damageCalc.mDealDamageDown + takeDamageCalc.mTakeDamageDown;

            hitValue *= (1 + damageUp) / (1 + damageDown);
            
            result.mDamage = hitValue;
        }
        
        // 应用伤害前的相关处理
        if (instigator != null)
        {
            instigator.PreDealDamage(report);
        }
        
        target.PreTakeDamage(report);

        
        // 开始应用伤害
        target.ApplyDamage(result.mDamage, true, out var killed);
        if (killed)
        {
            target.Die(report);
        }
        
        // 应用伤害后的相关处理
        if (instigator != null)
        {
            instigator.PostDealDamage(report);
        }
            
        target.PostTakeDamage(report);

        if (instigator != null)
        {
            var allies = instigator.Allies;
            foreach (var p in allies)
            {
                p.AllyDealDamage(report);
            }
                
            var enemies = instigator.Enemies;
            foreach (var p in enemies)
            {
                p.EnemyDealDamage(report);
            }
        }

        {

            var allies = target.Allies;
            foreach (var p in allies)
            {
                p.AllyTakeDamage(report);
            }

            var enemies = target.Enemies;
            foreach (var p in enemies)
            {
                p.EnemyTakeDamage(report);
            }
        }
        if (report.mMeta.mWordType == WordType.Verb&& report.mMeta.mAbility.Data.mVerbDamageCoefficient)
        {
            var Coefficient = instigator.GetAttributeValue(AttributeType.VerbDamageCoefficient);
            var Mod = instigator.GetAttributeValue(AttributeType.VerbDamageMod);
            report.mResult.mDamage = report.mResult.mDamage * (1 + Coefficient) + Mod;  
        }
        if (report.mMeta.mEffectType==EffectType.Damage&& report.mMeta.mAbility.Data.mEffectDamageCoefficient)
        {
            var Coefficient = instigator.GetAttributeValue(AttributeType.EffectDamageCoefficient);
            report.mResult.mDamage = report.mResult.mDamage * (1 +Coefficient);
        }
        if (report.mMeta.mDamageSource==DamageSource.AutoAttack && report.mMeta.mAbility.Data.mNormalAttackDamageCoefficient)
        {
            var Coefficient = instigator.GetAttributeValue(AttributeType.NormalAttackDamage);
            report.mResult.mDamage = report.mResult.mDamage * (1 + Coefficient);
        }
        if (report.mMeta.mDamageSource == DamageSource.AutoAttack && report.mMeta.mAbility.Data.mSuckBloodCoefficient)
        {
            var suckBlood=report.mResult.mDamage * instigator.GetAttributeValue(AttributeType.SuckBlood);
            instigator.ApplyHeal(suckBlood);
        }
        //todo: 伤害结算完成通知
    }

    public static float CalcBaseDamage(float attack, float defense)
    {
        //todo: 20 用全局变量代替
        defense = Mathf.Max(defense, -19.9f);
        return Mathf.Max((attack * 20) / (defense + 20), 0);
    }
}