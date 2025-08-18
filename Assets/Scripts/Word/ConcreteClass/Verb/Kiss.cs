using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：亲吻
/// </summary>
class Kiss : AbstractVerbs
{
    static public string s_description = "使敌人受到300%<sprite name=\"psy\">的精神伤害，并被<color=#dd7d0e>情迷</color>5s";
    static public string s_wordName = "亲吻";
    static public int s_rarity = 2;
    public override void Awake()
    {
        base.Awake();
        skillID = 8;
        wordName = "亲吻";
        bookName = BookNameEnum.Salome;
        description = "使敌人受到300%<sprite name=\"psy\">的精神伤害，并被<color=#dd7d0e>情迷</color>5s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange =  new SingleSelector();

        skillEffectsTime =5;
        rarity = 2;
        needCD = 5;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "QingMi";
        return _s;
    }
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        //奶妈
        if (useCharacter)
        {
            var _aims = skillMode.CalculateAgain(200, useCharacter);
            int x = 0;
            for (int i = 0; (i < _aims.Length) && (x < useCharacter.myState.aimCount); i++)
            {
                skillMode.UseMode(AttackType.psy, 3 * useCharacter.psy * useCharacter.psyMul, useCharacter, _aims[i], true, 0);

                buffs.Add(_aims[i].gameObject.AddComponent<QingMi>());
                buffs[0].maxTime = skillEffectsTime;
                x++;
            }
            return;
        }


        //
        for (int i = 0; i < useCharacter.myState.aim.Count; i++)
        {

            skillMode.UseMode(AttackType.psy, 3 * useCharacter.psy * useCharacter.psyMul, useCharacter, useCharacter.myState.aim[i], true, 0);

            buffs.Add(useCharacter.myState.aim[i].gameObject.AddComponent<FuHuo>());
            buffs[0].maxTime = skillEffectsTime;
        }
       
    
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
    }
        
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
