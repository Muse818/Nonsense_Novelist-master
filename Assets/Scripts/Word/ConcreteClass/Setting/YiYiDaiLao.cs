using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：以逸待劳
/// </summary>
public class YiYiDaiLao : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "以逸待劳";
        res_name = "yiyidailao";
        info = "角色每获取1能量，恢复3%最大生命";
        lables = new List<string> {  };
        hasAdd = false;
      
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull += Effect; //每次获得一个能量点的时候，都执行此函数
        }
        hasAdd = true;
    }
    float record = 0;//临时储存attackAmount系数
    
    //每次获得一个能量点的时候，都执行此函数
    void Effect(AbstractCharacter ac)
    {
        ac.BeCure(0.03f*ac.maxHp, true, 0, ac);

    }

    private void OnDestroy()
    {
        if (!hasAdd) return; 
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull -= Effect;
        }
    }
}
