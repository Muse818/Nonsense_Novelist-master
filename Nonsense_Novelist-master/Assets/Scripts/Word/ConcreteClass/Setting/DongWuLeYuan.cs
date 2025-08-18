using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：动物乐园
/// </summary>
public class DongWuLeYuan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "动物乐园";
        res_name = "dongwuleyuan";
        info = "我方随从的生命上限+100%，四维+50%，混养笼翻倍";
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
