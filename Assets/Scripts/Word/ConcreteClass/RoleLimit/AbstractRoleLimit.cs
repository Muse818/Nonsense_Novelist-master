using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 身份限制类
/// </summary>
public class AbstractRoleLimit : MonoBehaviour
{
    /// <summary>
    /// 名称
    /// </summary>
    public string thisName;
    /// <summary>
    /// 禁止的身份
    /// </summary>
    public List<AbstractRole> banRole=new List<AbstractRole>();
    /// <summary>
    /// 禁止的性别
    /// </summary>
    public List<GenderEnum> banGender=new List<GenderEnum>();
}
