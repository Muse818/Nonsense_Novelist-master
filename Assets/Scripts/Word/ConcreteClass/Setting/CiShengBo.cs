using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：次声波
/// </summary>
public class CiShengBo : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "次声波";
        res_name = "cishengbo";
        info = "全场共振从4层计算一次，改为3层计算一次";
        lables = new List<string> { "共振" };
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
