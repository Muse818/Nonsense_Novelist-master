using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：化学极乐
/// </summary>
public class HuaXueJiLe : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "化学极乐";
        res_name = "huaxuejile";
        info = "我方“改造”减少的意志会转化为精神";
        lables = new List<string> { "改造" };
        hasAdd = false;

    }
    public override void Init()
    {
        if (hasAdd) return;
        

        hasAdd = true;
    }
    void Effect(AbstractCharacter ac)
    {
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return;
    }
}
