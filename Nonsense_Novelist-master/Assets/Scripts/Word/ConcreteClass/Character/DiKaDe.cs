using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 狄卡德
/// </summary>
class DiKaDe : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

       

        event_AttackA += AttackMore;
    }

    /// <summary>
    /// 身份
    /// </summary>
    void AttackMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            myState.aim[i].BeAttack(AttackType.dir, myState.aim[i].maxHp * 0.03f, true, 0, this) ;
        }
    }

    private void OnDestroy()
    {
        event_AttackA-= AttackMore;
    }



    #region 文本
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
            return "市中心的霓虹灯牌爆了又爆，狄卡德在喧嚣的酒吧中沉默地喝下一杯劣质鸡尾酒。";
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
    
    #endregion

}
