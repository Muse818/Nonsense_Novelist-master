using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：食物丰容(原本杰士堆)
/// </summary>
class ShiWuFengRong : AbstractItems
{
    static public string s_description = "随从四维+10%，获得时随机获得一个<color=#dd7d0e>丰容设施</color>";
    static public string s_wordName = "食物丰容";
    static public int s_rarity = 3;
    static public int s_useTimes = 2;
    public override void Awake()
    {
        base.Awake();
        itemID = 4;
        wordName = "食物丰容";
        bookName = BookNameEnum.ZooManual;
        description = "随从四维+10%，获得时随机获得一个<color=#dd7d0e>丰容设施</color>";//随从
        rarity =3;
        useTimes = 2;
        VoiceEnum = MaterialVoiceEnum.materialNull;
   

        nowTime = 0;
        skillMode = new CureMode();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "CS_BenJieShiDui";
        _s[1] = "CS_YiZhiWeiShiQi";
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

        //当前所有随从的四维+10%；
        foreach (var _s in chara.GetComponentsInChildren<ServantAbstract>())
        {
            _s.atkMul += 0.1f;
            _s.defMul += 0.1f;
            _s.psyMul += 0.1f;
            _s.sanMul += 0.1f;
        }

        //为角色增加一个随从 
        //获得后随机获得一个丰容设施，即益智喂食器或者本杰士堆
        var _random = Random.Range(0,1);//int型，0或1
        if(_random==0)
            chara.AddServant("CS_BenJieShiDui");
        else
            chara.AddServant("CS_YiZhiWeiShiQi");

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
