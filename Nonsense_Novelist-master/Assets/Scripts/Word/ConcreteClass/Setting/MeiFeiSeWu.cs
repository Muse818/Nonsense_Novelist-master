using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：眉飞色舞
/// </summary>
public class MeiFeiSeWu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "眉飞色舞";
        res_name = "meifeisewu";
        info = "“亢奋”提供的能量+1";
        lables = new List<string> { "蓄能"};
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
    void Effect(AbstractCharacter ac)
    {
        var _kf= ac.GetComponents<KangFen>();
        foreach(var it in _kf)
        {
            it.nl += 1;
        }

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
