using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：野生的
/// </summary>
public class YeSheng : AbstractAdjectives
{
    static public string s_description = "召唤一个随从";
    static public string s_wordName = "野生的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 27;
        wordName = "野生的";
        bookName = BookNameEnum.allBooks;
        description = "召唤一个随从";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;


        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        this.GetComponent<AbstractCharacter>().AddRandomServant();
    }

    

    public override void End()
    {
        base.End();
       
    }

}
