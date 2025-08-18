
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugSpawnUnit : BattleDebugModule
{
    public override string ModuleName => "单位生成器";
    public override string ToolTip => "在规定区域内生成新单位, 按C建生成";
    
    protected int mPickIdx = 0;

    protected Dictionary<int, string> mItems = new Dictionary<int, string>();

    public override void OnRegistered()
    {
        foreach (var it in BattleUnitTable.DataList)
        {
            if (!it.Value.mForbidden)
            {
                mItems.Add(it.Key, $"{it.Key}_{it.Value.mName}");
            }
        }
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        ImGuiFunc.Combo("UnitList", mItems, ref mPickIdx, (int key, string value) => value);

        if (mPickIdx > 0 && Input.GetKeyDown(KeyCode.C))
        {
            var data = BattleUnitTable.Find(mPickIdx);
            if (data != null)
            {
                UnitSlotType typeFlags = UnitSlotType.None;

                switch (data.mInitType)
                {
                    case BattleUnitType.Character:
                        typeFlags = UnitSlotType.BackSeat | UnitSlotType.FrontSeat;
                        break;
                    
                    case BattleUnitType.Servant:
                        typeFlags = UnitSlotType.Servant;
                        break;
                    
                    case BattleUnitType.Monster:
                        typeFlags = UnitSlotType.Boss;
                        break;
                }
                
                
                context.mBattle.Stage.SpawnUnit(mPickIdx, typeFlags, ClientUtils.GetMouseWorldPosition());
            }
            
        }
    }
}