using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：垄断公司
/// </summary>
class LongDuanGongSi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        event_AttackA += RoleFunction;

    }



    void RoleFunction()//普通攻击造成伤害的30%，转化为生命
    {
     
        BeCure(atk * atkMul * attackAmount * 0.3f, true, 0,this);
        for (int i = 0; i < servants.Count; i++)
        {
            servants[i].GetComponent<AbstractCharacter>().BeCure(atk * atkMul * attackAmount * 0.3f, true, 0,this);
        }

    }

    private void OnDestroy()
    {
        event_AttackA -= RoleFunction;

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
            return "当一家公司倒下时，没有人能看到垄断公司庞大的黑影！";
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
    
    #endregion

}
