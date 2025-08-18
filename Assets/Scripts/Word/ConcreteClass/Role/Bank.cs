using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 银行
/// </summary>
class Bank : AbstractRole
{
    public void Awake()
    {
        roleID = 6;
        roleName = "银行";
        description = "总是伴随着金融和债务问题";
        growHP = 5;
        growATK = 1;
        growDEF = 0.7f;
        restrainRole.Add(3, 0.3f);
    }
}
