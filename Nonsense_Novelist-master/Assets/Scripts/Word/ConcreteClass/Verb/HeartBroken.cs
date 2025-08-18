using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：心碎
/// </summary>
class HeartBroken : AbstractVerbs
{

    static public string s_description = "造成200%<sprite name=\"psy\">的精神伤害";
    static public string s_wordName = "心碎";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();
        skillID = 15;
        wordName = "心碎";
        bookName = BookNameEnum.allBooks;
        description = "造成200%<sprite name=\"psy\">的精神伤害";
        //nickname.Add( "刺痛");

        skillMode = gameObject.AddComponent<DamageMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 1;
        needCD=2;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
       
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }
    public override void BasicAbility(AbstractCharacter useCharacter)
    {        
        //奶妈
        if (useCharacter.isNaiMa)
        {
            var _aims = skillMode.CalculateAgain(200, useCharacter);
            int x = 0;
            for (int i = 0; (i < _aims.Length) && (x < useCharacter.myState.aimCount); i++)
            {
                skillMode.UseMode(AttackType.psy, 2 * useCharacter.psy * useCharacter.psyMul, useCharacter, _aims[i], true, 0);
                x++;
            }

            return;
        }
        //造成200%的精神伤害
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            skillMode.UseMode(AttackType.psy, 2 * useCharacter.psy * useCharacter.psyMul, useCharacter, character.myState.aim[i], true, 0);
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
