using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：咳嗽的
/// </summary>
public class KeSou : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>患病</color>，持续20s";
    static public string s_wordName = "咳嗽的";
    static public int s_rarity = 2;

    public override void Awake()
    {
        adjID = 15;
        wordName = "咳嗽的";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>患病</color>，持续20s";

        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 20;
        rarity = 2;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "WordCollision";
        _s[1] = "Ill";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);


        buffs.Add(aimCharacter.gameObject.AddComponent<Ill>());
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
