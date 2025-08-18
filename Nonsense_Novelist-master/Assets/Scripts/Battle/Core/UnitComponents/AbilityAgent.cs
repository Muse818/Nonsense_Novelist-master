
using System.Collections.Generic;

// unit ability manager
public class AbilityAgent : UnitComponent
{
    protected List<AbilityBase> mAbilities = new List<AbilityBase>();
    public List<AbilityBase> Abilities => mAbilities;

    public override void Start()
    {
        foreach (var abi in mOwner.Data.mTalents)
        {
            RegisterAbility(abi);
        }
    }

    public AbilityBase RegisterAbility(int abiKind)
    {
        AbilityBase newAbi = AbilityFactory.CreateAbility(abiKind);

        if (newAbi != null)
        {
            newAbi.Unit = Owner;
            newAbi.Init();
            
            mAbilities.Add(newAbi);
        }
        
        return newAbi;
    }
    
    public void RemoveAbility(AbilityBase abiInstance)
    {
        if (abiInstance != null)
        {
            abiInstance.Dispose();
            mAbilities.Remove(abiInstance);
        }
    }

    public AbilityBase GetAbilityByType(AbilityType type)
    {
        foreach (var abi in mAbilities)
        {
            if (abi.Data.mType == type)
            {
                return abi;
            }
        }

        return null;
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var abi in mAbilities)
        {
            abi.Update(deltaTime);
        }
    }
}