using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：聚灵奇术
/// </summary>
public class JuLingQiShu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "聚灵奇术";
        res_name = "julingqishu";
        info = "木乃伊每次复活，攻击+3%，防御+6%";
        lables = new List<string> { "角色", "复活" };

        hasAdd = false;



    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<MuNaiYi>();
        if (chara != null)
        {
        // 每次复活攻击 + 3%防御 + 6%”
            var _list =chara.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
            foreach (var _t in _list)
            {
                if (_t.GetType() == typeof(AI.NoHealthTrigger))
                { (_t as AI.NoHealthTrigger).event_relife += Effect;  }

            }
        }
        hasAdd = true;
    }
    void Effect(AbstractCharacter ac)
    {
        chara.atkMul += 0.03f;
        chara.defMul += 0.06f;

    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            var _list = chara.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
            foreach (var _t in _list)
            {
                if (_t.GetType() == typeof(AI.NoHealthTrigger))
                { (_t as AI.NoHealthTrigger).event_relife -= Effect; }

            }
        }        
    }
}
