using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//攻击速度+10%

/// <summary>
/// buff：火热
/// </summary>
public class HuoRe : AbstractBuff
{
    static public string s_description = "攻击速度+10%";
    static public string s_wordName = "火热";


    override protected void Awake()
    {

        buffName = "火热";
        description = "攻击速度+10%";
        book = BookNameEnum.HongLouMeng;

        base.Awake();
        if (TryGetComponent < AbstractCharacter>(out var _c))
        {
            _c.attackSpeedPlus += 0.1f;
        }
    }


    private void OnDestroy()
    {
        if (TryGetComponent<AbstractCharacter>(out var _c))
        {
            _c.attackSpeedPlus -= 0.1f;
        }
        base.OnDestroy();
     
    }
}
