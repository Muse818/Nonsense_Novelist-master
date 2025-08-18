using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：情迷
/// </summary>
public class QingMi : AbstractBuff
{
    static public string s_description = "因背叛而攻击友方，伤害减半;";
    static public string s_wordName = "情迷";

    bool hasUse = false;
    override protected void Awake()
    {

        buffName = "情迷";
        description = "因背叛而攻击友方，伤害减半";
        book = BookNameEnum.Salome;
        isBad = true;
        isAll = false;
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
            chara.attackAmount += 0.5f;
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
