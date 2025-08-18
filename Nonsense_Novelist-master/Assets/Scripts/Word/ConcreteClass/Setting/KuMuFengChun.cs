using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：枯木逢春
/// </summary>
public class KuMuFengChun : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "枯木逢春";
        res_name = "kumufengchun";
        info = "全场角色每成功消除一种负面状态，随机出现10个花瓣";
        lables = new List<string> { "净化" };
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
