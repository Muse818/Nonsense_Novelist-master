using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词：外骨骼
/// </summary>
class WaiGuGe : AbstractItems
{
    static public string s_description = "自身与随从的<sprite name=\"def\">+5";
    static public string s_wordName = "外骨骼";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 18;
        wordName = "外骨骼";

        bookName = BookNameEnum.PHXTwist;
        description = "自身与随从的<sprite name=\"def\">+5";
        useTimes = 6;
        VoiceEnum = MaterialVoiceEnum.Meat;

        rarity = 1;
    }
    GameObject[] servants;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.def += 5;
        servants = chara.servants.ToArray();
        foreach (var _s in servants)
        {
            if (_s != null)
                _s.GetComponent<AbstractCharacter>().def += 5;
        }
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.def -= 5; 
        foreach (var _s in servants)
        {
            if(_s!=null)
            _s.GetComponent<AbstractCharacter>().def -= 5;
        }
    }
}
