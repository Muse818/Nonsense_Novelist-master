using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：防腐
/// </summary>
class FangFuShu : AbstractVerbs
{
    static public string s_description = "治疗队友100%<sprite name=\"san\">，当有队友倒下时，立刻追击2次";
    static public string s_wordName = "防腐";
    static public int s_rarity = 2;
    public override void Awake()
    {
        base.Awake();
        skillID = 5;
        wordName = "防腐";
        bookName = BookNameEnum.EgyptMyth;
        description = "治疗队友100%<sprite name=\"san\">，当有队友倒下时，立刻追击2次";
        //将这个技能的机制改一下，等角色倒下时才会释放，释放完才消耗能量点
        //目标：血量最低的友方
        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange =  new SingleSelector();


        skillEffectsTime = 10;
        rarity = 2;
        needCD = 8;


    }



    /// <summary>
    /// 复活
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        isUsing = true;

        //治疗10+100%意志
        AbstractCharacter[] aims = skillMode.CalculateAgain(attackDistance, useCharacter);
        var aim = aims[Random.Range(0, aims.Length)];
        skillMode.UseMode(AttackType.heal, aim.san * aim.sanMul * 1, aim, aim, true, 0);

        //找是否有倒下的友军
        var charas = skillMode.CalculateAgain(attackDistance, useCharacter);
        AbstractCharacter deadChara = null;
        foreach (var chara in charas)
        {
            if (chara.myState.nowState == chara.myState.allState.Find(p => p.id == AI.StateID.dead))
            {
                deadChara = chara;
            }
        }
        if (deadChara == null) return;

        useCharacter.AttackTimes += 1;
        useCharacter.attackSpeedPlus += 1;
       useCharacter.event_AttackA += Back;
        //deadChara.reLifes += 1;
        //CD = 0;
        //hasFull = false;
        //character.charaAnim.Play(AnimEnum.attack);
        //character.CreateFloatWord(this.wordName, FloatWordColor.physics, false);

    }
    bool back;
    public void Back()
    {
        this.GetComponent<AbstractCharacter>().attackSpeedPlus -= 1;
        this.GetComponent<AbstractCharacter>().AttackTimes -= 1;
        this.GetComponent<AbstractCharacter>().event_AttackA -= Back;
    }

    //bool hasFull = false;
    
    //如果满了且未使用，则能量不增加
    //public override void CdAdd(AbstractCharacter AC)
    //{
    //    if (!hasFull)
    //    {
    //        CD++;
    //        if (CD  >= needCD)
    //        {
    //            character.canUseSkills++;
    //            hasFull = true;
    //        }
    //    }
    //    else
    //    {
            
    //    }

    //}

    //public override void CDZero()
    //{
        
    //}

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
