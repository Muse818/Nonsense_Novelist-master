using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：被植入的记忆
/// </summary>
class BeiZhiRuDeJiYi : AbstractItems
{
    static public string s_description = "<sprite name=\"san\">-5,获得<color=#dd7d0e>改造</color>*2";
    static public string s_wordName = "被植入的记忆";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 14;
        wordName = "被植入的记忆";
        bookName = BookNameEnum.ElectronicGoal;
        description = "<sprite name=\"san\">-5,获得<color=#dd7d0e>改造</color>*2";
        VoiceEnum = MaterialVoiceEnum.Meat;
        useTimes = 6;
        rarity = 1;


        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "GaiZao";
        _s[1] = "XuWu_YunSu";
        return _s;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);

        chara.san -= 5;

        for (int x = 0; x < 2; x++)
        {
            buffs.Add(gameObject.AddComponent<GaiZao>());
            buffs[0].maxTime = Mathf.Infinity;
        }
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.san += 5;

    }
}
