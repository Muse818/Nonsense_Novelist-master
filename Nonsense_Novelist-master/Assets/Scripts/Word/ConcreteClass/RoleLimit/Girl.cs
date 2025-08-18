using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 女性
/// </summary>
class Girl : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "女性";
        banGender.Add(GenderEnum.boy);
        banGender.Add(GenderEnum.noGender);
    }

}
