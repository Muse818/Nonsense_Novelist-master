using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 影响区域（直线、扇形、圆形）
/// </summary>
public interface  IAttackRange
{
    /// <summary>
    /// 计算影响区域内目标
    /// </summary>
    /// <param name="attackDistance">射程</param>
    /// <param name="ownTrans">使用者站位</param>
    /// <param name="ownTrans">所需阵营</param>
    /// <returns>返回区域内目标数组</returns>
    abstract public AbstractCharacter[] CaculateRange(int attackDistance,Situation situation,NeedCampEnum needCamp, bool isJiangXu);
    abstract public AbstractCharacter[] CaculateRange(int attackDistance, Situation situation, bool _ignoreBoss);
}
