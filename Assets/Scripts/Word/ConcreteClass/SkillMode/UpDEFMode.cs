using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 防御力提升
/// </summary>
class UpDEFMode : AbstractSkillMode
{
    public void Awake()
    {
        skillModeID = 2;
        skillModeName = "状态提升";
    }
    public override float UseMode(AttackType attackType, float value, AbstractCharacter useCharacter, AbstractCharacter aimCharacter, bool hasFloat, float delay)
    {
        aimCharacter.def += value;
        return value;
    }
    /// <summary>
    /// 再次计算锁定的目标
    /// </summary>
    /// <param name="character">施法者</param>
    /// <returns></returns>
    override public AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character)
    {
       
        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, NeedCampEnum.friend, false);
        CollectionHelper.OrderByDescending(a, p => p.allValue);
        return a;
    }
    override public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character, bool _ignoreBoss)
    {

        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, _ignoreBoss);
        return a;
    }
}
