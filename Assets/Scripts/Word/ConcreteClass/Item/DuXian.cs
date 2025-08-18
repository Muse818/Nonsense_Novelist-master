using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：毒腺
/// </summary>
class DuXian : AbstractItems
{
    static public string s_description = "自身与随从的<sprite name=\"atk\">+1,附带<color=#dd7d0e>腐蚀</color>";
    static public string s_wordName = "毒腺";
    static public int s_rarity = 3;
    static public int s_useTimes = 2;
    public override void Awake()
    {
        base.Awake();
        itemID = 19;
        wordName = "毒腺";
        bookName = BookNameEnum.PHXTwist;
        useTimes = 2;
        description = "自身与随从的<sprite name=\"atk\">+1,附带<color=#dd7d0e>腐蚀</color>";
        holdEnum = HoldEnum.handSingle;
        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 3;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "FuShi";
        return _s;
    }
    bool hasAdd = false;


    GameObject[] servants;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);

        if (hasAdd) return;

        chara.atk += 5;
        if (chara == null) return;
        chara.event_AttackA += AddToAttackA;

        servants = chara.servants.ToArray();

        foreach (var _s in servants)
        {
            if (_s != null)
            {
                _s.GetComponent<AbstractCharacter>().atk += 5;
                _s.GetComponent<AbstractCharacter>().event_AttackA += AddToAttackA;
            }
        }

        hasAdd = true;
    }

    public override void UseVerb()
    {
        base.UseVerb();
       

    }
    public void AddToAttackA()
    {
        var _ac = GetComponent<AbstractCharacter>();
        //为攻击目标增加Buff
        for (int i = 0; i < _ac.myState.aim.Count; i++)
        {
            var buff = _ac.myState.aim[i].gameObject.AddComponent<FuShi>();
            buffs.Add(buff);
            buff.maxTime = 10;
        }
           
    }
    public override void End()
    {
        base.End();
        if (aim == null) return;

        aim.atk -= 5;
        aim.event_AttackA -= AddToAttackA;
        foreach (var _s in servants)
        {
            if (_s != null)
            {
                _s.GetComponent<AbstractCharacter>().atk -= 5;
                _s.GetComponent<AbstractCharacter>().event_AttackA -= AddToAttackA;
            }
        }
    }
}
