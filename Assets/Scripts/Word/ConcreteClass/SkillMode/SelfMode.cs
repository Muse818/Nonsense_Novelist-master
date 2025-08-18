using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自身（均自己写）
/// </summary>
class SelfMode : AbstractSkillMode
{
    public void Awake()
    {
        skillModeID = 2;
        skillModeName = "自身";
    }


    public override float UseMode(AttackType attackType, float value, AbstractCharacter useCharacter, AbstractCharacter aimCharacter, bool hasFloat, float delay)
    {
        return value;
    }
    public override AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character)
    {
        return new AbstractCharacter[] { character };
    }
    override public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character,bool _ignoreBoss)
    {

        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, _ignoreBoss);
        return a;
    }
}
