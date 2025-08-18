
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugAbility : BattleDebugModule
{
    public override string ModuleName => "技能调试器";
    public override string ToolTip => "";
    
    protected int mPickIdx = 0;

    protected Dictionary<int, string> mItems = new Dictionary<int, string>();

    List<AbilityBase> mRemoveAbilities = new List<AbilityBase>();

    public override void OnRegistered()
    {
        foreach (var it in AbilityTable.DataList)
        {
            mItems.Add(it.Key, $"{it.Key}_{it.Value.mName}");
        }
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        if (context.mPickedUnit == null)
            return;

        var unit = context.mPickedUnit;
        
        ImGui.Text($"单位Kind: {unit.UnitInstance.mKind}");
        ImGui.Text($"单位名称: {unit.Data.mName}");
        ImGui.Text($"单位ID: {unit.ID}");

        ImGuiFunc.Combo("技能列表", mItems, ref mPickIdx, (int key, string value) => value);
        ImGui.Spacing();
        
        if (mPickIdx > 0 && ImGui.Button("添加技能"))
        {
            unit.AbilityAgent.RegisterAbility(mPickIdx);
        }
        
        DrawAbilityAgent(unit.AbilityAgent);
    }

    public void DrawAbilityAgent(AbilityAgent abiAgent)
    {
        ImGui.Columns(3);
        ImGui.Separator();
        ImGui.Text("操作");ImGui.NextColumn();
        ImGui.Text("技能名");ImGui.NextColumn();
        ImGui.Text("Kind");ImGui.NextColumn();
        ImGui.Separator();
        mRemoveAbilities.Clear();

        int Count = 1;
        
        foreach (var abi in abiAgent.Abilities)
        {
            ImGui.PushID(ModuleImGuiID + Count++);
            
            if (ImGui.Button("移除"))
            {
                mRemoveAbilities.Add(abi);
            }
            ImGui.NextColumn();
            
            ImGui.Text($"{abi.Data.mName}");ImGui.NextColumn();
            ImGui.Text($"{abi.Data.mKind}");ImGui.NextColumn();
            
            ImGui.PopID();
        }

        foreach (var abi in mRemoveAbilities)
        {
            abiAgent.RemoveAbility(abi);
        }
        
        ImGui.Separator();
        ImGui.Columns();
    }
}