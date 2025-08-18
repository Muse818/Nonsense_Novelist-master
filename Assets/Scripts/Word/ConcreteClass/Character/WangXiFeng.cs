using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：王熙凤
/// </summary>
class WangXiFeng : AbstractCharacter
{
    override public void Awake()
    {

        base.Awake();

        atk += 5;
        def += 5;
        psy += 5;
        san += 5;

    }

    ///// <summary>
    ///// 身份
    ///// </summary>
    //public override float atk { get { return base.atk +5; } set { base.atk = value; } }
    //public override float def { get { return base.def + 5; } set { base.def = value; } }
    //public override float san { get { return base.san + 5; } set { base.san = value; } }
    //public override float psy { get { return base.psy  + 5; } set { base.psy  = value; } }
    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) && (type == GrowType.psy))
        {
            hasAddGrow.Add(GrowType.psy);
            string it = "凤姐歪在填漆床上，挑开大红销金撒花帐子，指着下人轻喝，“若碰到一点儿，可仔细你的皮！”";
            GameMgr.instance.draftUi.AddContent(it);
            return it;
        }



        return null;
    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return " 只听得一阵笑声，凤姐款款而来，其打扮彩绣辉煌，宛如神仙妃子，“我来迟了，不曾迎接远客。”";
        else
            return null;
    }

    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "王熙凤对着"+otherChara .wordName+"狠狠地一巴掌“你这癞狗扶不上墙的种子！”,";
        else
            return null;
    }
    public override string LowHPText()
    {
        return "王熙凤一生中操劳太过，因又气恼而气血上涌而引起血崩，此时身体变得十分虚弱。";
    }

    public override string DieText()
    {
        return "面色惨白的王熙凤支持不住倒了下去。";
    }

}
