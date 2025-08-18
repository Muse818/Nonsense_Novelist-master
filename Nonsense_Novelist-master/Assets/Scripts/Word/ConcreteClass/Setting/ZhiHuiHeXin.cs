using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：智慧核心
/// </summary>
public class ZhiHuiHeXin : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "智慧核心";
        res_name = "zhihuihexin";
        info = "全场每层“卓越性能”使角色攻击+10%";
        lables = new List<string> { "改造" };
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
