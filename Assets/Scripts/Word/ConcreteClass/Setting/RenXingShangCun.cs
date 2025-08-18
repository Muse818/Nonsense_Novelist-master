using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：人性尚存
/// </summary>
public class RenXingShangCun : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.DuTe;
        settingName = "人性尚存";
        res_name = "renxingshangcun";
        info = "我方角色最少拥有10意志";
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
