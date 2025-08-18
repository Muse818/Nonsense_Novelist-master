using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：好战的
/// </summary>
public class HaoZhan : AbstractAdjectives
{

    static public string s_description = "获得<color=#dd7d0e>火热</color>*2，持续10s";
    static public string s_wordName = "好战的";
    static public int s_rarity = 1;
    public override void Awake()
    {                
        skillEffectsTime = 10;
       
        adjID = 20;
        wordName = "好战的";
        bookName = BookNameEnum.PHXTwist;
        description = "获得<color=#dd7d0e>火热</color>*2，持续10s";

        skillMode = gameObject.AddComponent<SelfMode>();


        rarity = 1;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "WordCollision";
        return _s;
    }



    bool hasOther = false;
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<HuoRe>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<HuoRe>());
        buffs[1].maxTime = skillEffectsTime;
        BasicAbility(aimCharacter);
    }

    float record;
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
   
      
    
    }



    public override void End()
    {
      
        base.End(); Destroy(this);
       
    }

}
