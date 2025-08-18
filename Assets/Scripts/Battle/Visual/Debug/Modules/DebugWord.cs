
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugWord : BattleDebugModule
{
    public override string ModuleName => "词条调试器";
    public override string ToolTip => "";

    protected float mNow = 0;
    protected int mPickIdx = 0;

    protected Dictionary<int, string> mItems = new Dictionary<int, string>();

    List<WordEntry> mRemoveWords = new List<WordEntry>();
    
    public override void OnRegistered()
    {
        foreach (var it in WordTable.DataList)
        {
            mItems.Add(it.Key, $"{it.Key}_{it.Value.mType.ToString()}_{it.Value.mName}");
        }
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        if (context.mPickedUnit == null)
            return;

        var unit = context.mPickedUnit;
        mNow = context.mBattle.Now;
        
        ImGui.Text($"单位Kind: {unit.UnitInstance.mKind}");
        ImGui.Text($"单位名称: {unit.Data.mName}");
        ImGui.Text($"单位ID: {unit.ID}");

        ImGuiFunc.Combo("技能列表", mItems, ref mPickIdx, (int key, string value) => value);
        ImGui.Spacing();
        
        if (mPickIdx > 0 && ImGui.Button("添加词条"))
        {
            unit.WordComponent.AddWord(mPickIdx);
        }
        
        DrawWordAgent(unit.WordComponent);
    }
    
    public void DrawWordAgent(WordComponent wordAgent)
    {
        if (ImGui.BeginTabBar("词条信息"))
        {
            mRemoveWords.Clear();
            
            DrawProperty(wordAgent.GetWordsByType(WordType.Property));
            DrawVerb(wordAgent.GetWordsByType(WordType.Verb));
            DrawAdj(wordAgent.GetWordsByType(WordType.Adjective));
            DrawNoun(wordAgent.GetWordsByType(WordType.Noun));
            
            foreach (var w in mRemoveWords)
            {
                wordAgent.RemoveWord(w);
            }
            
            ImGui.EndTabBar();
        }
        

    }
    
    public void DrawProperty(List<WordEntry> words)
    {
        if (ImGui.BeginTabItem("特性"))
        {
            ImGui.Columns(4);
            ImGui.Separator();
            ImGui.Text("操作"); ImGui.NextColumn();
            ImGui.Text("Kind"); ImGui.NextColumn();
            ImGui.Text("词条名"); ImGui.NextColumn();
            ImGui.Text("书名"); ImGui.NextColumn();
            ImGui.Separator();

            int count = 1;

            foreach (var w in words)
            {
                ImGui.PushID(ModuleImGuiID + (count++) * 1000);

                if (ImGui.Button("移除"))
                {
                    mRemoveWords.Add(w);
                }

                ImGui.NextColumn();

                ImGui.Text($"{w.mData.mKind}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mName}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mBook}"); ImGui.NextColumn();

                ImGui.PopID();
            }

            ImGui.Separator();
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
    
    public void DrawVerb(List<WordEntry> words)
    {
        if (ImGui.BeginTabItem("动词"))
        {
            ImGui.Columns(5);
            ImGui.Separator();
            ImGui.Text("操作"); ImGui.NextColumn();
            ImGui.Text("Kind"); ImGui.NextColumn();
            ImGui.Text("词条名"); ImGui.NextColumn();
            ImGui.Text("书名"); ImGui.NextColumn();
            ImGui.Text("能量条"); ImGui.NextColumn();
            ImGui.Separator();

            int count = 1;

            foreach (var w in words)
            {
                ImGui.PushID(ModuleImGuiID + (count++) * 2000);

                if (ImGui.Button("移除"))
                {
                    mRemoveWords.Add(w);
                }

                ImGui.NextColumn();

                ImGui.Text($"{w.mData.mKind}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mName}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mBook}"); ImGui.NextColumn();
                ImGui.Text($"{w.mPower}/{w.mData.mTriggerPower}"); ImGui.NextColumn();

                ImGui.PopID();
            }

            ImGui.Separator();
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
    
    public void DrawAdj(List<WordEntry> words)
    {
        if (ImGui.BeginTabItem("形容词"))
        {
            ImGui.Columns(5);
            ImGui.Separator();
            ImGui.Text("操作"); ImGui.NextColumn();
            ImGui.Text("Kind"); ImGui.NextColumn();
            ImGui.Text("词条名"); ImGui.NextColumn();
            ImGui.Text("书名"); ImGui.NextColumn();
            ImGui.Text("剩余时间"); ImGui.NextColumn();
            ImGui.Separator();

            int count = 1;

            foreach (var w in words)
            {
                ImGui.PushID(ModuleImGuiID + (count++) * 3000);

                if (ImGui.Button("移除"))
                {
                    mRemoveWords.Add(w);
                }

                ImGui.NextColumn();

                ImGui.Text($"{w.mData.mKind}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mName}"); ImGui.NextColumn();
                ImGui.Text($"{w.mData.mBook}"); ImGui.NextColumn();
                ImGui.Text($"{(w.mEndTime - mNow):F2}"); ImGui.NextColumn();

                ImGui.PopID();
            }

            ImGui.Separator();
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
    
    public void DrawNoun(List<WordEntry> words)
    {
        if (ImGui.BeginTabItem("名词"))
        {
            Dictionary<WordTable.Data, int> counter = new Dictionary<WordTable.Data, int>();

            foreach (var w in words)
            {
                if (counter.ContainsKey(w.mData))
                {
                    ++counter[w.mData];
                }
                else
                {
                    counter.Add(w.mData, 1);
                }
            }

            ImGui.Columns(4);
            ImGui.Separator();
            ImGui.Text("Kind"); ImGui.NextColumn();
            ImGui.Text("词条名"); ImGui.NextColumn();
            ImGui.Text("书名"); ImGui.NextColumn();
            ImGui.Text("数量"); ImGui.NextColumn();
            ImGui.Separator();

            int count = 1;

            foreach (var w in counter)
            {
                ImGui.PushID(ModuleImGuiID + (count++) * 4000);

                ImGui.Text($"{w.Key.mKind}"); ImGui.NextColumn();
                ImGui.Text($"{w.Key.mName}"); ImGui.NextColumn();
                ImGui.Text($"{w.Key.mBook}"); ImGui.NextColumn();
                ImGui.Text($"{w.Value}"); ImGui.NextColumn();

                ImGui.PopID();
            }

            ImGui.Separator();
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
}