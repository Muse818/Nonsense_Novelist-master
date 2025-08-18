using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugEffect : BattleDebugModule
{
    public override string ModuleName => "Buff查看器";
    public override string ToolTip => "";

    public override void OnDrawImGui(BattleDebugContext context)
    {
        if (context.mPickedUnit == null)
            return;

        var effectAgent = context.mPickedUnit.EffectAgent;
        
        ImGui.Columns(4);
        ImGui.Separator();
        ImGui.Text("操作");ImGui.NextColumn();
        ImGui.Text("类型");ImGui.NextColumn();
        ImGui.Text("持续时间");ImGui.NextColumn();
        ImGui.Text("堆叠数量");ImGui.NextColumn();
        ImGui.Separator();

        int Count = 1;
        
        foreach (var effect in effectAgent.Effects)
        {
            if (effect.mExpired)
                continue;
            
            ImGui.PushID(ModuleImGuiID + Count++);
            
            if (ImGui.Button("移除"))
            {
                effect.MarkInvalid();
            }
            ImGui.NextColumn();
            
            ImGui.Text($"{effect.mType.ToString()}");ImGui.NextColumn();

            if (effect.mDurationRule == EffectDurationRule.HasDuration)
            {
                ImGui.Text((effect.mExpiredTime - context.mBattle.Now).ToString("F2"));
            }
            else
            {
                ImGui.Text("Script");
            }
            
            ImGui.NextColumn();
            ImGui.Text($"{effect.mStackCount}");ImGui.NextColumn();
            
            ImGui.PopID();
        }
        
        ImGui.Separator();
        ImGui.Columns();
    }  
}