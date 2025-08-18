
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStage : BattleModule
{
    #region UnitSlot
    
    private List<UnitSlot> mUnitSlots = new List<UnitSlot>();
    private int mGenID = 0;

    public static bool IsValidSlot(UnitSlot slot)
    {
        return slot != null && slot.ID > 0;
    }
    
    public override void Init()
    {
        mUnitSlots.Clear();
        
        var slots = GameObject.FindObjectsByType<UnitSlot>(FindObjectsSortMode.InstanceID);
        foreach (var slot in slots)
        {
            if (slot.gameObject.activeSelf)
            {
                AddSlot(slot);
            }
        }
    }

    public void AddSlot(UnitSlot slot)
    {
        slot.ID = ++mGenID;
        mUnitSlots.Add(slot);
    }

    public void RemoveSlot(UnitSlot slot)
    {
        if (slot == null)
            return;

        mUnitSlots.Remove(slot);
    }

    public UnitSlot FindSlot(UnitSlotType typeFlags)
    {
        foreach (var slot in mUnitSlots)
        {
            if (!IsValidSlot(slot))
                continue;
            
            if (slot.IsOccupied)
                continue;
            
            if ((slot.SlotType & typeFlags) == 0)
                continue;
            
            return slot;
        }

        return null;
    }

    public UnitSlot FindClosestSlot(UnitSlotType typeFlags, Vector2 pos, float range, bool ignoreOccupied = false)
    {
        float misDisSqr = float.MaxValue;
        UnitSlot closestSlot = null;

        foreach (var slot in mUnitSlots)
        {
            if (!IsValidSlot(slot))
                continue;

            if (!ignoreOccupied && slot.IsOccupied)
                continue;
            
            if ((slot.SlotType & typeFlags) == 0)
                continue;

            float disSqr = (slot.Pos - pos).sqrMagnitude;
            if (disSqr > range * range)
                continue;

            if (closestSlot == null || misDisSqr > disSqr)
            {
                closestSlot = slot;
                misDisSqr = disSqr;
            }
        }

        return closestSlot;
    }

    public UnitSlot GetSlot(int id)
    {
        foreach (var slot in mUnitSlots)
        {
            if (IsValidSlot(slot) && slot.ID == id)
            {
                return slot;
            }
        }

        return null;
    }
    #endregion

    public BattleUnit SpawnUnit(int kind, UnitSlotType typeFlags, Vector2 pos, float range = 0.5f)
    {
        var slot = FindClosestSlot(typeFlags, pos, range);
        if (slot != null)
        {
            UnitInstance instance = new UnitInstance()
            {
                mKind = kind,
                mCamp = slot.SpawnCamp,
            };

            UnitPlacement placement = new UnitPlacement()
            {
                mSlotIndex = slot.ID,
            };
            
            return Battle.ObjectFactory.CreateBattleUnit(instance, placement);
        }

        return null;
    }


    public BattleUnit SpawnUnit(int kind, UnitSlotType typeFlags)
    {
        var slot = FindSlot(typeFlags);
        if (slot != null)
        {
            UnitInstance instance = new UnitInstance()
            {
                mKind = kind,
                mCamp = slot.SpawnCamp,
            };

            UnitPlacement placement = new UnitPlacement()
            {
                mSlotIndex = slot.ID,
            };
            
            return Battle.ObjectFactory.CreateBattleUnit(instance, placement);
        }

        return null;
    }
}