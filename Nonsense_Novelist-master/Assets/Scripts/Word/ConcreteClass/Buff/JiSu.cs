using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：急速
/// </summary>
public class JiSu : AbstractBuff
{
    static public string s_description = "攻击速度+20%";
    static public string s_wordName = "急速";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "急速";
        description = "攻击速度+20%";
        book = BookNameEnum.allBooks;  

        upup = 2;

        base.Awake();
       
        chara.attackSpeedPlus += 0.2f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.attackSpeedPlus -= 0.2f;
    }

}
