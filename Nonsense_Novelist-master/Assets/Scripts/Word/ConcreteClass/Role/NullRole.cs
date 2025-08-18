using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 无身份
/// </summary>
class NullRole : AbstractRole
{
    public void Awake()
    {
        roleID = 0;
        roleName = "无身份";
        description = "难以定义的身份";
        growHP = 5;
        growATK = 1.2f;
        growDEF = 1;
    }
}
