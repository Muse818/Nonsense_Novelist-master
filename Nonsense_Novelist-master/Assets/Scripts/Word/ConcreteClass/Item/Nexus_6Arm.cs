using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词: Nexus-6型手臂
/// </summary>
class Nexus_6Arm : AbstractItems
{
    static public string s_description = "<sprite name=\"atk\">+5，攻击造成三段伤害，获得<color=#dd7d0e>改造</color>*3";
    static public string s_wordName = "Nexus-6型手臂";
    static public int s_rarity = 4;
    static public int s_useTimes = 1;
    public override void Awake()
    {
        base.Awake();
        itemID = 13;
        wordName = "Nexus-6型手臂";
        bookName = BookNameEnum.ElectronicGoal; 
        description = "<sprite name=\"atk\">+5，攻击造成三段伤害，获得<color=#dd7d0e>改造</color>*3";
        VoiceEnum = MaterialVoiceEnum.Meat;
        useTimes = 1;
        rarity = 4;
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "GaiZao";
        return _s;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        //三段伤害，每段造成其伤害的1/3，都可以触发攻击特效
        //如果装备2个，则造成6次伤害，每次1 / 6伤害，依此叠加

        var count = GetComponents<Nexus_6Arm>().Length;
        chara.AttackTimes = count * 3;

        chara.atk += 5;

        for (int x = 0; x < 3; x++)
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
        aim.atk -= 5;
    }
}
