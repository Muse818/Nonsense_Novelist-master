using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£ºÆÆ¼×
/// </summary>
public class PoJia : AbstractBuff
{
    static public string s_description = "<sprite name=\"def\">-20%£¬×î¶à3²ã";
    static public string s_wordName = "ÆÆ¼×";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "ÆÆ¼×";
        description = "<sprite name=\"def\">-20%£¬×î¶à3²ã";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = true;
        upup = 3;

        base.Awake();
       
        chara.defMul -= 0.2f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.defMul += 0.2f;
    }

}
