using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:花瓣
/// </summary>
public class HuaBan: AbstractBuff
{   
    static public string s_description = "提升1<sprite name=\"psy\">";
    static public string s_wordName = "花瓣";
    override protected void Awake()
    {
        
        buffName = "花瓣";
        description = "提升1<sprite name=\"psy\">";
        book = BookNameEnum.allBooks; 
         maxTime = 200;
        base.Awake();

        chara.psy += 1;
       

        
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        chara.psy -= 1;
    }
}
