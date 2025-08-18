using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：坚实
/// </summary>
public class JianShi : AbstractBuff
{
    static public string s_description = "<sprite name=\"def\">+20%";
    static public string s_wordName = "坚实";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "坚实";
        description = "<sprite name=\"def\">+20%";
        book = BookNameEnum.allBooks;  

        upup = 2;

        base.Awake();
       
        chara.defMul += 0.2f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.defMul -= 0.2f;
    }

}
