using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：休养生息
/// </summary>
public class XiuYangShengXi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "休养生息";
        res_name = "xiuyangshengxi";
        info = "复活后额外恢复我方角色30%的血量";
        lables = new List<string> { "复活" };
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
