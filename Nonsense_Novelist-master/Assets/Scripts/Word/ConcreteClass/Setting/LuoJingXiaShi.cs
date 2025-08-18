using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【角色】：落井下石
/// </summary>
public class LuoJingXiaShi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "落井下石";
        res_name = "luojingxiashi";
        info = "失恋对获得沮丧的敌人，追击一次“心碎”";
        lables = new List<string> { "角色","高频"};
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
