using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：要害侦测
/// </summary>
public class YaoHaiZhenCe : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "要害侦测";
        res_name = "yaohaizhence";
        info = "狄卡德每次攻击的攻击特效，有30%概率翻倍，5%概率变为4倍";
        lables = new List<string> { "角色", "攻击特效" };
        hasAdd = false;
        
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<DiKaDe>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
