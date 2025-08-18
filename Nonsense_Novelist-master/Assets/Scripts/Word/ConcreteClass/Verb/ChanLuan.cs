using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：产卵
/// </summary>
class ChanLuan : AbstractVerbs
{
    static public string s_description = "使所有队友获得<color=#dd7d0e>虫卵</color>，持续20s";
    static public string s_wordName = "产卵";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();

        skillID = 14;
        wordName = "产卵";
        bookName = BookNameEnum.PHXTwist;
        description = "使所有队友获得<color=#dd7d0e>虫卵</color>，持续20s";

       // nickname.Add( "刺痛");

        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 20;

        rarity = 1;
        needCD=8;
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChongLuan";
        return _s;
    }
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
     
        AbstractCharacter[] aim = skillMode.CalculateAgain(attackDistance, useCharacter);
        foreach (var _a in aim)
        {
            var b = _a.gameObject.AddComponent<ChongLuan>();
            buffs.Add(b);
            b.maxTime = skillEffectsTime;
        }




    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "的心爱之人对其说：“闻君有两意，故来相决绝”，因而悲痛欲绝。";

    }

}
