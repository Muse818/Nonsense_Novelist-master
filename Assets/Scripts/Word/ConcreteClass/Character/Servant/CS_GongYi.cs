using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GongYi : ServantAbstract
{
    static public string s_description = "己方的工蚁越多越强大";
    static public string s_wordName = "随从：工蚁";
    override public void Awake()
    {
        base.Awake();
       

        
    }

    private void Start()
    {
        
        //检测现在的工蚁数量，并实时更新给该角色身上的所有工蚁
        //是否会有重复问题？
        CS_GongYi[] _yiQun = transform.parent
            .GetComponentsInChildren<CS_GongYi>();
        foreach (var _gongYi in _yiQun)
        {
            _gongYi.ChangeNumber(_yiQun.Length);
        }
    }

    /// <summary>
    /// 本队伍中，每额外有一只工蚁，则所有工蚁攻击+1，防御+3
    /// </summary>
    /// <param name="_count">工蚁的数目</param>
    public void ChangeNumber(float _count)
    {
        if (_count > 3)
            return;
        if (-_count < 0)
            return;

        atk = 3 + _count * 1;
        def = 10 + _count * 3;
    }
 
    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "早已看见多了一个妹妹，细看形容，只见泪光点点，娇喘微微，闲静时如姣花照水，行动处似弱柳扶风，" + otherChara.wordName + "笑道：“这个妹妹，我曾见过的”";
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
