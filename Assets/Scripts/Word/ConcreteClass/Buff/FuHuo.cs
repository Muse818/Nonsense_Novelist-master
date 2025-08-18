using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：俘获
/// </summary>
public class FuHuo : AbstractBuff
{
    static public string s_description = "优先攻击友方，期间伤害减半，结束后恢复";
    static public string s_wordName = "俘获";

    bool hasUse = false;
    override protected void Awake()
    {
      
        buffName = "俘获";
        description = "优先攻击友方，期间伤害减半，结束后恢复";
        book = BookNameEnum.Salome;
        isBad = true;
        isAll = true;
        maxTime = 100;

        base.Awake();

        chara.teXiao.PlayTeXiao("fuhuo");

        //如果已被俘获，则延长时间。
        var _fuhuos = GetComponents<FuHuo>();
        for (int i = 0; i < _fuhuos.Length; i++)
        {
            if (_fuhuos[i] != this)
            {
                _fuhuos[i].maxTime += this.maxTime;
                
                Destroy(this);
                return;
            }
        }

        //伤害减半
        hasUse = true;
        chara.attackAmount -= 0.5f;
        chara.hasBetray = true;


    }


    private void OnDestroy()
    {
        if (hasUse)
        {
                    chara.attackAmount+= 0.5f;
        chara.hasBetray = false;
            base.OnDestroy();
           
        }

       

       // if (GetComponent<AbstractCharacter>() == null) return;
       //if(this.GetComponents<FuHuo>().Length<=1)//只有自己
       // {
       //     if(GameObject.Find("AllCharacter").GetComponentsInChildren<FuHuo>().Length<=1)
       //         AbstractVerbs.OnAwake -= FuHuoSkill;

       //     AbstractCharacter character = GetComponent<AbstractCharacter>();
       //     if (character.attackA.GetType() == typeof(FuHuoMode))
       //     {
       //         DamageMode newMode = gameObject.AddComponent<DamageMode>();
       //         newMode.attackRange = character.attackA.attackRange;
       //         Destroy(character.attackA);
       //         character.attackA = newMode;
       //     }
       //     AbstractVerbs[] allVerb = GetComponents<AbstractVerbs>();
       //     foreach (AbstractVerbs verb in allVerb)
       //     {
       //         if (verb.skillMode.GetType() == typeof(FuHuoMode))
       //         {
       //             DamageMode newMode = gameObject.AddComponent<DamageMode>();
       //             newMode.attackRange = verb.skillMode.attackRange;
       //             Destroy(verb.skillMode);
       //             verb.skillMode = newMode;
       //         }
       //     }
       // }
    }

}

class FuHuoMode : AbstractSkillMode
{
    /// <summary>
    /// 对目标实际影响
    /// </summary>
    /// <param name="value">实际伤害</param>
    /// <param name="character">目标（来自目标数组）</param>
    public override float UseMode(AttackType attackType, float value, AbstractCharacter useCharacter, AbstractCharacter aimCharacter, bool hasFloat, float delay)
    { 
        //if (useCharacter != null)//角色使用
        //{
        //    float a = Random.Range(0, 100);//暴击抽奖
        //    if (a <= useCharacter.criticalChance * 100)//暴击
        //    {
        //        value *= useCharacter.multipleCriticalStrike;
        //        aimCharacter.teXiao.PlayTeXiao("BaoJi");
        //        AbstractBook.afterFightText += useCharacter.CriticalText(aimCharacter);
        //    }
        //     aimCharacter.hp -= value;
        //}
        //else//玩家使用（形容词）
        //{
        //    aimCharacter.hp -= (int)value;
        //}


        return value;
    }
    /// <summary>
    /// 再次计算锁定的目标
    /// </summary>
    /// <param name="character">施法者</param>
    /// <returns></returns>
    override public AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character)
    {
        
        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, NeedCampEnum.friend, false);//优先攻击友方，期间伤害减半，结束后恢复
        return a;
    }
    override public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character, bool _ignoreBoss)
    {

        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, _ignoreBoss);
        return a;
    }
}

