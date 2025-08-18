using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：动物专家
/// </summary>
public class DongWuZhuanJia : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "动物专家";
        res_name = "dongwuzhuanjia";
        info = "饲养员以普通攻击治疗时，随从回血效果翻倍";
        lables = new List<string> { "角色", "随从" };
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

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
