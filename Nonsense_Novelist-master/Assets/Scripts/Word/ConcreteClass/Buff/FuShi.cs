using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:腐蚀
/// </summary>
public class FuShi: AbstractBuff
{
    static public string s_description = "降低1 <sprite name=\"def\">，可叠加";
    static public string s_wordName = "腐蚀";

    override protected void Awake()
    {
        //防御-1
       // 这个状态不需要用图标显示出来，是隐藏的负面状态
        //可以被净化，但不计入减益状态的计算，因为它可以叠加的层数太高了
        buffName = "腐蚀";
        description = "降低1<sprite name=\"def\">，可叠加";
        
        book = BookNameEnum.allBooks;

        isAll = true;
        isBad = true;

        base.Awake(); 

        chara.def -= 1;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        chara.def += 1;
    }

}
