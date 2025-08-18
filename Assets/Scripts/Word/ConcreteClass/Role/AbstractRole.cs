using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象身份
/// </summary>
abstract public class AbstractRole : MonoBehaviour
{
    /// <summary>身份序号 </summary>
    public int roleID;
    /// <summary>身份名称 </summary>
    public string roleName;
    /// <summary>身份描述 </summary>
    public string description;
    /// <summary>血量成长 </summary>
    public int growHP;
    /// <summary>攻击成长 </summary>
    public float growATK;
    /// <summary>防御成长 </summary>
    public float growDEF;
    /// <summary>受克制的身份序号，克制强度（小数） </summary>
    public Dictionary<int, float> restrainRole=new Dictionary<int, float>();

}
