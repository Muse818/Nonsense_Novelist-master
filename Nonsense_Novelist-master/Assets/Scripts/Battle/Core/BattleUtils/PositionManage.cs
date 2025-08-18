
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BattleUnitPos
{
    none = 0,
    pos1 = 1,
    pos2 = 2,
    pos3 = 3,
    pos4 = 4,
    pos5 = 5,
    pos6 = 6,
    pos7 = 7,
    pos8 = 8,
    pos_Monster1=9,
    pos_Monster2 = 10,
    pos_Monster3 = 11,
    pos_Boss = 12,
}
public class PositionManage
{
    protected Dictionary<BattleUnitPos, BattleUnit> situationDic = new Dictionary<BattleUnitPos, BattleUnit>();
    public Dictionary<BattleUnitPos, BattleUnit> SituationDic => situationDic;
    public void InitAllPosition()
    {
        situationDic.Clear();
        for (int i = 0; i < 8; i++)
        {
            SetPosition((BattleUnitPos)i);
        }
    }



    public bool SetPosition(BattleUnitPos pos, BattleUnit unit = null)
    {
        if (!situationDic.ContainsKey(pos)) situationDic.Add(pos, null);

        if ((situationDic[pos] != null)&&(pos!=0))
        {
            situationDic[pos].Pos = unit.Pos==BattleUnitPos.none? BattleUnitPos.none: unit.Pos;
        }

        if (unit != null)
        {
            situationDic[pos] = unit;
            unit.Pos = pos;
        }
        return true;
    }

    public BattleUnit[] GetNearBy(BattleUnitPos a)
    {
        BattleUnit[] _resSits = new BattleUnit[3];

        switch (a)
        {
            case BattleUnitPos.pos1: { _resSits[0] = SituationDic[BattleUnitPos.pos2]; _resSits[1] = SituationDic[BattleUnitPos.pos3]; _resSits[2] = null; } break;
            case BattleUnitPos.pos2: { _resSits[0] = SituationDic[BattleUnitPos.pos1]; _resSits[1] = SituationDic[BattleUnitPos.pos4]; _resSits[2] = null; } break;
            case BattleUnitPos.pos3: { _resSits[0] = SituationDic[BattleUnitPos.pos1]; _resSits[1] = SituationDic[BattleUnitPos.pos4]; _resSits[2] = SituationDic[BattleUnitPos.pos_Boss]; } break;
            case BattleUnitPos.pos4: { _resSits[0] = SituationDic[BattleUnitPos.pos2]; _resSits[1] = SituationDic[BattleUnitPos.pos3]; _resSits[2] = null; } break;
            case BattleUnitPos.pos5: { _resSits[0] = SituationDic[BattleUnitPos.pos6]; _resSits[1] = SituationDic[BattleUnitPos.pos7]; _resSits[2] = SituationDic[BattleUnitPos.pos_Boss]; } break;
            case BattleUnitPos.pos6: { _resSits[0] = SituationDic[BattleUnitPos.pos5]; _resSits[1] = SituationDic[BattleUnitPos.pos8]; _resSits[2] = null; } break;
            case BattleUnitPos.pos7: { _resSits[0] = SituationDic[BattleUnitPos.pos5]; _resSits[1] = SituationDic[BattleUnitPos.pos8]; _resSits[2] = null; } break;
            case BattleUnitPos.pos8: { _resSits[0] = SituationDic[BattleUnitPos.pos6]; _resSits[1] = SituationDic[BattleUnitPos.pos7]; _resSits[2] = null; } break;
        }
        return _resSits;
    }

    public BattleUnit GetConfront(BattleUnitPos a)
    {
        BattleUnit _resSits=null;

        switch (a)
        {
            case BattleUnitPos.pos1: {_resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos2: {_resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos3: {_resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos4: {_resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos5: { _resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos6: {_resSits=SituationDic[BattleUnitPos.pos1]; } break;
            case BattleUnitPos.pos7: { _resSits=SituationDic[BattleUnitPos.pos1];} break;
            case BattleUnitPos.pos8: { _resSits=SituationDic[BattleUnitPos.pos1]; } break;
        }
        return _resSits;
    }


}