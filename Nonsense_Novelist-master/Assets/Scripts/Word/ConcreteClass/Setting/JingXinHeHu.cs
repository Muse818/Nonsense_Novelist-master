using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：精心呵护
/// </summary>
public class JingXinHeHu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "精心呵护";
        res_name = "jingxinhehu";
        info = "由贝洛姬·姬妮孵化出的工蚁，生命上限+30";
        lables = new List<string> { "角色" };
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<BeiLuoJi>();
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
