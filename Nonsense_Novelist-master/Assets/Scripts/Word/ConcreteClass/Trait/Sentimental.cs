using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敏感性格
/// </summary>
class Sentimental :AbstractTrait
{
    public void Awake()
    {
        traitName = "敏感";
        traitEnum=TraitEnum.Sentimental;
        restrainRole.Add(TraitEnum.Persistent);
        restrainRole.Add(TraitEnum.Pride);
    }
}
