using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物：赏金猎人
/// </summary>
public class ShangJingLieRen : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 114;
        wordName = "赏金猎人";
        bookName = BookNameEnum.allBooks;
        brief = "暂无文案";
        description = "暂无文案";

        //数值
        hp = maxHp = 30;
        atk = 6;
        def = 20;
        psy = 6;
        san = 15;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "刺客";
        roleInfo = "攻击时，对方每有一个名词伤害+1";//攻击时，对方每有一个名词伤害+1
       

        event_AttackA += AttackMore;
    }


    void AttackMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            int _count = myState.aim[i].GetComponents<AbstractItems>().Length;
            myState.aim[i].BeAttack(AttackType.dir, 1* _count, true, 0, this);
        }
    }

    private void OnDestroy()
    {
        event_AttackA -= AttackMore;
    }

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
            return "阿努比斯出场文本";
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
