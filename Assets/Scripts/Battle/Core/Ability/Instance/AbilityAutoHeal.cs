using System.Collections.Generic;
using UnityEngine;

public class AbilityAutoHeal : AbilityActive
{
    public override float CD => mData.mCoolDown * Unit.GetAttributeValue(AttributeType.Attack);

    protected override void OnInit()
    {
        base.OnInit();
        Unit.AIAgent.RegisterAttackAbility(this);
    }
    public virtual List<BattleUnit> PickTarget() 
    {
        var allies = Unit.Allies;
        var ori = Unit.UnitView.transform.position;
        allies.Sort((a, b) => (a.UnitView.transform.position - ori).sqrMagnitude.CompareTo((b.UnitView.transform.position - ori).sqrMagnitude));

        return Trim(allies);
    }
    public virtual List<BattleUnit> Trim(List<BattleUnit> targetList)
    {
        return targetList;
    }
}
    