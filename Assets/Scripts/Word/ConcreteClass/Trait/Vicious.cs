using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¶ñ¶¾ÐÔ¸ñ
/// </summary>
class Vicious : AbstractTrait
{
    public void Awake()
    {
        traitName = "¶ñ¶¾";
        traitEnum = TraitEnum.Vicious;
        restrainRole.Add(TraitEnum.Sentimental);
        restrainRole.Add(TraitEnum.Mercy);
    }
}
