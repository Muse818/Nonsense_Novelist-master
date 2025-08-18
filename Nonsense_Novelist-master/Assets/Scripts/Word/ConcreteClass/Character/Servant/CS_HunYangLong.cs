
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 随从：混养笼
/// </summary>
public class CS_HunYangLong : ServantAbstract
{
    static public string s_description = "暂无介绍文本";
    static public string s_wordName = "随从：混养笼";

    AbstractCharacter[] aims;
    Coroutine coroutineAttack = null;


    override public void Awake()
    {
        base.Awake();
        

    }



    /// <summary>
    /// 设置最开始的数值
    /// </summary>
    public void SetInitNumber(ServantAbstract _s1)
    {
        atk += _s1.atk;
        def += _s1.def;
        san += _s1.san;
        psy += _s1.psy;
        if (maxHp == 1340.25f)
        {
            //初始数值
            maxHp = _s1.maxHp;
        }
        else
        {
             maxHp += _s1.maxHp; 
        }
        hp = maxHp;   
        
    }

    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<CS_YiZhiWeiShiQi>() != null)
            Destroy(masterNow.GetComponent<CS_YiZhiWeiShiQi>());
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
