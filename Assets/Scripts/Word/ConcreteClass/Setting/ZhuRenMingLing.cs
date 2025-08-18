using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：主人命令
/// </summary>
public class ZhuRenMingLing : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "主人命令";
        res_name = "zhurenmingling";
        info = "敌方被情迷的角色，攻击有30%概率造成5s随机负面状态";
        lables = new List<string> { "异常" , "情迷" };
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
