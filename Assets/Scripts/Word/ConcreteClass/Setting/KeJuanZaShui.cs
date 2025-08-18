using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：苛捐杂税
/// </summary>
public class KeJuanZaShui : AbstractSetting
{

    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.QiaoSi;
        settingName = "苛捐杂税";
        res_name = "kejuanzashui";
        info = "每当敌方释放动词，垄断公司对其造成一次伤害为20%的普通攻击，附带攻击效果";
        lables = new List<string> { "角色"};

        hasAdd = false;

   


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            foreach (var it in CharacterManager.instance.GetEnemy(chara.Camp))//获取敌方所有角色
            {
                it.event_UseVerb += Effect;//每个释放动词的行为加一个效果
            }
        }
        hasAdd = true;
    }
    void Effect(AbstractVerbs buff)
    {
        buff.GetComponent<AbstractCharacter>().BeAttack(AttackType.atk, 0.2f * chara.atk * chara.atkMul, true, 0, chara);
        //由动词获取到角色本身，被攻击
        //攻击效果未写
    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            foreach (var it in CharacterManager.instance.GetEnemy(chara.Camp))
            {
                it.event_UseVerb -= Effect;
            }
        }
    }
}
