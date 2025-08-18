using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 坚毅性格
/// </summary>
class Persistent : AbstractTrait
{
    public void Awake()
    {
        traitName = "坚毅";
        traitEnum = TraitEnum.Persistent;
        restrainRole.Add(TraitEnum.Vicious);
        restrainRole.Add(TraitEnum.Possessive);
    }
}
