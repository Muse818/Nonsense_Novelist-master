using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象性格
/// </summary>
abstract public class AbstractTrait : MonoBehaviour
{
    /// <summary>性格名 </summary>
    public string traitName;
    /// <summary>性格枚举，仅用于克制</summary>
    public TraitEnum traitEnum;
    /// <summary>受克制的性格(额外造成30%伤害)</summary>
    public List<TraitEnum> restrainRole=new List<TraitEnum>();
}

/// <summary>
/// 性格枚举，仅用于克制
/// </summary>
public enum TraitEnum
{
    /// <summary>敏感</summary>
    Sentimental,
    /// <summary>泼辣</summary>
    Spicy,
    /// <summary>冰冷</summary>
    ColdInexorability,
    /// <summary>恶毒</summary>
    Vicious,
    /// <summary>强欲</summary>
    Possessive,
    /// <summary>坚毅</summary>
    Persistent,
    /// <summary>慈爱</summary>
    Mercy,
    /// <summary>傲慢</summary>
    Pride,
}

