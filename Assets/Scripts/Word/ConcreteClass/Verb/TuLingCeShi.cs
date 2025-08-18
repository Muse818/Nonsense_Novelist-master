using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：图灵测试
/// </summary>
class TuLingCeShi : AbstractVerbs
{
    static public string s_description = "被动：对 <sprite name=\"san\">低于10的敌人造成30%额外伤害;主动：使敌人受到自身 <sprite name=\"atk\"> +  <sprite name=\"san\">的精神伤害";
    static public string s_wordName = "图灵测试";
    static public int s_rarity = 4;


    public override void Awake()
    {
        base.Awake();
        skillID = 10;
        wordName = "图灵测试";
        bookName = BookNameEnum.ElectronicGoal;
        description = "被动：对 <sprite name=\"san\">低于10的敌人造成30%额外伤害;主动：使敌人受到自身 <sprite name=\"atk\"> +  <sprite name=\"san\">的精神伤害";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange =  new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 4;
        needCD = 3;
        if (character == null) return;

        if (!character.isNaiMa)
        {     
            character.event_AttackA += AttackAEvent;
        }
   
    }

    void AttackAEvent()
    {
        //这里不太确定，低于10san的敌人是在全部敌人中筛选还是就只针对aims
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            if (character.myState.aim[i].san < 10)
            {
                character.myState.aim[i].BeAttack(AttackType.dir, 0.3f * character.atk * character.atkMul, true, 0, character);
            }
        }
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }
    public override void OnDestroy()
    {
        if (character == null) return;

        if (!character.isNaiMa)
        {
            character.event_AttackA -= AttackAEvent;
        }
        
        base.OnDestroy();
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        //奶妈
        if (useCharacter.isNaiMa)
        {
            var _aims = skillMode.CalculateAgain(200, useCharacter);
            int x = 0;
            for (int i = 0; (i < _aims.Length) && (x < useCharacter.myState.aimCount); i++)
            {
                _aims[i].BeAttack(AttackType.psy, _aims[i].atk * _aims[i].atkMul + _aims[i].san * _aims[i].sanMul, true, 0, character);
                x++;
            }

            return;
        }
        //其它
        AbstractCharacter ac;
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            ac = character.myState.aim[i];
            ac.BeAttack(AttackType.psy, ac.atk * ac.atkMul + ac.san * ac.sanMul, true, 0, character);
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
