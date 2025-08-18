using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：以讹传讹
/// </summary>
public class YiEChuanE : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "以讹传讹";
        res_name = "yiechuane";
        info = "夸张有30%概率，传播次数+1";
        lables = new List<string> { "弹射" };
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
