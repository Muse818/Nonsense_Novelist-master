using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：饮酒乐甚
/// </summary>
public class YinJiuLeShen : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "饮酒乐甚";
        res_name = "yinjiuleshen";
        info = "我方精神超过50的角色，自身和随从的攻击额外造成5精神伤害";
        lables = new List<string> { "攻击特效" };
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
