
using System.Collections.Generic;
using UnityEngine;

public class UnitDeckManager : BattleModule
{
    public class UnitPoolItem
    {
        public int mKind = 0;
        public int mLevel = 0;
        public int mUsed = 0;
    }
    
    protected Dictionary<int, UnitPoolItem> mCharacterPools = new Dictionary<int, UnitPoolItem>();
    protected Dictionary<int, UnitPoolItem> mMonsterPools = new Dictionary<int, UnitPoolItem>();
    
    public override void Init()
    {
        mCharacterPools.Clear();
        foreach (var data in BattleUnitTable.DataList.Values)
        {
            if (data.mForbidden)
                continue;

            if (data.mInitType == BattleUnitType.Character)
            {
                mCharacterPools.Add(data.mKind, new UnitPoolItem()
                {
                    mKind = data.mKind
                });
            }
        }
        
        mMonsterPools.Clear();
        foreach (var data in BattleUnitTable.DataList.Values)
        {
            if (data.mForbidden)
                continue;
            
            if (data.mInitType == BattleUnitType.Monster)
            {
                mMonsterPools.Add(data.mKind, new UnitPoolItem()
                {
                    mKind = data.mKind,
                    mLevel = data.mLevel,
                });
            }
        }
    }

    public override void Begin()
    {
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitSpawn, OnUnitSpawn);
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitDie, OnUnitDie);
    }

    public void OnUnitSpawn(BattleUnit unit)
    {
        switch (unit.Data.mInitType)
        {
            case BattleUnitType.Character:
            {
                if (mCharacterPools.TryGetValue(unit.Data.mKind, out var unitPool))
                {
                    ++unitPool.mUsed;
                }
                break;
            }

            case BattleUnitType.Monster:
            {
                if (mCharacterPools.TryGetValue(unit.Data.mKind, out var unitPool))
                {
                    ++unitPool.mUsed;
                }
                break;
            }
        }
    }

    public void OnUnitDie(BattleUnit unit)
    {
        
    }

    public void AddVisitor(int kind)
    {
        var data = BattleUnitTable.Find(kind);
        if (data != null)
        {
            mCharacterPools.Add(kind, new UnitPoolItem()
            {
                mKind = data.mKind
            });
        }
    }

    public List<int> ShuffleUnit(int count, BattleUnitType type = BattleUnitType.Character, int level = -1)
    {
        List<int> result = new List<int>();

        switch (type)
        {
            case BattleUnitType.Character:
            {

                foreach (var it in mCharacterPools.Values)
                {
                    if (it.mUsed == 0)
                    {
                        result.Add(it.mKind);
                    }
                }

                break;
            }

            case BattleUnitType.Monster:
            {
                foreach (var it in mMonsterPools.Values)
                {
                    if (level == -1 || level == it.mLevel)
                    {
                        result.Add(it.mKind);
                    }
                }

                break;
            }
        }

        return NnMathUtils.Shuffle(result, count);
    }

    public int RandomUnit(BattleUnitType type = BattleUnitType.Character, int level = -1)
    {
        var rst = ShuffleUnit(1, type, level);

        return rst.Count > 0 ? rst[0] : -1;
    }
}