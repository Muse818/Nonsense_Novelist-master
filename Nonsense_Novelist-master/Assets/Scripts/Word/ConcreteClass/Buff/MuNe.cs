using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff��ľګ
/// </summary>
public class MuNe : AbstractBuff
{
    static public string s_description = "<sprite name=\"psy\">-30%";
    static public string s_wordName = "ľګ";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "ľګ";
        description = "<sprite name=\"psy\">-30%";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = false;
        upup = 2;

        base.Awake();
       
        chara.psyMul -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.psyMul += 0.3f;
    }

}
