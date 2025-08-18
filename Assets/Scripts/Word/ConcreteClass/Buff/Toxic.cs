using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:中毒
/// </summary>
public class Toxic : AbstractBuff
{
    static public string s_description = "每秒受到5点物理伤害";
    static public string s_wordName = "中毒";
    /// <summary>外部赋值使用者</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
 
        buffName = "中毒";
        description = "每秒受到5点物理伤害";
        book = BookNameEnum.allBooks;
        //
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
        base.Awake();
    }


    public override void Update()
    {
        base.Update();
        nowTime += Time.deltaTime;
        if(nowTime>1)
        {
            nowTime= 0;
            damageMode.UseMode(AttackType.atk, 5, useCharacter != null ? useCharacter : chara, chara, true, 0);
            
        }
    }
}
