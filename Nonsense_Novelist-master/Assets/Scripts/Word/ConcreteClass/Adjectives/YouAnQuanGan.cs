using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 有安全感的
/// </summary>
public class YouAnQuanGan : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>坚实，再生</color>，持续10s";
    static public string s_wordName = "有安全感的";
    static public int s_rarity = 2;
    public override void Awake()
    {
        adjID = 6;
        wordName = "有安全感的";
        bookName = BookNameEnum.ZooManual;
        description = "<color=#dd7d0e>坚实，再生</color>，持续10s";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity = 2;

        base.Awake();

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "JianShi";
        _s[1] = "ZaiSheng";
        return _s;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        
        buffs.Add(aimCharacter.gameObject.AddComponent<JianShi>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<ZaiSheng>());
        buffs[1].maxTime = skillEffectsTime;
    }

    float time;
    
    protected override void Update()
    {
        base.Update();
      
    }

    public override void End()
    {
        base.End();
        
    }

}
