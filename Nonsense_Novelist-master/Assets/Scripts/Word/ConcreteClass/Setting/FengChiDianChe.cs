using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：风驰电掣
/// </summary>
public class FengChiDianChe : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "风驰电掣";
        res_name = "fengchidianche";
        info = "我方角色每次触发追击，获得5s“急速”";
        lables = new List<string> { "高频" };
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
