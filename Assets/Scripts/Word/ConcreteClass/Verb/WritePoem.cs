using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：赋诗
/// </summary>
class WritePoem : AbstractVerbs
{
    static public string s_description = "使友方获得<color=#dd7d0e>诗情</color>和<color=#dd7d0e>亢奋</color>，持续10s";
    static public string s_wordName = "赋诗";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();
        skillID = 1;
        wordName = "赋诗";
        bookName = BookNameEnum.HongLouMeng;
        description = "使友方获得<color=#dd7d0e>诗情</color>和<color=#dd7d0e>亢奋</color>，持续10s";

       // nickname.Add("作诗");
       
        skillMode = gameObject.AddComponent<UpPSYMode>();

        skillEffectsTime = 10;
        rarity = 1;
        needCD=4;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ShiQing";
        _s[1] = "KangFen";
        return _s;
    }
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        var chara = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        base.UseVerb(useCharacter);
        //优先四维之和最高的
        buffs.Add(chara.gameObject.AddComponent<KangFen>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(chara.gameObject.AddComponent<ShiQing>());
        buffs[0].maxTime = skillEffectsTime;
 
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "被身边的美景所震撼，不由得诗性大发，颂唱起了诗歌“登山则情满于山，观海则情溢于海，吟咏之间，吐纳珠玉之声”。";

    }
}
