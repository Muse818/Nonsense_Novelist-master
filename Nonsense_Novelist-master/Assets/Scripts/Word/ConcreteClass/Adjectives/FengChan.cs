using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：丰产的
/// </summary>
public class FengChan : AbstractAdjectives
{
    static public string s_description = " < sprite name=\"hpmax\">+20";
    static public string s_wordName = "丰产的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        
        adjID = 8;
        wordName = "丰产的";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"hpmax\">+20";
        skillMode = gameObject.AddComponent<CureMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;
        
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            wordCollisionShoots[0] = gameObject.AddComponent<SanShe>();

        }
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "SanShe";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
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
