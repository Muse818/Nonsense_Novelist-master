using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【高频技能】：乘胜追击
/// </summary>
public class ChengShengZhuiJi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "眉飞色舞";
        res_name = "meifeisewu";
        info = "角色每释放4个动词，下次动词追击一次相同技能";
        lables = new List<string> { "高频"};
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
