using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£∫–È»ı
/// </summary>
public class XuRuo : AbstractBuff
{
    static public string s_description = "<sprite name=\"atk\">-30%£¨◊Ó∂‡3≤„";
    static public string s_wordName = "–È»ı";
    AttackState state;
    override protected void Awake()
    {
       
        buffName = "–È»ı";
        description = "<sprite name=\"atk\">-30%£¨◊Ó∂‡3≤„";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = true;
        upup = 3;
        state=GetComponentInChildren<AttackState>();

        base.Awake();

        chara.atkMul -= 0.3f;
    }

    public override void Update()
    {
        base.Update();
      

    }
    private void OnDestroy()
    {
        base.OnDestroy();
        chara.atkMul += 0.3f;
    }

}


