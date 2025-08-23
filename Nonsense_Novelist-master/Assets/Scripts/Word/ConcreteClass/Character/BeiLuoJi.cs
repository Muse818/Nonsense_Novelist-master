using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：贝洛姬·姬妮
/// </summary>
class BeiLuoJi : AbstractCharacter
{
    override public void Awake()
    {

        base.Awake();
        event_AttackA += Role;
    }

    AbstractCharacter[] aims;
    float record;
    void Role()
    {
        
        var chara = CharacterManager.instance.GetFriend(this.Camp);
        if (chara.Count > 0 && chara[0] != null)
        {
            chara[0].BeCure(chara[0].san * chara[0].sanMul * 0.7f, true, 0.5f, this);
            CreateBullet(chara[0].gameObject);
        }
        if (chara.Count > 1 &&chara[1] != null)
        {
            chara[1].BeCure(chara[1].san * chara[1].sanMul * 0.7f, true, 0.5f, this);
            CreateBullet(chara[1].gameObject);
        }
    }
    int aimCount = 0;
    //public override bool AttackA()
    //{//代替平A
    //    aimCount = 0;
    //    if (myState.aim != null)
    //    {
    //        if (myState.character.aAttackAudio != null)
    //        {
    //            myState.character.source.clip = myState.character.aAttackAudio;
    //            myState.character.source.Play();
    //        }
    //        myState.character.charaAnim.Play(AnimEnum.attack);


    //        aims = attackA.CalculateAgain(100, this);

    //        foreach (AbstractCharacter aim in aims)
    //        {//普通攻击目标为所有队友，恢复70%意志的血量，不附带攻击攻击特效
    //            if (aim.myState.nowState == aim.myState.allState.Find(p => p.id == AI.StateID.dead) || (aimCount >= 3))
    //            {}
    //            else
    //            {
    //                aimCount += 1;
    //                attackA.UseMode(AttackType.heal, san *sanMul* 0.7f, myState.character, aim, true, 0);
    //                myState.character.CreateBullet(aim.gameObject);
    //            }


             
    //        }
    //        return true;    
    //    }
    //    return false;

    //}

    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) && (type == GrowType.psy))
        {
            hasAddGrow.Add(GrowType.psy);
            string it = "那天渐渐的黄昏，且阴的沉重，兼着那雨滴竹梢，更觉凄凉，黛玉不觉心有所感，亦不禁发于章句，遂成诗一首。";
            GameMgr.instance.draftUi.AddContent(it);
            return it;
        }



        return null;
    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "蚂蚁帝国有条不紊地运转，这都有赖贝洛姬·姬妮的精心筹谋。" ;
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "“我就知道，别人不挑剩下的也不给我。”林黛玉轻捻一朵花瓣，向" + otherChara.wordName + "飞去";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "黛玉对侍女喘息道：“笼上火盆罢。”便将一对帕子，一叠诗稿焚尽于火盆中。";
    }
    public override string DieText()
    {
        return "“宝玉…宝玉…你好……”黛玉没说完便合上了双眼。";
    }

}
