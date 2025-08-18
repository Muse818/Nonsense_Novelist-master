using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：七重纱之舞
/// </summary>
class QiChongShaDance : AbstractVerbs
{

    static public string s_description = "被动：攻击额外造成20%的精神伤害；主动：起舞，攻击所有敌人，持续10s";
    static public string s_wordName = "七重纱之舞";
    static public int s_rarity = 4;
    public override void Awake()
    {
        base.Awake();
        skillID = 7;
        wordName = "七重纱之舞";
        bookName = BookNameEnum.Salome;
       
        skillMode = gameObject.AddComponent<SelfMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime =10;
        rarity = 4;
        needCD=8;
        description = "被动：攻击额外造成20%的精神伤害；主动：<color=#dd7d0e>起舞</color>，攻击所有敌人，持续10s";
        if (character == null) return;

        if (!character.isNaiMa)
        {
            character.event_AttackA += AddToAttackA;
        }
       
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "QiWu";
        return _s;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
       
        base.UseVerb(useCharacter);

         //其它
        buffs.Add(gameObject.AddComponent<QiWu>());
        buffs[0].maxTime = skillEffectsTime;
    }
    void AddToAttackA()
    {
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            //攻击额外造成20%的精神伤害；
            character.myState.aim[i].BeAttack(AttackType.psy, 0.2f * character.psy * character.psyMul, true, 0, character);
        }
       
    }

    private void OnDestroy()
    {
        if (character == null) return;

        if (GetComponent<AbstractCharacter>() != null)
        {
            GetComponent<AbstractCharacter>().event_AttackA -= AddToAttackA;
        }
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "伴着轻风与身上挂坠碰撞的金属声，" + character.wordName + "开始蹁跹起舞。周围的人们都纷纷被这翩若惊鸿的舞姿激励了，并且感觉充满了力量。";

    }
}
