using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£º³°·í
/// </summary>
public class ChaoFeng : AbstractBuff
{
    static public string s_description = "ÎüÒýµÐÈËµÄ¹¥»÷";
    static public string s_wordName = "³°·í";
    AttackState state;
    override protected void Awake()
    {
    
        buffName = "³°·í";
        description = "ÎüÒýµÐÈËµÄ¹¥»÷";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();
        base.Awake();

    }

    public override void Update()
    {
        base.Update();
        
    }
    private void OnDestroy()
    {
        base.OnDestroy();
       
    }

}


