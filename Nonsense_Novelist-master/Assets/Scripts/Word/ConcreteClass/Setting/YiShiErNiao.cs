using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：一石二鸟
/// </summary>
public class YiShiErNiao : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "一石二鸟";
        res_name = "yishierniao";
        info = "递进每次撞击墙体，会视作撞击2次";
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
