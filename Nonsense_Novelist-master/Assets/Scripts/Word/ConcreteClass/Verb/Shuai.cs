using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：摔
/// </summary>
class Shuai : AbstractVerbs
{
    static public string s_description = "造成50%<sprite name=\"atk\">的物理伤害，使敌人<color=#dd7d0e>晕眩</color>1s";
    static public string s_wordName = "摔";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();
        skillID = 16;
        wordName = "摔";
        bookName = BookNameEnum.allBooks;

        description = "造成50%<sprite name=\"atk\">的物理伤害，使敌人<color=#dd7d0e>晕眩</color>1s";
        //nickname.Add("砸");
        //nickname.Add("甩");
        //nickname.Add("投掷");

        skillMode = gameObject.AddComponent<DamageMode>();

        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 1f;

        rarity = 1;
        needCD=1;
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "Dizzy";
        return _s;
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
                skillMode.UseMode(AttackType.atk, 0.5f*useCharacter.atk * useCharacter.atkMul, useCharacter, _aims[i], true, 0);

                buffs.Add(_aims[i].gameObject.AddComponent<Dizzy>());
                buffs[0].maxTime = skillEffectsTime;

                x++;
            }

            return;
        }
        //其它
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            skillMode.UseMode(AttackType.atk, 0.5f * useCharacter.atk * useCharacter.atkMul, useCharacter, character.myState.aim[i], true, 0);
            
            buffs.Add(character.myState.aim[i].gameObject.AddComponent<Dizzy>());
            buffs[0].maxTime = skillEffectsTime;
        }
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return  character.wordName + "胡乱捡起东西砸了出去。";

    }
}
