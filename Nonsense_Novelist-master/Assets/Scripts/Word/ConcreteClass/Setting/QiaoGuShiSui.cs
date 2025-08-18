using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【角色】：敲骨食髓
/// </summary>
public class QiaoGuShiSui : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "敲骨食髓";
        res_name = "qiaogushisui";
        info = "垄断公司造成技能与攻击特效伤害的15%，恢复自身与随从的生命";
        lables = new List<string> { "角色"};
        hasAdd = false;
        
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LongDuanGongSi>();
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
