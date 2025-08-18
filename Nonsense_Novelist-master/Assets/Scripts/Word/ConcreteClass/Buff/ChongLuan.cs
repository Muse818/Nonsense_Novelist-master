using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：虫卵
/// </summary>
public class ChongLuan : AbstractBuff
{
    static public string s_description = "受到治疗后，孵化一只工蚁随从";
    static public string s_wordName = "虫卵";

    float recordHp = 0;
    override protected void Awake()
    {
        
        buffName = "虫卵";
        description = "受到治疗后，孵化一只工蚁随从";
        book = BookNameEnum.allBooks;
        upup = 3;
        //
  
        base.Awake();
        chara.event_BeCure += OnCure;
    }


    public override void Update()
    {
        
        base.Update();

    }
    void OnCure()
    {
        chara.AddServant("CS_GongYi");
        chara.event_BeCure -= OnCure;
        Destroy(this);
    }
}
