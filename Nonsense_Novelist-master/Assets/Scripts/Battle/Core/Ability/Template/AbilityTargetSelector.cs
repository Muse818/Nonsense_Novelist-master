
using UnityEngine;
using System.Collections.Generic;

public class AbilityTargetSelector : AbilityModule
{
    public enum Type
    {
        None = 0,
        Instigator = 1,
        Target = 2,
        LowHp = 3,
        HighestAttack = 4,
        HighestSan = 5,
        HighestDef = 6,
        Nearest = 7,
    }

    public static AbilityTargetSelector Create(Type type)
    {
        AbilityTargetSelector selector = null;
        
        switch (type)
        {
            case Type.Instigator:
                selector = new AMTSInstigator();
                break;
            
            case Type.Target:
                selector = new AMTSTarget();
                break;
            
            case Type.LowHp:
                selector = new AMTSLowHp();
                break;
            
            case Type.HighestAttack:
                selector = new AMTSHighestAttack();
                break;
            
            case Type.HighestSan:
                selector = new AMTSHighestSan();
                break;
            
            case Type.HighestDef:
                selector = new AMTSHighestDef();
                break;
            
            case Type.Nearest:
                selector = new AMTSNearest();
                break;
        }

        if (selector != null)
            selector.mType = type;

        return selector;
    }
    
    public Type mType = Type.None;
    public AbilitySelectorTable.Data mData = null;

    public BattleCamp mTargetCamp = BattleCamp.None;
    public int mTargetCount = 1;

    public virtual bool Setup(AbilitySelectorTable.Data data)
    {
        mData = data;
        mTargetCount = mData.mTargetCount;
        mCustomParams = data.mCustomParams;
        
        return ParseParams();
    }

    public virtual List<BattleUnit> Pick(object triggerData)
    {
        return null;
    }

    public virtual List<BattleUnit> Trim(List<BattleUnit> targetList)
    {
        if (targetList == null || targetList.Count == 0)
            return targetList;
        
        if (mTargetCount <= 1)
            return targetList.GetRange(0, 1);
        
        if (mTargetCount == 999 || mTargetCount >= targetList.Count)
            return targetList;
        
        return targetList.GetRange(0, mTargetCount);
    }
}

public class AMTSInstigator : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        return mOwner.Unit != null ? new List<BattleUnit> { mOwner.Unit } : null;
    }
}

public class AMTSTarget : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        return mOwner.Unit.AIAgent.Target != null ? new List<BattleUnit> { mOwner.Unit.AIAgent.Target } : null;
    }
}

public class AMTSLowHp : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        var allies = mOwner.Unit.Allies;
        allies.Sort((a,b) => a.Hp.CompareTo(b.Hp));
        
        return Trim(allies);
    }
}

public class AMTSHighestAttack : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        var allies = mOwner.Unit.Allies;
        allies.Sort((a,b) => -a.GetAttributeValue(AttributeType.Attack).CompareTo(b.GetAttributeValue(AttributeType.Attack)));
        
        return Trim(allies);
    }
}

public class AMTSHighestSan : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        var allies = mOwner.Unit.Allies;
        allies.Sort((a,b) => -a.GetAttributeValue(AttributeType.San).CompareTo(b.GetAttributeValue(AttributeType.San)));
        
        return Trim(allies);
    }
}

public class AMTSHighestDef : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        var allies = mOwner.Unit.Allies;
        allies.Sort((a,b) => -a.GetAttributeValue(AttributeType.Def).CompareTo(b.GetAttributeValue(AttributeType.Def)));
        
        return Trim(allies);
    }
}

public class AMTSNearest : AbilityTargetSelector
{
    public override List<BattleUnit> Pick(object triggerData)
    {
        var enemies = mOwner.Unit.Enemies;
        var ori = mOwner.UnitView.transform.position;
        enemies.Sort((a,b) => (a.UnitView.transform.position - ori).sqrMagnitude.CompareTo((b.UnitView.transform.position - ori).sqrMagnitude));
        
        return Trim(enemies);
    }
}