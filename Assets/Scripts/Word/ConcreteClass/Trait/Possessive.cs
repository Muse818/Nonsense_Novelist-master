using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 强欲性格
/// </summary>
class Possessive: AbstractTrait
{
    public void Awake()
    {
        traitName = "强欲";
        traitEnum = TraitEnum.Possessive;
        restrainRole.Add(TraitEnum.ColdInexorability);
        restrainRole.Add(TraitEnum.Pride);
    }
}
