using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£ºº®Àä
/// </summary>

public class HanLen : AbstractBuff
{
    static public string s_description = "¹¥»÷ËÙ¶È-30%";
    static public string s_wordName = "º®Àä";
    float record;
    override protected void Awake()
    {


        buffName = "º®Àä";
        description = "¹¥»÷ËÙ¶È-30%";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = false;
        upup = 2;

        base.Awake();

        chara.attackSpeedPlus -= 0.3f;



    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.attackSpeedPlus += 0.3f;
    }
}
