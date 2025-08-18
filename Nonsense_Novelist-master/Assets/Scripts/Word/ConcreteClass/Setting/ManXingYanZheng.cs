using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：慢性炎症
/// </summary>
public class ManXingYanZheng : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "慢性炎症";
        res_name = "manxingyanzheng";
        info = "所有敌方角色的减益状态的时间+2s，患病+4s";
        lables = new List<string> { "异常" };
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
