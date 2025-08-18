using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¿Ì÷«
/// </summary>
class Sense : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "¿Ì÷«";
        banRole.Add(gameObject.AddComponent<Beast>());

    }

}
