using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ´È°®ÐÔ¸ñ
/// </summary>
class Mercy :AbstractTrait
{
    public void Awake()
    {
        traitName = "´È°®";
        traitEnum=TraitEnum.Mercy;
        restrainRole.Add(TraitEnum.Spicy);
        restrainRole.Add(TraitEnum.ColdInexorability);
    }
}
