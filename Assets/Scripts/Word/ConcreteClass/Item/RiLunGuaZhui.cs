using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 日轮挂坠
/// </summary>
class RiLunGuaZhui : AbstractItems
{
    static public string s_description = "<sprite name=\"hpmax\">+30，恢复+3,可消耗进行复活";
    static public string s_wordName = "日轮挂坠";
    static public int s_rarity = 3;
    static public int s_useTimes = 2;
    public override void Awake()
    {
        base.Awake();
        itemID = 6;
        useTimes = 2;
        wordName = "日轮挂坠";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"hpmax\">+30，恢复+3,可消耗进行复活";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 3;
    }

    AbstractCharacter ac;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        //生命上限+30，恢复+3
        chara.cure += 3;
        chara.maxHp += 30;
        chara.CreateFloatWord(30, FloatWordColor.healMax, false);
        ac = chara;

    }

    public override void UseVerb()
    {
        base.UseVerb();
        if (CharacterManager.instance.pause) return;
        if (ac == null) return;
        if (ac.hp > 1) return;

        if ((ac.myState.nowState == ac.myState.allState.Find(p => p.id == AI.StateID.dead)))
        {
            //角色死亡时
            if (ac.reLifes == 0)
            {
                ac.reLifes++;
                Destroy(this);
            }
             
         
        }
    }

    public override void End()
    {
        base.End();
        aim.maxHp -= 30;
        aim.cure -= 3;
    }
}
