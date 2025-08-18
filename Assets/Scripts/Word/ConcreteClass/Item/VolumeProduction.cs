using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 量产改装件
/// </summary>
class VolumeProduction : AbstractItems
{
    static public string s_description = "获得<color=#dd7d0e>改造</color>";
    static public string s_wordName = "量产改装件";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 14;
        wordName = "量产改装件";
        bookName = BookNameEnum.ElectronicGoal;
        description = "获得<color=#dd7d0e>改造</color>";
        holdEnum = HoldEnum.handSingle;
        VoiceEnum = MaterialVoiceEnum.Ceram;
        useTimes = 6;
        rarity = 1;
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<SanShe>();

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "SanShe";
        _s[1] = "GaiZao";
        return _s;
    }
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        buffs.Add(gameObject.AddComponent<GaiZao>());
        buffs[0].maxTime = Mathf.Infinity;
    }

    public override void UseVerb()
    {
        base.UseVerb();
   
    }

    public override void End()
    {
        base.End();
    }
}
