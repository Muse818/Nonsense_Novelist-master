using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动词：包扎
/// </summary>
class BaoZa : AbstractVerbs
{
    static public string s_description = "治疗3*<sprite name=\"san\">";
    static public string s_wordName = "包扎";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();

        skillID = 18;
        wordName = "包扎";
        bookName = BookNameEnum.allBooks;
        description = "治疗3*<sprite name=\"san\">";

       // nickname.Add( "刺痛");

        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 1;
        needCD=3;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        //基本效果：恢复300%*意志
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        skillMode.UseMode(AttackType.heal, 3* aim.san*aim.sanMul, useCharacter, aim, true, 0);
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "的心爱之人对其说：“闻君有两意，故来相决绝”，因而悲痛欲绝。";

    }

}
