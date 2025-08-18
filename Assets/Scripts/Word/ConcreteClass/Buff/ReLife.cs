using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:复活
/// </summary>
public class ReLife : AbstractBuff
{
    static public string s_description = "在受到致命伤害时恢复所有生命与魔法，清除所有负面状态";
    static public string s_wordName = "复活";
    override protected void Awake()
    {
       
        buffName = "复活";
        description = "在受到致命伤害时恢复所有生命与魔法，清除所有负面状态";
        book = BookNameEnum.EgyptMyth;
        maxTime = 100;

        base.Awake();

     
        chara.reLifes ++;;
   
 
    }

    private void OnDestroy()
    {
       
        chara.reLifes --;     chara.DeleteBadBuff(100);
        base.OnDestroy();
    }
}
