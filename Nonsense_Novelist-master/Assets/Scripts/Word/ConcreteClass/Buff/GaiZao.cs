using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:改造
/// </summary>
public class GaiZao : AbstractBuff
{
    static public string s_description = " 减少10%<sprite name=\"san\">，每5层获得一种“卓越性能”";
    static public string s_wordName = "改造";

    // 3层，生命上限+60；
    //5层，生命上限+130，攻击+5，意志减半；
    //7层，生命上限+260，攻击+15，意志最高为0
    int count=0;

    float record=0;
    override protected void Awake()
    {

        buffName = "改造";
        description = " 减少10%<sprite name=\"san\">，每5层获得一种“卓越性能”";
        book = BookNameEnum.ElectronicGoal;


        base.Awake();
        chara.sanMul -= 0.05f;

        count = GetComponents<GaiZao>().Length;
        //每层改造-5%意志
        //每5层改造，随机永久获得以下状态的一种
        //卓越性能 - 锐利
        //卓越性能 - 疾速
        //卓越性能 - 坚实
        //卓越性能 - 再生
        if (count % 5 == 0)
        {
            //获得一种卓越性能
        }

    }

    private void OnDestroy()
    {
        base.OnDestroy();
        count = GetComponents<GaiZao>().Length;
        if (count >= 7)
        {
            chara.maxHp -= 260;
            chara.atk -= 15;

            chara.san = record; 

        }
        else if (count >= 5)
        {
            chara.maxHp -= 130;
            chara.atk -= 5;
            chara.san += record / 2;
        }
        else if (count >= 3)
        {
            chara.maxHp -= 60;

        }
        else
        { }
    }

}
