using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词:社群丰容
/// </summary>
class SheQunFengRong : AbstractItems
{
    static public string s_description = "自动合成<color=#dd7d0e>混养笼</color>。每种不同生物，使混养笼<sprite name=\"hp\"> + 40";
    static public string s_wordName = "社群丰容";
    static public int s_rarity = 2;
    static public int s_useTimes = 4;


    List<string> servantsNow = new List<string>();

    public override void Awake()
    {
        base.Awake();
        itemID = 5;
        wordName = "社群丰容";
        bookName = BookNameEnum.ZooManual;
        description = "自动合成<color=#dd7d0e>混养笼</color>。每种不同生物，使混养笼<sprite name=\"hp\"> + 40";//随从
        rarity =2;
        useTimes = 4;
        VoiceEnum = MaterialVoiceEnum.materialNull;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();


        nowTime = 0;
        skillMode = new CureMode();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "CS_BenJieShiDui";

        return _s;
    }

    bool hasAdd=false;

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (hasAdd)
            return;
        if (chara == null)
        { 
            print("chara=null");return;
        }
        if (chara.servants.Count <= 3)
        {
            chara.AddRandomServant();
            return;
        }

        servantsNow.Clear();
        foreach (var _s in chara.servants)
        {
            var _ss = _s.GetComponent<ServantAbstract>().wordName;
            if (servantsNow.Contains(_ss))
            {

            }
            else
            {
                servantsNow.Add(_ss);
            }
        }
        chara.ServantMerge();
        chara.GetComponent<CS_HunYangLong>().maxHp += servantsNow.Count * 40;
        chara.GetComponent<CS_HunYangLong>().hp += servantsNow.Count * 40;


        //避免重复的开关
        hasAdd = true;
    }

    float nowTime;
    AbstractSkillMode skillMode;
    AbstractCharacter[] friends;
    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "白牡丹花蕊、白荷花花蕊、白芙蓉花蕊、白梅花花蕊......等十年未必都这样巧能做出这冷香丸呢！”" + character.wordName + "说道。";

    }
}
