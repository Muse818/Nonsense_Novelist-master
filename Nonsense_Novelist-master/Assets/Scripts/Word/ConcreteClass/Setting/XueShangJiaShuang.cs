using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【异常流】：雪上加霜
/// </summary>
public class XueShangJiaShuang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "雪上加霜";
        res_name = "xueshangjiashuang";
        info = "减益状态可叠加更多次";
        lables = new List<string> { "异常"};
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<ShiLian>();
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
