using UnityEngine;
using ImGuiNET;

namespace UImGui
{
    public class UImGuiFont : MonoBehaviour
    {
        public void AddChineseFont(ImGuiIOPtr io)
        {
            // you can put on StreamingAssetsFolder and call from there like:
            //string fontPath = $"{Application.streamingAssetsPath}/NotoSansCJKjp - Medium.otf";
            string fontPath = $"{Application.streamingAssetsPath}/simkai.ttf";
            io.Fonts.AddFontFromFileTTF(fontPath, 16, null, io.Fonts.GetGlyphRangesChineseSimplifiedCommon());

            // you can create a configs and do a lot of stuffs
            //ImFontConfig fontConfig = default;
            //ImFontConfigPtr fontConfigPtr = new ImFontConfigPtr(&fontConfig);
            //fontConfigPtr.MergeMode = true;
            //io.Fonts.AddFontDefault(fontConfigPtr);
            //int[] icons = { 0xf000, 0xf3ff, 0 };
            //fixed (void* iconsPtr = icons)
            //{
            //	io.Fonts.AddFontFromFileTTF("fontawesome-webfont.ttf", 18.0f, fontConfigPtr, (System.IntPtr)iconsPtr);
            //}
        }
    }
}