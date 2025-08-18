using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// °ÁÂıĞÔ¸ñ
/// </summary>
class Pride :AbstractTrait
{
    public void Awake()
    {
        traitName = "°ÁÂı";
        traitEnum=TraitEnum.Pride;
        restrainRole.Add(TraitEnum.Persistent);
        restrainRole.Add(TraitEnum.Spicy);
    }
}
