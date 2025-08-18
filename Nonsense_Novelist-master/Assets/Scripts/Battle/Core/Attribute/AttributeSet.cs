using System.Collections.Generic;
using System;
using System.Diagnostics;

public class AttributeSet
{
    public Dictionary<AttributeType, Attribute> mAttributes = new Dictionary<AttributeType, Attribute>();
    
    public event Action<AttributeSet> OnAttributeChanged;

    public void Define(AttributeType type, float initValue = 0)
    {
        
        if (mAttributes.ContainsKey(type))
        {
            mAttributes[type].mBaseValue = initValue;
            mAttributes[type].mOriginValue = initValue;
        }
        else
        {
            Attribute attr = new Attribute
            {
                mType = type,
                mOriginValue = initValue,
                mBaseValue = initValue,
                mValue = initValue,
                mMod = 0,
                mPercentMod = 0
            };

            mAttributes.TryAdd(type, attr);
        }
        
        OnAttributeChanged?.Invoke(this); 
    }
    

    public Attribute GetAttribute(AttributeType type)
    {
        return mAttributes.TryGetValue(type, out var attr) ? attr : null;
    }

    public float Get(AttributeType type)
    {
        return mAttributes.TryGetValue(type, out var attr) ? attr.mValue : 0;
    }

    public void AddMod(AttributeType type, float mod)
    {
        if (mAttributes.TryGetValue(type, out var attr))
        {
            attr.mMod += mod;
        }
    }
    
    public void AddPercentMod(AttributeType type, float mod)
    {
        if (mAttributes.TryGetValue(type, out var attr))
        {
            attr.mPercentMod += mod;
        }
    }

    public void ModifyBase(AttributeType type, float mod, bool isPercent = false)
    {
        if (mAttributes.TryGetValue(type, out var attr))
        {
            if (isPercent)
            {
                attr.mBaseValue += attr.mOriginValue * mod;
            }
            else
            {
                attr.mBaseValue += mod;
            } 
            
            OnAttributeChanged?.Invoke(this); 
        }
    }

    public void ApplyMod()
    {
        foreach (var attr in mAttributes.Values)
        {
            attr.mValue = attr.mBaseValue * (1 + attr.mPercentMod) + attr.mMod;
            attr.mMod = 0;
            attr.mPercentMod = 0;
        } 
        
        OnAttributeChanged?.Invoke(this); 
    }
}