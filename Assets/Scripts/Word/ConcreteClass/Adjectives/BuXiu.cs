using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 形容词：不朽
/// </summary>

public class BuXiu : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>嘲讽，复活</color>，持续10s";
    static public string s_wordName = "不朽的";
    static public int s_rarity = 2;


    public override void Awake()
    {
        
        adjID = 7;
        wordName = "不朽的";
        bookName = BookNameEnum.EgyptMyth;
        description = "<color=#dd7d0e>嘲讽，复活</color>，持续10s";

        skillMode = gameObject.AddComponent<CureMode>();
        skillEffectsTime = 10;

        rarity = 2;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ReLife";
        _s[1] = "ChaoFeng";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<ReLife>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<ChaoFeng>());
        buffs[1].maxTime = skillEffectsTime;

        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
       
      

    }

    

    public override void End()
    {
        base.End();
       
    }

}
