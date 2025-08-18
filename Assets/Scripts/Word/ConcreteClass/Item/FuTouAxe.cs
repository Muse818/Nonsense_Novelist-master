using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：钝斧头
/// </summary>
class FuTouAxe : AbstractItems
{
    static public string s_description = "<sprite name=\"atk\">+1";
    static public string s_wordName = "钝斧头";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
     public override void Awake()
    {
        useTimes = 6;
        base.Awake();
        itemID = 20;
        wordName = "钝斧头";
       
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"atk\">+1";
 
        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 1;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.atk += 1;

    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.atk -= 1;
    }
}
