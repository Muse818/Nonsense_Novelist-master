using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 名词：礼物
/// </summary>
public class LiWu : AbstractItems, IJiHuo
{
    static public string s_description = "<sprite name=\"atk\">+1";
    static public string s_wordName = "礼物";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 24;
        wordName = "礼物";
        useTimes = 6;
        bookName = BookNameEnum.Salome;
        description = "攻击一定次数后消除自身，随机获得一个珍贵的名词。未激活，攻击40次;激活，攻击20次";
        //效果需要在角色身上时才开始计算，且多个礼物独立计算
        //珍贵的名词包括3级和4级名词
        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 1;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
    }

    int attackTimes = 0;
    bool hasUsed = false;
    bool hasAdd= false;
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "JiHuo";
        return _s;
    }
    /// <summary>是否激活共振 </summary>
    private bool jiHuo;
    public void JiHuo(bool value)
    {
        jiHuo = value;
    }


    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);

        if (hasUsed) return;

        if (jiHuo) attackTimes = 20;
        else attackTimes = 2;

        //增加攻击的委托事件
        chara.event_AttackA += AttackOnce;
        hasUsed = true;

    }
    void AttackOnce()
    {
        if (hasAdd) return;

        attackTimes -= 1;
        if (attackTimes <= 0)
        {
            List<Type> _list = new List<Type>();
            _list.AddRange(AllSkills.Rare_3); _list.AddRange(AllSkills.Rare_4);
            int _randomR = UnityEngine.Random.Range(0, _list.Count);
           
            int _loop = 0;
            while ((!(AllSkills.list_noun.Contains(_list[_randomR])))&&(_loop<100))
            {
                _randomR = UnityEngine.Random.Range(0, _list.Count);
                _loop++;
            }
            this.gameObject.AddComponent(_list[_randomR]);
            hasAdd = true; 
            Destroy(this);
        }
    }





    public override void End()
    {
        if(hasUsed) aim.event_AttackA -= AttackOnce;
        base.End();
    }
}
