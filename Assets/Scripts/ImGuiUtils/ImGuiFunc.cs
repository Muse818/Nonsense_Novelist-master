
using System.Collections.Generic;
using ImGuiNET;

public class ImGuiFunc
{
    public delegate string StringGetter<TK, TV>(TK key, TV value);
    
    public static void Combo<TK, TV>(string label, Dictionary<TK, TV> dataMap, ref TK pickIdx, StringGetter<TK, TV> strGetter)
    {
        dataMap.TryGetValue(pickIdx, out var comboPreviewValue);
        if (ImGui.BeginCombo(label, strGetter(pickIdx, comboPreviewValue)))
        {
            foreach (var it in dataMap)
            {
                bool isSelected = pickIdx.Equals(it.Key);
                if (ImGui.Selectable(strGetter(it.Key, it.Value), isSelected))
                    pickIdx = it.Key;
                
                if (isSelected)
                    ImGui.SetItemDefaultFocus();
            }
            ImGui.EndCombo();
        }
    }
}