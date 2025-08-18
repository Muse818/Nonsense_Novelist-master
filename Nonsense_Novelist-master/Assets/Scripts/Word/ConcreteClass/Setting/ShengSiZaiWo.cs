using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：生死在握
/// </summary>
public class ShengSiZaiWo : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "生死在握";
        res_name = "shengsizaiwo";
        info = "阿努比斯的队友获得与触发复活时，阿努比斯与其都会获得30生命";
        lables = new List<string> { "角色", "复活" };

        hasAdd = false;

    


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<Anubis>();
        if (chara != null)
        {
            foreach (var it in CharacterManager.instance.GetFriend(chara.Camp))//获取友方所有角色（包含自己）
            {
                if (it.name == "阿努比斯") { }
                else
                {
                    //触发复活
                    var _list = it.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;//找到it身上攻击状态的所有触发器
                    foreach (var _t in _list)
                    {
                        if (_t.GetType() == typeof(AI.NoHealthTrigger))//判断触发器类型，复活
                        { (_t as AI.NoHealthTrigger).event_relife += Effect; }//增加一个效果

                    }
                    //获得复活
                    it.event_GetRelife += Effect;
                }
                
            }
        }
        hasAdd = true;
    }
    void Effect(AbstractCharacter c)
    {
        c.BeCure(30, true, 0, c);
        chara.BeCure(30, true, 0, chara); 
    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            foreach (var it in CharacterManager.instance.GetFriend(chara.Camp))//获取友方所有角色（包含自己）
            {
                if (it.name == "阿努比斯") { }
                else
                {
                    //触发复活
                    var _list = it.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;//找到it身上攻击状态的所有触发器
                    foreach (var _t in _list)
                    {
                        if (_t.GetType() == typeof(AI.NoHealthTrigger))//判断触发器类型，复活
                        { (_t as AI.NoHealthTrigger).event_relife -= Effect; }//增加一个效果

                    }
                    it.event_GetRelife -= Effect;
                }

            }
        }
    }
}
