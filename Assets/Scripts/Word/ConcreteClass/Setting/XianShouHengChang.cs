using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：仙寿恒昌
/// </summary>
public class XianShouHengChang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "仙寿恒昌";
        res_name = "xianshouhengchang";
        info = "通灵宝玉将50%的伤害转移给友方";
        lables = new List<string> {  };
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
