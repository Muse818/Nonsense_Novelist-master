using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：放血疗法
/// </summary>
public class FangXueLiaoFa : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "放血疗法";
        res_name = "fangxueliaofa";
        info = "使敌方角色的某项减益状态达到10层时引爆，消除其身上所有减益状态，每层造成其3%自身生命的伤害";
        lables = new List<string> { "异常" };
        hasAdd = false;
        
    }
    public override void Init()
    {
        if (hasAdd) return;
        

        hasAdd = true;
    }
    void Effect(AbstractCharacter ac)
    {
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return;
    }
}
