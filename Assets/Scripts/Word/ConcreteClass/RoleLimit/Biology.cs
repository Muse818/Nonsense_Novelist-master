using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// СЄИв
/// </summary>
class Biology : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "СЄИв";
        banRole.Add(gameObject.AddComponent<Bank>());
    }

}
