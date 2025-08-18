using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：群情激奋
/// </summary>
public class QunQingJIFen : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "群情激奋";
        res_name = "qunqingjifen";
        info = "我方角色倒下时，所有友方随从全属性+5";
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
