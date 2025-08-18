

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleHelper
{
    protected static DealDamageCalc mReusableDealDamageCalc = new DealDamageCalc();
    public static DealDamageCalc GetReusableDealDamageCalc(BattleUnit instigator)
    {
        //todo : fill calc info
        mReusableDealDamageCalc.Reset();
        
        mReusableDealDamageCalc.mInstigator = instigator;
        
        return mReusableDealDamageCalc;
    }
    
    protected static TakeDamageCalc mReusableTakeDamageCalc = new TakeDamageCalc();
    public static TakeDamageCalc ReusableTakeDamageCalc => mReusableTakeDamageCalc;

    public static bool IsPositiveEffect(BattleEffectSpec spec)
    {
        return false;
    }
    
    public static bool IsNegativeEffect(BattleEffectSpec spec)
    {
        return !IsPositiveEffect(spec);
    }

    public static float MergerEffectValue(EffectType type, float a, float b)
    {
        //todo: expand merge function
        return a + b;
    }

    public static EffectType ToEffectType(AttributeType type, bool bPositive)
    {
        switch (type)
        {
            case AttributeType.Attack:
                return bPositive ? EffectType.AttackUp : EffectType.AttackDown;
            
            case AttributeType.Def:
                return bPositive ? EffectType.DefUp : EffectType.DefDown;
            
            case AttributeType.Psy:
                return bPositive ? EffectType.PsyUp : EffectType.PsyDown;
            
            case AttributeType.San:
                return bPositive ? EffectType.SanUp : EffectType.SanDown;
            
            case AttributeType.MaxHp:
                return bPositive ? EffectType.MaxHpUp : EffectType.MaxHpDown;
            
            default:
                break;
        }

        return EffectType.None;
    }
    
    public static AttributeType ToAttrType(EffectType type)
    {
        switch (type)
        {
            case EffectType.AttackUp:
            case EffectType.AttackDown:
                return AttributeType.Attack;
            
            case EffectType.DefUp:
            case EffectType.DefDown:
                return AttributeType.Def;
            
            case EffectType.PsyUp:
            case EffectType.PsyDown:
                return AttributeType.Psy;
            
            case EffectType.SanUp:
            case EffectType.SanDown:
                return AttributeType.San;
            
            case EffectType.MaxHpUp:
            case EffectType.MaxHpDown:
                return AttributeType.MaxHp;
        }

        return AttributeType.None;
    }

    
    private static Dictionary<ShootType, string> shootStrMap = new Dictionary<ShootType, string>()
    {

        { ShootType.Split, "分裂" },
        { ShootType.Activate, "激活" },
        { ShootType.spread, "传播" },
        { ShootType.Alpha, "穿透" },
        { ShootType.Start, "起兴" },
        { ShootType.Servants, "连及" },
        { ShootType.Dead, "歇后" },
        { ShootType.Add, "递进" },
        { ShootType.Small, "委婉" },
        { ShootType.Big, "直白" },
        { ShootType.Mirror, "对仗" },
        { ShootType.Expect, "衬托" },
        { ShootType.Copy, "比喻" },
        { ShootType.SameChara, "顶针" },
        { ShootType.ReTrigger, "回环" }
    };

    public static string GetShootTypeString(ShootType type)
    {
        if (shootStrMap.TryGetValue(type, out var rst))
        {
            return rst;
        }
        return "";
    }
}