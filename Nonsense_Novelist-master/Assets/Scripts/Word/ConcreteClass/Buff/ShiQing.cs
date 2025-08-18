using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:诗情
/// </summary>
public class ShiQing : AbstractBuff
{
    static public string s_description = "提升20%<sprite name=\"psy\">";
    static public string s_wordName = "诗情";


    override protected void Awake()
    {
      
        buffName = "诗情";
        description = "提升20%<sprite name=\"psy\">";
        book = BookNameEnum.HongLouMeng;
        chara.psyMul += 0.2f;

        base.Awake();
        chara.teXiao.PlayTeXiao("shiQing");
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        chara.psyMul -= 0.2f;
    }
}
