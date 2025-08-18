using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// buff:起舞
/// </summary>
public class QiWu : AbstractBuff
{
    static public string s_description = "普通攻击攻击所有敌人，伤害降低70%，附带攻击效果";
    static public string s_wordName = "起舞";

    private int recordAimCount;
    bool hasUse = false;

    override protected void Awake()
    {
       
        buffName = "起舞";
        description = "普通攻击攻击所有敌人，伤害降低70%，附带攻击效果";
        book = BookNameEnum.allBooks;

        base.Awake();

        var _qiwus = GetComponents<QiWu>();
        for (int i=0;i< _qiwus.Length;i++)
        {
            if (_qiwus[i] != this)//如果角色身上还挂有其他的起舞
            {
                _qiwus[i].maxTime += this.maxTime;
                Destroy(this);
                return;
            }
        }

        //如果角色身上没有挂着其他的起舞

        recordAimCount = chara.myState.aimCount;
        chara.myState.aimCount = 10;

        chara.attackAmount -= 0.7f;
        hasUse = true;

    }

    private void Update()
    {
        
        base.Update();
    }
    private void OnDestroy()
    {
        if (hasUse)//在起舞获得时间和失去时间相撞的时候可能会有bug，如何处理
        { 
            chara.myState.aimCount = recordAimCount;
            chara.attackAmount += 0.7f;
            base.OnDestroy();
        }
        
    }
}
