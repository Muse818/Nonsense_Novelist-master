using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// 设定：硕鼠硕鼠
/// </summary>
public class ShuoShuShuoShu :AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "硕鼠硕鼠";
        res_name = "shuoshushuoshu";
        info = "老鼠基于双方名词数之差，额外造成差值*5%伤害";
        lables = new List<string> { "角色" };

        hasAdd = false;

      


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<Rat>();
        if (chara != null)
        {
            chara.event_AttackA += Effect;
        }
        hasAdd = true;
    }
    void Effect()
    {
        int chara_num= chara.GetComponents<AbstractItems>().Length;
        foreach(var it in chara.myState.aim)
        {
            int a = math.abs(it.GetComponents<AbstractItems>().Length - chara_num);
            it.BeAttack(AttackType.atk, a*0.05f * chara.atk * chara.atkMul, true, 0, chara);

        }

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AttackA -= Effect;
    }
}
