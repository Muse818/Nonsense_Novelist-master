using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：索多玛之女
/// </summary>
public class SuoDuoMaZhiNv : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "索多玛之女";
        res_name = "suoduomazhinv";
        info = "莎乐美攻击意志低于20的角色，有10%概率造成2s俘获";
        lables = new List<string> { "角色", "俘获" };
        hasAdd = false;
       
    }

    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<ShaLeMei>();
        if (chara != null)
        {
            chara.event_AttackA += Effect;//触发条件
        }
        hasAdd = true;
    }
    void Effect()
    {
        foreach (var it in chara.myState.aim)
        {
            int num = Random.Range(1, 11);
            if (it.san < 20 && num == 1)
            {
                //加buff的基本操作
                var buff= it.gameObject.AddComponent<FuHuo>();
                buff.maxTime = 2;
            }
        }

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AttackA -= Effect;
    }
}
