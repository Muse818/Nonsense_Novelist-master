using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：难以消灭的
/// </summary>


public class NanYiXiaoMieDe : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>复活，再生</color>，持续10s";
    static public string s_wordName = "难以消灭的";
    static public int s_rarity = 3;


    public override void Awake()
    {

        adjID = 32;
        wordName = "难以消灭的";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>复活，再生</color>，持续10s";


        skillEffectsTime = 10;

        rarity = 3;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ReLife";
        _s[1] = "ZaiSheng";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<ReLife>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<ZaiSheng>());
        buffs[1].maxTime = skillEffectsTime;

    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {


    }



    public override void End()
    {
        base.End();

    }

}
