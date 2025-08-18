using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 莎乐美
/// </summary>
class ShaLeMei : AbstractCharacter
{
    override public void Awake()
    {

        base.Awake();

        //基础信息
        characterID = 8;
        wordName = "莎乐美";
        bookName = BookNameEnum.Salome;
        brief = "暂无介绍";
        description = "暂无介绍";

        //数值
        hp = maxHp = 70;
        atk = 3;
        def = 3;
        psy = 6;
        san = 3;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "舞女";
        roleInfo = "攻击降低对方的意志";//普通攻击附带“冷漠”，不可叠加，持续10s
        event_AttackA += AttackAMore;
    }

   

    void AttackAMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            myState.aim[i].gameObject.AddComponent<LengMo>().maxTime = 10;
        }
    }

    private void OnDestroy()
    {
        event_AttackA -= AttackAMore;
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
            return "莎乐美手捧银盘而来，银盘上血迹斑斑，约翰的头颅已无生气，嘴唇泛着苍白的微笑。";
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
