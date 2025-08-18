using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ±ùÀäĞÔ¸ñ
/// </summary>
class ColdInexorability : AbstractTrait
{
    public void Awake()
    {
        traitName = "±ùÀä";
        traitEnum = TraitEnum.ColdInexorability;
        restrainRole.Add(TraitEnum.Mercy);
        restrainRole.Add(TraitEnum.Sentimental);
    }
}
