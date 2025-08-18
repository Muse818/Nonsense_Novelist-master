

using System.Collections.Generic;
using UnityEngine;

public class ServantsAgent : UnitComponent
{
    protected List<BattleUnit> mServants = new List<BattleUnit>();
    public List<BattleUnit> Servants => mServants;

    public override void Start()
    {
        foreach (var ser in mOwner.Data.mInitServants)
        {
            RegisterServants(ser);
        }
    }

    public override void OnSelfDeath(DamageReport report)
    {
        foreach (var ser in Servants)
        {
            ser.Die(report);
        }
    }

    public BattleUnit RegisterServants(int servantKind)
    {
        var unitData = BattleUnitTable.Find(servantKind);
        if (unitData == null)
            return null;

        if (unitData.mInitType != BattleUnitType.Servant)
        {
            Debug.LogError($"单位[{mOwner.UnitInstance.mKind}]的随从单位[{servantKind}]不是随从类型!");
            return null;
        }
        
        UnitInstance servantInstance = new UnitInstance()
        {
            mKind = servantKind,
            mCamp = mOwner.Camp
        };
        
        BattleUnit newServant = mOwner.Battle.ObjectFactory.CreateBattleUnit(servantInstance);
        if (newServant == null)
            return null;

        if (!IsVaildServants(newServant)) 
            return null;
        
        while (mServants.Count > 2)
        {
            RemoveServants();
        }
        
        newServant.ServantOwner = mOwner;
        
        Servants.Add(newServant);
        return newServant;
    }

    public void RemoveServants(int index = 0)
    {
        if (Servants.Count <= index) 
            return;

        Servants[index].Die(new DamageReport());
        Servants.RemoveAt(index);
    }

    public bool RemoveServants(BattleUnit servant)
    {
        if (!Servants.Contains(servant)) 
            return false;

        servant.Die(new DamageReport());
        Servants.Remove(servant);
        return true;
    }

    protected bool IsVaildServants(BattleUnit servant)
    {
        if (servant.Data.mInitType != BattleUnitType.Servant) 
            return false;
        
        return true;
    }
    public List<BattleUnit> GetEnemiesServantList(BattleUnit unit)
    {
        var enemies = unit.Battle.CampManager.GetEnemies(unit);
        var list = new List<BattleUnit>();
        foreach (var enemy in enemies)
        {
            foreach (var servant in enemy.ServantsAgent.Servants)
            {
                list.Add(servant);
            }
        }
        return list;
    }
    public BattleUnit GetEnemyMaxServantUnit(BattleUnit unit)
    {
        var enenemies = unit.Battle.CampManager.GetEnemies(unit);
        BattleUnit MaxServantunit = null;
        var maxcount = 0;
        foreach (var enemy in enenemies)
        {
            if (enemy.IsAlive && enemy.ServantsAgent.Servants.Count > 0)
            {
                if (enemy.ServantsAgent.Servants.Count > maxcount)
                {
                    maxcount = enemy.ServantsAgent.Servants.Count;
                    MaxServantunit = enemy;
                }
            }
        }
        return MaxServantunit;
    }
}