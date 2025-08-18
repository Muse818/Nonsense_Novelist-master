using ImGuiNET;
using UnityEngine;

public class DebugBattleInfo : BattleDebugModule
{
    public override string ModuleName => "战斗信息";
    
    protected int mPickIdx = 0;

    public override void OnRegistered()
    {
        
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        ImGui.BulletText($"Battle State: {context.mBattle.State.ToString()}");
        
        ImGui.BulletText($"Battle Phase: {context.mBattle.BattlePhase.CurrentPhaseType.ToString()}");

        Vector3 position = Input.mousePosition;
        
        ImGui.BulletText($"鼠标屏幕坐标：{position}");
        ImGui.BulletText($"鼠标世界坐标：{Camera.main.ScreenToWorldPoint(position)}");
    }
}