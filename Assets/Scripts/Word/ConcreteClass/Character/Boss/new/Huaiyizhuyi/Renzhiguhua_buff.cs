using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//huaiyizhuyiBOSS专属动词
//效果是：随机对三个角色生效，10秒内，攻击与精神每秒提升5%;结束时，对其造成2*（攻击+精神）-4*意志的精神伤害，伤害最大为20点


public class Renzhiguhua_buff : AbstractBuff
{
    float record;
    float remaindTime = 10;//提升效果的维持时长
    AbstractCharacter usingChara;

    float timePer = 0;
    float time = 0;


    override protected void Awake()
    {
       
        buffName = "认知固化";
        book = BookNameEnum.allBooks;

        usingChara=this.GetComponent<AbstractCharacter>();

        StartCoroutine(DoPerSecond());
        StartCoroutine(EndBuff());


        upup = 1;
        base.Awake();
    }


    /// <summary>
    /// 每秒执行
    /// </summary>
    /// <returns></returns>
    IEnumerator DoPerSecond()
    {
        while (true)
        {
            AddEffect();
            yield return new WaitForSeconds(1);
        }
       

    }

    IEnumerator EndBuff()
    {
      
        yield return new WaitForSeconds(remaindTime);

        EndAndAttack();
        StopCoroutine(DoPerSecond());
    }

    /// <summary>
    ///结束时，一次性造成伤害s
    /// </summary>
    public void EndAndAttack()
    {

        //2*（攻击+精神）-4*意志的精神伤害，伤害最大为20点
        float _endAttack=2*(usingChara.atk+usingChara.psy)-4*usingChara.san*usingChara.sanMul;
        if (_endAttack >= 20)
        { 
           
            usingChara.BeAttack(AttackType.dir, 20, true, 0, this.GetComponent<AbstractCharacter>());
            print(usingChara.wordName + "的怀疑主义结束，一次性造成伤害:" + 20); 
        }
        else
        {
            usingChara.BeAttack(AttackType.dir, _endAttack, true, 0, this.GetComponent<AbstractCharacter>());
           
            print(usingChara.wordName + "的怀疑主义结束，一次性造成伤害:"+ _endAttack);
        }
    }

    /// <summary>
    /// 攻击与精神每秒提升5%;
    /// </summary>
    public void AddEffect()
    {
        //print(usingChara.wordName + "的攻击与精神提升5%;");
        usingChara.hp *= 1.05f;
        usingChara. psyMul+= 0.05f;
    }
}
