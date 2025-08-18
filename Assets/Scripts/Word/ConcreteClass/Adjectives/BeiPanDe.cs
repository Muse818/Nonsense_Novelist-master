using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：背叛的
/// </summary>


public class BeiPanDe : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>疯狂</color>，持续10s";
    static public string s_wordName = "背叛的";
    static public int s_rarity = 1;


    public override void Awake()
    {

        adjID = 28;
        wordName = "背叛的";
        bookName = BookNameEnum.EgyptMyth;
        description = "<color=#dd7d0e>疯狂</color>，持续10s";

     
        skillEffectsTime = 10;

        rarity = 1;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "FengKuang";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<FengKuang>());
        buffs[0].maxTime = skillEffectsTime;

        
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
     

    }



    public override void End()
    {
        base.End();
     
    }

}
