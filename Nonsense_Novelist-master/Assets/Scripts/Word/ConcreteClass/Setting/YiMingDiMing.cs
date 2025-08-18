using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：蚁命抵命
/// </summary>
public class YiMingDiMing : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "蚁命抵命";
        res_name = "yimingdiming";
        info = "当角色倒下时，牺牲3只友方工蚁可以将其复活";
        lables = new List<string> { "牺牲","复活" };
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
