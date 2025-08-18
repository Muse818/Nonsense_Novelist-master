using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 回血
/// </summary>
class CureMode : AbstractSkillMode
{
    public void Awake()
    {
        skillModeID = 3;
        skillModeName = "回血";
    }


    public override float UseMode(AttackType attackType, float value, AbstractCharacter useCharacter, AbstractCharacter aimCharacter, bool hasFloat, float delay)
    {
       
        aimCharacter.BeCure(value,hasFloat,delay, useCharacter);
        return value;
    }




    /// <summary>
    /// 再次计算锁定的目标(低血量友方）
    /// </summary>
    /// <param name="character">施法者</param>
    /// <returns></returns>
    override public AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character)
    {
        if (character.hasBetray)
        {
            AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, NeedCampEnum.enemy, false);
            CollectionHelper.OrderBy(a, p => p.hp);
            return a;
        }
        else
        {
             AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, NeedCampEnum.friend, false);
            CollectionHelper.OrderBy(a, p => p.hp); 
            return a;
        }
           
        

       
    }
    override public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character, bool _ignoreBoss)
    {

        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation,_ignoreBoss);
        return a;
    }
}
