using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：人造的
/// </summary>
public class RenZao : AbstractAdjectives
{

    static public string s_description = "<sprite name=\"hpmax\">+20，获得<color=#dd7d0e>改造</color>";
    static public string s_wordName = "人造的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 14;
        wordName = "人造的";
        bookName = BookNameEnum.ElectronicGoal;
        description = "<sprite name=\"hpmax\">+20，获得<color=#dd7d0e>改造</color>";
        skillMode = gameObject.AddComponent<CureMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;
        base.Awake();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "GaiZao";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<GaiZao>());
        buffs[0] .maxTime = skillEffectsTime;
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.maxHp += 20;
        aimCharacter.CreateFloatWord(20, FloatWordColor.healMax, false);
    }

    

    public override void End()
    {
        base.End();
        aim.maxHp -= 20;
    }

}
