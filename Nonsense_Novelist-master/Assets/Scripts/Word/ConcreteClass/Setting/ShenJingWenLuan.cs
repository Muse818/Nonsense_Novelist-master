using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：神经紊乱
/// </summary>
public class ShenJingWenLuan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.DuTe;
        settingName = "神经紊乱";
        res_name = "shenjingwenluan";
        info = "全场每层“改造”有5%概率让角色不分敌我地随机攻击";
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
