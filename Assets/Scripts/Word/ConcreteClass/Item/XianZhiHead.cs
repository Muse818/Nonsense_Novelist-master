using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：先知的头颅
/// </summary>
class XianZhiHead : AbstractItems
{
    static public string s_description = "<sprite name=\"psy\">+30%，<sprite name=\"san\">-30%，受到的控制时间 - 1s";
    static public string s_wordName = "先知的头颅";
    static public int s_rarity = 3;
    static public int s_useTimes = 2;
    public override void Awake()
    {
        base.Awake();
        itemID = 8;
        wordName = "先知的头颅";
        bookName = BookNameEnum.Salome;
        description = "<sprite name=\"psy\">+30%，<sprite name=\"san\">-30%，受到的控制时间 - 1s";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 3;
        useTimes = 2;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.psyMul += 0.3f;
        chara.sanMul -= 0.3f;
    }

    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
        aim.psyMul -= 0.3f;
        aim.sanMul += 0.3f;
    }
}
