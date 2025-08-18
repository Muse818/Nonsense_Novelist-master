using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugPinball : BattleDebugModule
{
    public override string ModuleName => "发射器调试";

    public override void OnRegistered()
    {
        
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        var ball = context.mBattle.PinBallLauncher.mNowBall;
        if (ball == null)
        {
            ImGui.BulletText($"小球等待加载中！");
        }
        else
        {
            ImGui.BulletText($"ChargeSec: {context.mBattle.PinBallLauncher.mCurrentChargeTime}");
            ImGui.BulletText($"Angle: {ball.mBall.mShootAngle}");
            ImGui.BulletText($"Speed: {ball.mBall.mVelocity} / {ball.mBall.mVelocity.magnitude}");
        }
        
        ImGui.Columns(4);
        ImGui.Separator();
        ImGui.Text("Index"); ImGui.NextColumn();
        ImGui.Text("State"); ImGui.NextColumn();
        ImGui.Text("RemainSec"); ImGui.NextColumn();
        ImGui.Text("Next"); ImGui.NextColumn();
        ImGui.Separator();

        int index = 0;
        foreach (var slot in context.mBattle.CardDeckManager.LoadSlots)
        {
            ImGui.Text($"{index++}"); ImGui.NextColumn();
            ImGui.Text($"{slot.mState.ToString()}"); ImGui.NextColumn();
            ImGui.Text($"{slot.mLoadTimer}"); ImGui.NextColumn();
            ImGui.Text($"{slot.mNext}"); ImGui.NextColumn();

            ImGui.PopID();
        }
    }
}