using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能类型
/// </summary>
abstract public class AbstractSkillMode : MonoBehaviour
{
    /// <summary>技能类型序号</summary>
    public int skillModeID;
    /// <summary>技能类型名称</summary>
    public string skillModeName;
    /// <summary>目标身份优先级(弃用）</summary>
    //public List<AbstractRole> roleOrder=new List<AbstractRole>();
    /// <summary>目标性格优先级(弃用）</summary>
    //public List<AbstractTrait> traitOrde=new List<AbstractTrait>();

    /// <summary>影响区域（直线、扇形、圆形）</summary>
    public IAttackRange attackRange = new SingleSelector();

    /// <summary>
    /// 再次计算锁定的目标
    /// </summary>
    /// <param name="character">施法者</param>
    /// <returns></returns>
    abstract public AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character);
    /// <summary>
    /// 对目标实际影响
    /// </summary>
    /// <param name="useCharacter"></param>
    /// <param name="value"></param>
    /// <param name="aimCharacter"></param>
    /// <returns>用于漂浮文字</returns>
    abstract public float UseMode(AttackType attackType,float value,AbstractCharacter useCharacter,AbstractCharacter aimCharacter,bool hasFloat,float delay);


    abstract public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character,bool ignoreBoss);
}
