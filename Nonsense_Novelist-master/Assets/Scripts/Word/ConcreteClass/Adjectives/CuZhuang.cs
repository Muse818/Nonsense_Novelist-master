using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：粗壮的
/// </summary>
public class CuZhuang : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"hpmax\">+30";
    static public string s_wordName = "粗壮的";
    static public int s_rarity = 1;

    public override void Awake()
    {
        adjID = 24;
        wordName = "粗壮的";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"hpmax\">+30";
        skillMode = gameObject.AddComponent<CureMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.maxHp += 30;
        aimCharacter.CreateFloatWord(30, FloatWordColor.healMax, false);
    }

    

    public override void End()
    {
        base.End();
        aim.maxHp -= 30;
    }

}
