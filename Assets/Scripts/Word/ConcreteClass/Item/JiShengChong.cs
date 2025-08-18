using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词：寄生虫
/// </summary>
class JiShengChong : AbstractItems
{
    static public string s_description = "<sprite name=\"def\">-3，自身与随从的攻击附带<color=#dd7d0e>患病</color>";
    static public string s_wordName = "寄生虫";
    static public int s_rarity = 2;
    static public int s_useTimes = 4;
    public override void Awake()
    {
        base.Awake();
        itemID = 16;
        wordName = "寄生虫";
        bookName = BookNameEnum.FluStudy;
        description = "<sprite name=\"def\">-3，自身与随从的攻击附带<color=#dd7d0e>患病</color>";
        //自身与随从的攻击附带“患病”，持续6s
        useTimes = 4;
        VoiceEnum = MaterialVoiceEnum.Meat;

        rarity = 2;
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<SanShe>();

    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "SanShe";
        _s[1] = "Ill";
        return _s;
    }

    bool hasAdd = false;
    AbstractCharacter[] _acs;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.def-=3;

        if (hasAdd) return;
        //为自身和所有随从，增加平A附加效果
        _acs = GetComponentsInChildren<AbstractCharacter>();
        foreach (var _ac in _acs)
        {
            _ac.event_AttackA += AddToAttackA;
        }
        hasAdd = true;
    }

    public void AddToAttackA()
    {
        var _ac = GetComponent<AbstractCharacter>();
        //为攻击目标增加Buff
        for (int i = 0; i < _ac. myState.aim.Count; i++)
        {
                var buff = _ac.myState.aim[i].gameObject.AddComponent<Ill>();
        buffs.Add(buff);
        buff.maxTime = 6;
        }
        
    }


    public override void UseVerb()
    {
        base.UseVerb();
        //buffs.Add(gameObject.AddComponent<Ill>());
        //buffs[0].maxTime = 5;
    }

    public override void End()
    {
        base.End();
        foreach (var _ac in _acs)
        {
            _ac.event_AttackA -= AddToAttackA;
        }
        aim.def+=3;
    }
}
