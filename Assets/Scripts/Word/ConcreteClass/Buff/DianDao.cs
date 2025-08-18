using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：颠倒
/// </summary>
public class DianDao : AbstractBuff
{
    static public string s_description = "交换当前的<sprite name=\"atk\">和<sprite name=\"psy\">，结束后恢复";
    static public string s_wordName = "颠倒";


    bool hasUse = false;
    float record = 0;
    override protected void Awake()
    {
       
        buffName = "颠倒";
        description = "交换当前的<sprite name=\"atk\">和<sprite name=\"psy\">，结束后恢复";
        book = BookNameEnum.allBooks;

        upup = 1;
        isBad = true;
        base.Awake();

        //如果已被颠倒，则延长时间。
        var _diandaos = GetComponents<DianDao>();
        for (int i = 0; i < _diandaos.Length; i++)
        {
            if (_diandaos[i] != this)
            {
                _diandaos[i].maxTime += this.maxTime;
                Destroy(this);
                return;
            }
        }

        //伤害减半
      
        record = chara.atk*chara.atkMul; 
     
        chara.atk = (chara.psy *chara.psyMul)/chara.atkMul;
       
        chara.psy = record / chara.psyMul;
     
        hasUse = true;
 

    }


    private void OnDestroy()
    {
        if (hasUse)
        {
            record = chara.atk * chara.atkMul;

            chara.atk = (chara.psy * chara.psyMul) / chara.atkMul;

            chara.psy = record / chara.psyMul;
            base.OnDestroy();
        }

    }
}
