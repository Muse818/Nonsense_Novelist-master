using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 纨绔
/// </summary>
class Noble : AbstractRole
{
    public void Awake()
    {
        roleID = 1;
        roleName = "纨绔";
        description = "游手好闲的子弟";
        growHP = 3;
        growATK = 0.7f;
        growDEF = 0.4f;
        restrainRole.Add(3, 0.1f);
        restrainRole.Add(6, 0.3f);
    }
}
