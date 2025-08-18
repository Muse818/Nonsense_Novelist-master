using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：刻板的
/// </summary>
public class KeBan : AbstractAdjectives
{
    static public string s_description = "12s内无法攻击";
    static public string s_wordName = "刻板的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 3;
        wordName = "刻板的";
        bookName = BookNameEnum.ZooManual;
        description = "  12s内无法攻击              ";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 12;
        rarity = 1;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<Upset>());
            buffs[0].maxTime = skillEffectsTime;
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        
    }
    
}
