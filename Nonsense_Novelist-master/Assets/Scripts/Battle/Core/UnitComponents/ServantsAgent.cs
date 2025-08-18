

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

    public BattleUnit RegisterServants(int servantID)
    {
        var unitData = BattleUnitTable.Find(servantID);
        if (unitData == null)
            return null;

        if (unitData.mInitType != BattleUnitType.Servant)
        {
            Debug.LogError($"单位[{mOwner.UnitInstance.mKind}]的随从单位[{servantID}]不是随从类型!");
            return null;
        }
        
        UnitInstance servantInstance = new UnitInstance()
        {
            mKind = servantID,
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

}