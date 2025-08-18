
using ImGuiNET;
using UnityEngine;
using System;

public class DebugUnitInspector : BattleDebugModule
{
    public override string ModuleName => "单位检视器";
    public override string ToolTip => "查看Unit的信息";
    
    public override void OnDrawImGui(BattleDebugContext context)
    {
        int unitNum = 1;
        
        ImGui.PushID(ModuleImGuiID + unitNum * 100);
        DrawUnit(context.mPickedUnit);
        ImGui.PopID();

        if (context.mPickedUnit is { ServantsAgent: { } })
        {
            var servants = context.mPickedUnit.ServantsAgent.Servants;
            if (servants.Count > 0)
            {
                if (ImGui.TreeNode("随从信息"))
                {
                    foreach (var servant in servants)
                    {
                        ImGui.PushID(ModuleImGuiID + (++unitNum) * 100);
                        if (ImGui.TreeNode($"随从"))
                        {
                            DrawUnit(servant);
                            ImGui.TreePop();
                        }
                        ImGui.PopID();
                    }
                    
                    ImGui.TreePop();
                }
            }
        }
    }

    public static void DrawUnit(BattleUnit unit)
    {
        if (unit == null)
            return;

        ImGui.Text($"单位Kind: {unit.UnitInstance.mKind}");
        ImGui.Text($"单位名称: {unit.Data.mName}");
        ImGui.Text($"单位ID: {unit.ID}");

        ImGui.Text($"单位血量:");
        ImGui.SameLine();
        ImGui.ProgressBar(unit.HpPercent, new Vector2(150, 20));
        ImGui.SameLine();
        ImGui.Text($"{unit.Hp}/{unit.MaxHp}");

        if (ImGui.Button("杀死单位"))
        {
            unit.Die(null);
        }

        if (ImGui.BeginTabBar("单位信息"))
        {
            DrawAIInfo(unit);
            DrawUnitAttributes(unit);

            ImGui.EndTabBar();
        }

        
    }

    public static void DrawAIInfo(BattleUnit unit)
    {
        if (ImGui.BeginTabItem("AI"))
        {
            ImGui.Text($"状态：{unit.AIAgent.State.ToString()}");
            ImGui.EndTabItem();
        }
    }

    public static void DrawUnitAttributes(BattleUnit unit)
    {
        if (ImGui.BeginTabItem("属性"))
        {
            if (ImGui.Button("重置基础属性"))
            {
                unit.InitAttributes();
            }
            
            ImGui.Columns(3);
            ImGui.Text("属性名");ImGui.NextColumn();
            ImGui.Text("属性值");ImGui.NextColumn();
            ImGui.Text("修改值");ImGui.NextColumn();

            foreach (AttributeType attyType in Enum.GetValues(typeof(AttributeType)))
            {
                ImGui.PushID(unit.ID * 100 + (int)attyType);
                if (attyType == AttributeType.None)
                    continue;

                ImGui.Text(attyType.ToString());
                ImGui.NextColumn();

                var attr = unit.AttributeSet.GetAttribute(attyType);
                ImGui.Text($"{attr.mValue}");
                ImGui.NextColumn();

                ImGui.InputFloat("", ref attr.mBaseValue);
                ImGui.NextColumn();

                ImGui.PopID();
            }
            
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
}