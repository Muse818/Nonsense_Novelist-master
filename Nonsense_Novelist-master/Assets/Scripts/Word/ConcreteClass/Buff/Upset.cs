using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£º¾ÚÉ¥
/// </summary>
public class Upset : AbstractBuff
{
    static public string s_description = "Í£Ö¹ÆÕÍ¨¹¥»÷";
    static public string s_wordName = "¾ÚÉ¥";
    AttackState state;
    override protected void Awake()
    {
       
        buffName = "¾ÚÉ¥";
        description = "Í£Ö¹ÆÕÍ¨¹¥»÷";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();

        base.Awake();
    }

    public override void Update()
    {
        base.Update();
        chara.charaAnim.SetSpeed(AnimEnum.attack, 0);

    }
    private void OnDestroy()
    {
        base.OnDestroy();
        chara.charaAnim.SetSpeed(AnimEnum.attack, chara.attackSpeedPlus);
    }

}


