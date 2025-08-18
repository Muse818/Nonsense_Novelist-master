using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：通灵宝玉
/// </summary>
class TongLingBaoyu : AbstractItems
{
    static public string s_description = "<sprite name=\"psy\">+5，将受到伤害的30%，转移给血量最高的队友";
    static public string s_wordName = "通灵宝玉";
    static public int s_rarity = 4;
    static public int s_useTimes = 1;

    private bool openDelege = false;
    public override void Awake()
    {
        base.Awake();
        itemID = 3;
        wordName = "通灵宝玉";
        bookName = BookNameEnum.HongLouMeng;
        rarity = 4;
        useTimes = 1;
        VoiceEnum = MaterialVoiceEnum.materialNull;


        description = "<sprite name=\"psy\">+5，将受到伤害的30%，转移给血量最高的队友";


    }
    override public string[] DetailLable()
    {
        string[] _s = new string[0];

        return _s;
    }


    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (GetComponents<TongLingBaoyu>().Length <= 1)
        {//是最开始的一个，唯一增加的一个
           
            chara.event_BeAttack += SwitchDamage;
            openDelege = true;
        }
        chara.psy += 5;


    }

    public void AddDelege()
    {
        if (!openDelege)
        {
           
            this.GetComponent<AbstractCharacter>().event_BeAttack += SwitchDamage;
            openDelege = true;
        }
    }


    void SwitchDamage(float _damage, AbstractCharacter _whoDid)
    { 
        //如果装备多个，则会叠加转移伤害的百分比，最高100%
        var count = GetComponents<TongLingBaoyu>().Length;
        var rate = Mathf.Clamp(0.3f * count,0,1);

        print(count);
        //寻找血量最高的队友
        IAttackRange attackRange = new SingleSelector();
        AbstractCharacter[] a = attackRange.CaculateRange(200, this.GetComponent<AbstractCharacter>().situation, NeedCampEnum.friend, false);
        CollectionHelper.OrderByDescending(a, p => p.hp);

        //避免转移给自己，嵌套委托
        int _count = -1;
        for (int x = 0; (x < a.Length)&&(_count==-1); x++)
        {
            if (a[x] != this.GetComponent<AbstractCharacter>()) _count = x;
        }

        //如果没有队友了，就不转移
        if (_count == -1) return;

        //转移血量
        a[_count].BeAttack(AttackType.dir, _damage* rate, true, 0, _whoDid);

        //自己受伤（把血量加回来）4
        this.GetComponent<AbstractCharacter>().BeCure(_damage * rate, true, 0, _whoDid);
    }
  
    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        if (openDelege)
        {//是最开始的一个且是最后一个
            var other = GetComponents<TongLingBaoyu>();
            if (other.Length <= 1)
            {
                this.GetComponent<AbstractCharacter>().event_BeAttack -= SwitchDamage;
            }
            else //是最开始的一个且不是最后一个
            {
                int x = 0;
                while (other[x] == this) x++;
                other[x].AddDelege();
            }
        }
        aim.psy -= 5;
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
