using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【高频技能】：酒后余兴
/// </summary>
public class JiuHouYuXing : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "酒后余兴";
        res_name = "jiuhouyuxing";
        info = "角色每释放4个动词，获得7s“诗情”和“锐利”";
        lables = new List<string> { "高频" };
        hasAdd = false;
 
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_UseVerb+= Effect;//每次释放动词，增加效果
            
        }
        hasAdd = true;
    }    
    void Effect(AbstractVerbs _av)
    {
        
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return; 
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_AddVerb += Effect;
        }
    }
}
