using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff\状态：患病
/// </summary>
/// 

public class HuanBing_x : AbstractBuff
{
    public float effectTime;
    private float nowTime = 0;
    private bool isOpen = false;
    WaitForSeconds wf1second= new WaitForSeconds(1);

    DamageMode damageMode;
    override protected void Awake()
    {
        base.Awake();

        buffName = "患病";
        book = BookNameEnum.FluStudy;

        StartCoroutine(Wait());
        upup = 999;//无限

        isOpen = true;
        nowTime = 0;
        damageMode.attackRange = new SingleSelector();
    }


    //每秒受到2点物理伤害，
    IEnumerator Wait()
    {
        while (isOpen)
        {
             yield return wf1second;

        }
    }

    public override void Update()
    {
        base.Update();
        if (nowTime > effectTime)
        {
            isOpen = false;
            StopCoroutine(Wait());
            return;
        }

        nowTime += Time.deltaTime;

    }
}
