using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：能量缓冲
/// </summary>
public class NengLiangHuanChong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "能量缓冲";
        res_name = "nenglianghuanchong";
        info = "角色每拥有1能量点，受到的最终伤害-5%";
        lables = new List<string> { "蓄能" };
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull += Effect; //每次获得一个能量点的时候，都执行此函数
        }
        
        hasAdd = true;
    }

    float record=0;//临时储存attackAmount系数


    //每次获得一个能量点的时候，都执行此函数
    void Effect(AbstractCharacter ac)
    {
        //效果描述：计算角色每增加一个能量点时，当前所有技能的能量点总和，乘以伤害系数
        int count=0;//用于记录当前能量点的数量
        foreach (var _skill in ac.skills)
        {
            count += _skill.CD;//获取所有技能的当前能量点
        }
        //恢复系数
        ac.attackAmount += record;
        //增加系数
        ac.attackAmount -= count * 0.05f;
        //记录系数以便下次恢复
        record = count * 0.03f;
    }

    private void OnDestroy()
    {
        if (!hasAdd) return;
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull -= Effect;
        }
    }
}
