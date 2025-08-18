using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：瘟疫传播
/// </summary>
class WenYiChuanBo : AbstractVerbs
{
    static public string s_description = "传播<color=#dd7d0e>疾病</color>，持续5s";
    static public string s_wordName = "瘟疫传播";
    static public int s_rarity = 3;
    public override void Awake()
    {
        base.Awake();
        skillID = 12;
        wordName = "瘟疫传播";
        bookName = BookNameEnum.FluStudy;
        description = "传播<color=#dd7d0e>疾病</color>，持续5s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = true;
        skillMode.attackRange =  new SingleSelector();

        skillEffectsTime = 5;
        rarity = 3;
        needCD = 2;


    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "Ill";
        _s[1] = "ChuanBo";
        return _s;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        AbstractCharacter center = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        //中心得到传播
        buffs.Add(center.gameObject.AddComponent<ChuanBo>());
        //相邻得到患病
        AbstractCharacter[] neighbors = (buffs[0] as ChuanBo).GetNeighbor(center);
        foreach (AbstractCharacter n in neighbors)
        {
            buffs.Add(n.gameObject.AddComponent<Ill>());
        }
        //中心得到患病
        buffs.Add(center.gameObject.AddComponent<Ill>());

        foreach (AbstractBuff buff in buffs)
        {
            buff.maxTime = skillEffectsTime;
        }
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
