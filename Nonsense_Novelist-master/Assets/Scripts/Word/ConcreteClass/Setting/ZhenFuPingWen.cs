using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：振幅平稳
/// </summary>
public class ZhenFuPingWen : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "振幅平稳";
        res_name = "zhenfupingwen";
        info = "我方每层共振生命上限+20";
        lables = new List<string> {"共振" };
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
