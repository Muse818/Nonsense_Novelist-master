using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：冷漠
/// </summary>
public class LengMo : AbstractBuff
{
    static public string s_description = "意志-30%，最多3层";
    static public string s_wordName = "冷漠";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "冷漠";
        description = "意志-30%，最多3层";
        book = BookNameEnum.allBooks;  
        isBad = true;
        upup = 2;

        base.Awake();
       
        chara.sanMul -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.sanMul += 0.3f;
    }

}
