using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：茶饭无心的
/// </summary>
public class ChaFanWuXin : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>木讷，沮丧</color>，持续7s";
    static public string s_wordName = "茶饭无心的";
    static public int s_rarity = 1;

    public override void Awake()
    {
        
        adjID = 1;
        wordName = "茶饭无心的";
        bookName = BookNameEnum.HongLouMeng;
        description = "<color=#dd7d0e>木讷，沮丧</color>，持续7s";

        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 7;

        rarity = 1;
        base.Awake();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "Upset";
        _s[1] = "MuNe";
        return _s;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<Upset>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<MuNe>());
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
