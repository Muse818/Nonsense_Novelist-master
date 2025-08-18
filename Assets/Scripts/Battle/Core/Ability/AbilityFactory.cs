

using System;
using System.Collections.Generic;
using System.Linq;

public enum AbilityType
{
    None = 0,
    TemplateAbility = 1,                            // 模板类技能
    PermanentAttribute = 2,
    AttributeMod = 3,
    AttributePercentMod = 4,


    AutoAttack = 10,
    TestUltra = 11,
    AutoHeal = 12,
}

public class AbilityFactory
{
    private static Dictionary<AbilityType, System.Type> mAbilityClassMap = new Dictionary<AbilityType, System.Type>()
    {
        { AbilityType.TemplateAbility, typeof(AbilityTemplate) },
        { AbilityType.PermanentAttribute, typeof(AbilityPermanentAttribute) },
        { AbilityType.AutoAttack, typeof(AbilityAutoAttack) },
        { AbilityType.AutoHeal, typeof(AbilityAutoHeal) },
        { AbilityType.TestUltra, typeof(AbilityTestUltra) },
    };

    public static AbilityBase CreateAbility(int abiID)
    {
        var data = AbilityTable.Find(abiID);
        if (data == null || data.mForbidden)
            return null;
        
        if (data.mType == AbilityType.TemplateAbility)
        {
            return CreateTemplateAbility(data);
        }
        else
        {
            if (!mAbilityClassMap.TryGetValue(data.mType, out var type))
                return null;

            AbilityBase ability = (AbilityBase)System.Activator.CreateInstance(type);
            ability.Data = data;

            if (!ability.ParseParams())
                return null;
            
            return ability;
        }
    }

    public static AbilityTemplate CreateTemplateAbility(int abiID)
    {
        var data = AbilityTable.Find(abiID);
        if (data == null)
            return null;

        return CreateTemplateAbility(data);
    }

    public static AbilityTemplate CreateTemplateAbility(AbilityTable.Data data)
    {
        var triggerData = AbilityTriggerTable.Find(data.mTriggerKind);
        if (triggerData == null)
            return null;
        
        var selectorData = AbilitySelectorTable.Find(data.mSelectorKind);
        if (selectorData == null)
            return null;

        var effectDataList = data.mEffectApplierList.Select(AbilityEffectApplierTable.Find).Where(effectData => effectData != null).ToList();
        if (effectDataList.Count == 0)
            return null;

        var trigger = AbilityTrigger.Create((AbilityTrigger.Type)triggerData.mType);
        if (trigger == null || !trigger.Setup(triggerData))
            return null;
        
        var selector = AbilityTargetSelector.Create((AbilityTargetSelector.Type)selectorData.mType);
        if (selector == null || !selector.Setup(selectorData))
            return null;

        var effectAppliers = new List<AbilityEffectApplier>();
        
        foreach (var effectData in effectDataList)
        {
            var effectApplier = AbilityEffectApplier.Create((AbilityEffectApplier.Type)effectData.mType);
            if (effectApplier == null || !effectApplier.Setup(effectData))
                return null;
            
            effectAppliers.Add(effectApplier);
        }

        var abi = new AbilityTemplate(trigger, selector, effectAppliers)
        {
            Data = data
        };

        return abi;
    }
}