using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 宿敌
/// </summary>
class OldEnemy : AbstractRole
{
    public void Awake()
    {
        roleID = 5;
        roleName = "宿敌";
        description = "针锋相对却又总是相遇的对手";
        growHP = 5;
        growATK = 1.6f;
        growDEF = 1;
        restrainRole.Add(1, 0.3f);
    }
}
