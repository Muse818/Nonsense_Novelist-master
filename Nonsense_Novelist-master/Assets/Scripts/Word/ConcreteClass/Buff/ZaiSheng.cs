using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：再生
/// </summary>
public class ZaiSheng : AbstractBuff
{
    static public string s_description = "生命恢复+10";
    static public string s_wordName = "再生";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "再生";
        description = "生命恢复+10";
        book = BookNameEnum.allBooks;  

        upup = 3;

        base.Awake();
       
        chara.cure += 10f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.cure -= 10f;
    }

}
