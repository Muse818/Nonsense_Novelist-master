using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：吾之爱犬
/// </summary>
public class WuZhiAiQuan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "吾之爱犬";
        res_name = "wuzhiaiquan";
        info = "敌方被情迷的角色攻击速度+50%，且无视“沮丧”";
        lables = new List<string> { "情迷" };
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
