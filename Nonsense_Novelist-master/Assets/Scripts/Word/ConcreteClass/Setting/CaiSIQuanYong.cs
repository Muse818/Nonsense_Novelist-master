using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：才思泉涌
/// </summary>
public class CaiSIQuanYong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "才思泉涌";
        res_name = "caisiquanyong";
        info = "我方“亢奋”能够给缺少4能量（原本为5）的动词充能";
        lables = new List<string> { "蓄能" };
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
