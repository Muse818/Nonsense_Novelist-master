using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：患病
/// </summary>
public class Ill : AbstractBuff
{
    static public string s_description = "每秒受到2点物理伤害，连续患病10s后变得<color=#dd7d0e>虚弱</color>,可无限叠加";
    static public string s_wordName = "患病";

    /// <summary>外部赋值使用者</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
        
        buffName = "患病";
        description = "每秒受到2点物理伤害，连续患病10s后变得<color=#dd7d0e>虚弱</color>,可无限叠加";
        book = BookNameEnum.FluStudy;
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
        isAll = true;
        nowTime = 0;

        base.Awake();
        StartCoroutine(MakeAttack());
    }


    public override void Update()
    {
        base.Update();

    }

    IEnumerator MakeAttack() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            nowTime += 1;
            
             damageMode.UseMode(AttackType.atk, 2,useCharacter != null ? useCharacter : chara, chara,true,0/*2 * (1 - chara.def / (chara.def + 20))*/);
            if (nowTime >= 10)
            {
                var xr=gameObject.AddComponent<XuRuo>();
                xr.maxTime = Mathf.Infinity;
                nowTime = -9999;
            }
        }
    }

    private void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}
