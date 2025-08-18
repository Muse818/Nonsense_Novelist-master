using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：失恋
/// </summary>
class ShiLian : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

      
        event_AttackA += UpsetAttackA;
    }
  
    /// <summary>
    /// 特性
    /// </summary>
    void UpsetAttackA()
    {
      
        for (int i = 0; i < myState.aim.Count; i++)
        {  
            if (Random.Range(1, 101) <= 20)
            {
                myState.aim[i].gameObject.AddComponent<Upset>().maxTime = 3;
            }
        }
    }

    private void OnDestroy()
    {
        event_AttackA -= UpsetAttackA;
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
        return "玫瑰凋谢在昏黄的月色下，月亮又目睹了一次失恋。";
    }

    public override string CriticalText(AbstractCharacter otherChara)
    {
        return "";
    }

    public override string LowHPText()
    {
        return "";
    }
    public override string DieText()
    {
        return "";
    }
}
