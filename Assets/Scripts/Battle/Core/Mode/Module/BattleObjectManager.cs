
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleObjectManager : BattleModule
{
    protected int mGenID = 0;
    protected bool mStarted = false;
    protected List<int> mRemovedIDs = new List<int>();

    protected Dictionary<int, BattleObject> mObjects = new Dictionary<int, BattleObject>();
    protected Dictionary<int, BattleUnit> mUnits = new Dictionary<int, BattleUnit>();
    protected Dictionary<Collider2D, WallObject> mWalls = new Dictionary<Collider2D, WallObject>();

    public Dictionary<int, BattleObject> Objects => mObjects;
    public Dictionary<int, BattleUnit> Units => mUnits;
    public Dictionary<Collider2D, WallObject> Walls => mWalls;

    public T Find<T>(int id) where T : BattleObject
    {
        if (mObjects.TryGetValue(id, out var obj))
        {
            return obj as T;
        }

        return null;
    }

    public T Find<T>(Collider2D col) where T : WallObject
    {
        if (mWalls.TryGetValue(col, out var obj))
        {
            return obj as T;
        }

        return null;
    }

    public bool RegisterObject(BattleObject obj)
    {
        if (obj.IsRegistered)
            return false;

        obj.ID = ++mGenID;
        obj.Battle = Battle;
        obj.IsRegistered = true;

        obj.Start();

        mObjects.TryAdd(obj.ID, obj);

        return true;
    }

    public bool RegisterWall<T>(T wall, Collider2D collider) where T : WallObject
    {
        if (wall.IsRegistered)
            return false;

        wall.IsRegistered = true;
        wall.mWall.collider = collider;

        wall.Start();

        mWalls.TryAdd(wall.mWall.collider, wall);
        return true;
    }

    public bool RegisterUnit(BattleUnit unit)
    {
        if (unit.IsRegistered)
            return false;

        unit.Init();

        RegisterObject(unit);

        mUnits.Add(unit.ID, unit);

        return true;
    }

    public void RemoveObject(BattleObject obj)
    {
        if (obj != null)
        {
            obj.MarkPendingKill();
        }
    }

    public void ClearTrashObjects()
    {
        {
            foreach (var objIt in mObjects.Values)
            {
                if (objIt.IsPendingKill)
                {
                    objIt.Dispose();
                    mRemovedIDs.Add(objIt.ID);
                }
            }

            foreach (var objId in mRemovedIDs)
            {
                mObjects.Remove(objId);
            }

            mRemovedIDs.Clear();
        }

        {
            foreach (var objIt in mUnits.Values)
            {
                if (objIt.IsPendingKill)
                {
                    objIt.Dispose();
                    mRemovedIDs.Add(objIt.ID);
                }
            }

            foreach (var objId in mRemovedIDs)
            {
                mUnits.Remove(objId);
            }
        }
        mRemovedIDs.Clear();
    }

    public override void Update(float deltaSec)
    {
        ClearTrashObjects();

        foreach (var objIt in mObjects.Values.Where(objIt => objIt.IsTickEnable))
        {
            objIt.Update(deltaSec);
        }
    }

    public override void LateUpdate(float deltaSec)
    {
        foreach (var objIt in mObjects.Values.Where(objIt => objIt.IsTickEnable))
        {
            objIt.LateUpdate(deltaSec);
        }
    }

    public override void LateFixedUpdate(float deltaSec)
    {
        foreach (var objIt in mObjects.Values.Where(objIt => objIt.IsTickEnable))
        {
            objIt.LateFixedUpdate(deltaSec);
        }
    }
    
    public override void OnEnterCombatPhase()
    {
        foreach (var objIt in mObjects.Values)
        {
            if (!objIt.IsPendingKill)
            {
                objIt.OnEnterCombatPhase();
            }
        }
    }
    
    public override void OnExitCombatPhase()
    {
        foreach (var objIt in mObjects.Values)
        {
            if (!objIt.IsPendingKill)
            {
                objIt.OnExitCombatPhase();
            }
        }
    }

    public override void OnEnterResetPhase()
    {
        foreach (var objIt in mObjects.Values)
        {
            if (!objIt.IsPendingKill)
            {
                objIt.OnEnterRestPhase();
            }
        }
    }

    public override void OnExitRestPhase()
    {
        foreach (var objIt in mObjects.Values)
        {
            if (!objIt.IsPendingKill)
            {
                objIt.OnExitRestPhase();
            }
        }
    }
}