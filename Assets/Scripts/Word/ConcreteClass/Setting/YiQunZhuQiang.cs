using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：蚁群筑墙
/// </summary>
public class YiQunZhuQiang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "蚁群筑墙";
        res_name = "yiqunzhuqiang";
        info = "随从的生命上限+50%，工蚁的生命上限+100%";
        lables = new List<string> { "随从" };
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
