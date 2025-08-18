using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：喧哗
/// </summary>
public class XuanHua : AbstractVerbs
{
    static public string s_description = "使自己获得<color=#dd7d0e>嘲讽</color>，持续5s";
    static public string s_wordName = "喧哗";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();
        skillID = 20;
        wordName = "喧哗";
        bookName = BookNameEnum.allBooks;
        description = "使自己获得<color=#dd7d0e>嘲讽</color>，持续5s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange = new SingleSelector();

        skillEffectsTime = 5;
        rarity = 1;
        needCD = 3;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChaoFeng";
        return _s;
    }

 
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);

        buffs.Add(useCharacter.gameObject.AddComponent<ChaoFeng>());
        buffs[0].maxTime = skillEffectsTime;




    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
        //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
