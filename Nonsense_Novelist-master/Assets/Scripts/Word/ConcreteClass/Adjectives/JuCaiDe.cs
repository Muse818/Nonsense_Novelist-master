using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：聚财的
/// </summary>

public class JuCaiDe : AbstractAdjectives
{
    static public string s_description = "使角色获得的下一个名词翻倍";
    static public string s_wordName = "聚财的";
    static public int s_rarity = 2;


    public override void Awake()
    {

        adjID = 29;
        wordName = "聚财的";
        bookName = BookNameEnum.CrystalEnergy;
        description = "使角色获得的下一个名词翻倍";


        skillEffectsTime = Mathf.Infinity;

        rarity = 2;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChuanBoCollision";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<JuCai>());
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
