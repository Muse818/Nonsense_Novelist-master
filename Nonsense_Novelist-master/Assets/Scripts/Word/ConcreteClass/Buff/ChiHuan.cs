using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：迟缓
/// </summary>
public class ChiHuan : AbstractBuff
{
    static public string s_description = "攻击速度-30%，最多3层";
    static public string s_wordName = "迟缓";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "迟缓";
        description = "攻击速度-30%，最多3层";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = true;
        upup = 3;

        base.Awake();
       
        chara.attackSpeedPlus -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.attackSpeedPlus += 0.3f;
    }

}
