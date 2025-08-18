using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：林黛玉
/// </summary>
class LinDaiYu : AbstractCharacter
{
    override public void Awake()
    {
       
        base.Awake();

        //特性

        psyMul += 0.25f;
        defMul -= 0.2f;

    }

    /// <summary>
    /// 身份
    /// </summary>
    //public override float psy { get { return base.psy * 1.25f; } set { base.psy = value; ; } }
    //public override float def { get { return base.def*0.8f; } set { base.def = value; } }


    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) &&(type == GrowType.psy))
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
            return "忽听外面人说：“林姑娘来了！'话犹未了，林黛玉已摇摇的走了进来，笑道，“嗳呦，我来的不巧了！”";
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
