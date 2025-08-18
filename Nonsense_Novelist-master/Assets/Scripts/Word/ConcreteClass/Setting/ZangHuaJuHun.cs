using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：葬花聚魂
/// </summary>
public class ZangHuaJuHun : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "葬花聚魂";
        res_name = "zanghuajuhun";
        info = "当我方角色倒下时，消耗50花瓣使其复活";
        lables = new List<string> { "花瓣","复活" };
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
