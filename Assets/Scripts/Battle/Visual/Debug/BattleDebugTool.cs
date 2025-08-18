using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class BattleDebugTool : ImGuiObjBase
{
    private bool mStartBattle = false;
    private BattleBase mBattle = null;
    private BattleRunner mRunner = null;

    private GameObject mModuleRoot = null;
    private List<BattleDebugModule> mModules = new List<BattleDebugModule>();
    
    private BattleUnit mPickedUnit = null;
    private int mMoudleID = 0;

    protected override void Start()
    {
        mRunner = BattleRunner.Instance;

        RegisterModules();
    }

    private void RegisterModules()
    {
        mModuleRoot = new GameObject("Modules");
        mModuleRoot.transform.SetParent(transform);
        
        RegisterModule<DebugBattleInfo>();
        RegisterModule<DebugPinball>();
        RegisterModule<DebugSpawnUnit>();
        RegisterModule<DebugUnitInspector>();
        RegisterModule<DebugAbility>();
        RegisterModule<DebugEffect>();
        RegisterModule<DebugWord>();
    }
    
    private void RegisterModule<Ty>() where Ty : BattleDebugModule
    {
        Ty module = mModuleRoot.AddComponent<Ty>();
        module.ModuleImGuiID = (++mMoudleID) * 10000;
        module.OnRegistered();
        mModules.Add(module);
    }

    protected override void OnDrawImGui(UImGui.UImGui obj)
    {
        if (!mStartBattle)
        {
            DrawInitiator(obj);
        }
        else
        {
            DrawDebugTool(obj);
        }
    }

    private void DrawInitiator(UImGui.UImGui obj)
    {
        if (!ImGui.Begin("战斗启动器", ImGuiWindowFlags.MenuBar))
            return;

        if (ImGui.Button("Play"))
        {
            mStartBattle = true;
            mBattle = BattleRunner.Battle;
            mBattle.Mode = BattleMode.Normal;
            mRunner.StartBattle();
        }
        
        if (ImGui.Button("TestShoot"))
        {
            mStartBattle = true;
            mBattle = BattleRunner.Battle;
            mBattle.Mode = BattleMode.TestShoot;
            mRunner.StartBattle();
        }
        
        ImGui.End();
    }
    
    private void DrawDebugTool(UImGui.UImGui obj)
    {
        if (!ImGui.Begin("战斗调试器", ImGuiWindowFlags.MenuBar))
            return;

        BattleDebugContext context = new BattleDebugContext
        {
            mBattle = mBattle,
            mImGuiObj = obj,
            mPickedUnit = UpdatePickedUnit(),
        };
        
        if(mPickedUnit != null)
        {
            DrawUtils.DrawRotatedSquare(mPickedUnit.ViewPos,2, Time.time * 45,Color.red);
        }
        
        foreach (var module in mModules)
        {
            ImGui.PushID(module.ModuleImGuiID);

            bool bItemHovered = false;
            
            if (ImGui.CollapsingHeader(module.ModuleName))
            {
                bItemHovered = ImGui.IsItemHovered();
                
                module.OnDrawImGui(context);
            }
            else
            {
                bItemHovered = ImGui.IsItemHovered();
            }

            if (bItemHovered && !module.ToolTip.Equals(""))
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip(module.ToolTip);
                ImGui.EndTooltip();
            }

            ImGui.Spacing();
            ImGui.PopID();
        }
        
        ImGui.End();
    }

    public BattleUnit UpdatePickedUnit()
    {
        ImGui.Text("Alt + 鼠标4号键挑选单位");
        
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButtonDown(5))
        {
            mPickedUnit = BVHelper.GetUnitAtPosition(ClientUtils.GetMouseWorldPosition());
        }

        if (!mPickedUnit.IsValid())
        {
            mPickedUnit = null;
        }
        
        return mPickedUnit;
    }
}