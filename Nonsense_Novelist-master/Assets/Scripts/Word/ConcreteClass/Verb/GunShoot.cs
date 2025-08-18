using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动词：枪击
/// </summary>
class GunShoot : AbstractVerbs
{
    static public string s_description = "造成3*<sprite name=\"atk\">的物理伤害";
    static public string s_wordName = "枪击";
    static public int s_rarity = 2;
    public override void Awake()
    {
        base.Awake();
        skillID = 11;
        wordName = "枪击";
        bookName = BookNameEnum.ElectronicGoal;
        description = "造成3*<sprite name=\"atk\">的物理伤害";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = true;
        skillMode.attackRange =  new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 2;
        needCD = 2;

    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
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
                _aims[i].BeAttack(AttackType.atk, character.atk * character.atkMul * 3, true, 0, character);
                x++;
            }

            return;
        }
        //其它
        for (int i = 0; i < character.myState.aim.Count; i++)
        {
            character.myState.aim[i].BeAttack(AttackType.atk, character.atk * character.atkMul * 3, true, 0, character);
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
