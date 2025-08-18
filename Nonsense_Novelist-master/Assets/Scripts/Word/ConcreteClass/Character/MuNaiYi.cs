using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 木乃伊
/// </summary>
class MuNaiYi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();


    }
    NoHealthTrigger _nht = null;
    private void Start()
    {  // 每次复活攻击 + 3防御 + 6”
        var _list = myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
        foreach (var _t in _list)
        {
            if (_t.GetType() == typeof(AI.NoHealthTrigger))
            { (_t as AI.NoHealthTrigger).event_relife += OnLive; _nht = (_t as AI.NoHealthTrigger); }

        }

        //出场获得“复活”
        var re =gameObject.AddComponent<ReLife>();
        re.maxTime = Mathf.Infinity;//复活技能
  
    }

    /// <summary>
    /// 身份
    /// </summary>
    public void OnLive(AbstractCharacter ac)
    {
        atk += 3;
        def += 6;

    }

    private void OnDestroy()
    {
        if (_nht != null)
        {
            _nht.event_relife -= OnLive;
        }
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
            return "自干燥的沙砾中伸出一只缠满绷带的手，木乃伊咳嗽着吐掉胸腔中的沙子，注视着永不熄灭的太阳叹了口气。";
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
