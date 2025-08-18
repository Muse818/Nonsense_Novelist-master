using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【角色】：食谱优化
/// </summary>
public class ShiPuYouHua : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "食谱优化";
        res_name = "shipuyouhua";
        info = "饲养员普通攻击附带“亢奋”的概率，每次攻击提升20%，成功触发后恢复为20%";
        lables = new List<string> { "角色","蓄能"};
        hasAdd = false;
         
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<SiYangYuan>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }
}
