using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【通用】：手术准备
/// </summary>
public class ShouShuZhunBei : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "手术准备";
        res_name = "shoushuzhunbei";
        info = "角色每2次释放动词，都会让下次攻击变成真实伤害";
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
