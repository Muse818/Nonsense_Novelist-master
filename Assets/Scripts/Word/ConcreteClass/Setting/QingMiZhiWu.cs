using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：情迷之雾
/// </summary>
public class QingMiZhiWu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "情迷之雾";
        res_name = "qingmizhiwu";
        info = "敌方所有减益状态的时间+1s，情迷+2s";
        lables = new List<string> { "异常","情迷" };
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
