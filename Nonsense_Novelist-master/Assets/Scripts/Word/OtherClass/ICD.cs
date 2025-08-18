using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有词条的CD
/// </summary>
public interface ICD
{
    /// <summary>
    /// 冷却（外部使用此方法时，将cd重置为0）
    /// </summary>
    /// <returns>是否冷却完毕</returns>
    abstract public bool CalculateCD();
}
