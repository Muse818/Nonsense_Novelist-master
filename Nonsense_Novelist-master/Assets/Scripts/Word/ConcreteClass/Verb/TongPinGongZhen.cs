using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：同频共振
/// </summary>
class TongPinGongZhen: AbstractVerbs
{
    static public string s_description = "使2个友方获得<color=#dd7d0e>共振</color>10s，并消除其共振层数的负面状态";
    static public string s_wordName = "同频共振";
    static public int s_rarity = 3;
    public override void Awake()
    {
        base.Awake();
        skillID = 9;
        wordName = "同频共振";
        bookName = BookNameEnum.CrystalEnergy;
        description = "使2个友方获得<color=#dd7d0e>共振</color>10s，并消除其共振层数的负面状态";

        skillMode = gameObject.AddComponent<UpATKMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 10;

        rarity = 3;
        needCD=2;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "GongZhen";
        return _s;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);


        AbstractCharacter[] a = skillMode.CalculateAgain(attackDistance, useCharacter);

        buffs.Add(a[0].gameObject.AddComponent<GongZhen>());
        buffs[0].maxTime = skillEffectsTime;

        int count0 = a[0].GetComponents<GongZhen>().Length;
        a[0].DeleteBadBuff(count0);


        if (a[1] != null)
        {
            buffs.Add(a[1].gameObject.AddComponent<GongZhen>());
            buffs[0].maxTime = skillEffectsTime;
        }
        count0 = a[1].GetComponents<GongZhen>().Length;
        a[1].DeleteBadBuff(count0);

    }


    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "用手在水晶上以慢速滑动，手掌与水晶的摩擦产生了一种具有规律的振动，"+character.wordName+"通过控制这股振动慢慢地积蓄力量，最终让整个山体都颤动了起来。";

    }

}
