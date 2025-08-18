using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///形容词：中毒的
/// </summary>
public class ZhongDu : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>中毒</color>，<color=#dd7d0e>颠倒</color>，持续10s";
    static public string s_wordName = "中毒的";
    static public int s_rarity = 3;

    public override void Awake()
    {
        adjID = 26;
        wordName = "中毒的";
        bookName = BookNameEnum.allBooks;
        description = "<color=#dd7d0e>中毒</color>，<color=#dd7d0e>颠倒</color>，持续10s";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity =3;
        base.Awake();
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "Toxic";
        _s[1] = "DianDao";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<Toxic>());
        buffs.Add(aimCharacter.gameObject.AddComponent<DianDao>());
        foreach(AbstractBuff buff in buffs)
        {
            buff.maxTime = skillEffectsTime;
        }
        
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
        
    }

}
