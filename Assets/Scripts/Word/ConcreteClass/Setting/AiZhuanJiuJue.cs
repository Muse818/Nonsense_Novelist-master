using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：哀转久绝
/// </summary>
public class AiZhuanJiuJue : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "哀转久绝";
        res_name = "aizhuanjiujue";
        info = "我方角色倒下时，随机2名敌方角色获得10s“沮丧”";
        lables = new List<string> { "牺牲" };
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
