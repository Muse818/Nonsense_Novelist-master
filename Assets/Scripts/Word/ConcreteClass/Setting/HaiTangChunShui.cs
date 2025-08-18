using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 设定：海棠春睡
/// </summary>
public class HaiTangChunShui : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "海棠春睡";
        res_name = "haitangchunshui";
        info = "林黛玉每拥有50花瓣，她拥有动词中的一个，减少1能量上限，最少为1";
        lables = new List<string> { "角色", "花瓣" };
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;

         chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {
        

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
