using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：落英缤纷的
/// </summary>
public class LuoYingBinFen : AbstractAdjectives
{

    static public string s_description = "获得<color=#dd7d0e>花瓣</color>";
    static public string s_wordName = "落英缤纷的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 23;
        wordName = "落英缤纷的";
        bookName = BookNameEnum.allBooks;
        description = "获得<color=#dd7d0e>花瓣</color>";
        skillMode = gameObject.AddComponent<DamageMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<SanShe>();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "SanShe";
        _s[1] = "HuaBan";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<HuaBan>());
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
