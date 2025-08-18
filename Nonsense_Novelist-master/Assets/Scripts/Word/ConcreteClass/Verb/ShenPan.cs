using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 动词：审判
/// </summary>
class ShenPan : AbstractVerbs
{
    static public string s_description = "目标生命<400，造成300% <sprite name=\"atk\">的物理伤害；目标生命 > 400，造成500% <sprite name=\"psy\">的精神伤害，且自身获得<color=#dd7d0e>亢奋</color>*2";
    static public string s_wordName = "审判";
    static public int s_rarity = 4;

    public override void Awake()
    {
        base.Awake();
        skillID = 6;
        wordName = "审判";
        bookName = BookNameEnum.HongLouMeng;
        description = "目标生命<400，造成300% <sprite name=\"atk\">的物理伤害；目标生命 > 400，造成500% <sprite name=\"psy\">的精神伤害，且自身获得<color=#dd7d0e>亢奋</color>*2";
        skillMode = gameObject.AddComponent<DamageMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 10;
        rarity = 4;
        needCD = 6;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "KangFen";
        return _s;
    }


    AbstractCharacter chara;

    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        //奶妈
        AbstractCharacter[] _aims;
        if (useCharacter.isNaiMa)
        {
            _aims = skillMode.CalculateAgain(200, useCharacter);
            int x=0;
            for (int i = 0; (i < _aims.Length)&&(x<useCharacter.myState.aimCount); i++)
            {
                if (_aims[i].hp < 400)
                {
                    _aims[i].BeAttack(AttackType.atk, 3 * useCharacter.atk * useCharacter.atkMul, true, 0, useCharacter);
                }
                if (_aims[i].hp >= 400)
                {
                    _aims[i].BeAttack(AttackType.psy, 3 * useCharacter.psy * useCharacter.psyMul , true, 0, useCharacter);
                    buffs.Add(useCharacter.gameObject.AddComponent<KangFen>());
                    buffs[0].maxTime = skillEffectsTime;
                }
                x++;
            }

            return;
        }

        
        //
        for (int i = 0; i < useCharacter.myState.aim.Count; i++)
        {
            if (useCharacter.myState.aim[i].hp < 400)
            {
                useCharacter.myState.aim[i].BeAttack(AttackType.atk, 3 * useCharacter.atk * useCharacter.atkMul, true, 0,useCharacter);
            }
            if (useCharacter.myState.aim[i].hp >= 400)
            {
                useCharacter.myState.aim[i].BeAttack(AttackType.psy, 3 * useCharacter.psy * useCharacter.psyMul, true, 0, useCharacter);
                //获得的能量会应用于角色所有技能，是正常的能量
                buffs.Add(useCharacter.gameObject.AddComponent<KangFen>());
                buffs[0].maxTime = skillEffectsTime;
            }
        }
      



    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n"+character.wordName+"将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
    
}
