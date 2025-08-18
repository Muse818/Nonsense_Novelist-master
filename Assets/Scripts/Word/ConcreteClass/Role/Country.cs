using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 乡下人
/// </summary>
class Country : AbstractRole
{
    public void Awake()
    {
        roleID = 3;
        roleName = "乡下人";
        description = "来自农村的人，没怎么见过世面";
        growHP = 6;
        growATK = 1.3f;
        growDEF = 0.7f;
        restrainRole.Add(7, 0.2f);

    }
}
