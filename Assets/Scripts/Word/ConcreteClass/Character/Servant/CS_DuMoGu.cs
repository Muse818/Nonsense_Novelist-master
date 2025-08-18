
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 随从：毒蘑菇
/// </summary>
/// 

public class CS_DuMoGu : ServantAbstract
{
    static public string s_description = "对攻击者造成1*<sprite name=\"psy\">的魔法伤害，3s<color=#dd7d0e>“中毒”</color>";
    static public string s_wordName = "随从：毒蘑菇";

    override public void Awake()
    {
        base.Awake();
       
        this.event_BeAttack += DoToAttacker;


    }

    void DoToAttacker(float _value,AbstractCharacter _attacker)
    {
        _attacker.BeAttack(AttackType.psy, 1 * this.atk * atkMul, true, 0, this);
        var buff=_attacker.gameObject.AddComponent<Upset>();buff.maxTime =3;
    }

    private void OnDestroy()
    {
        this.event_BeAttack -= DoToAttacker;
        masterNow.DeleteServant(this.gameObject);

    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "早已看见多了一个妹妹，细看形容，只见泪光点点，娇喘微微，闲静时如姣花照水，行动处似弱柳扶风，" + otherChara.wordName + "笑道：“这个妹妹，我曾见过的”";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "“我就知道，别人不挑剩下的也不给我。”林黛玉轻捻一朵花瓣，向" + otherChara.wordName + "飞去";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "黛玉对侍女喘息道：“笼上火盆罢。”便将一对帕子，一叠诗稿焚尽于火盆中。";
    }
    public override string DieText()
    {
        return "“宝玉…宝玉…你好……”黛玉没说完便合上了双眼。";
    }


}

