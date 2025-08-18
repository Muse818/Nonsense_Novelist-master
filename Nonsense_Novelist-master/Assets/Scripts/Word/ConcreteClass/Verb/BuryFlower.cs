using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// 动词：葬花
/// </summary>
class BuryFlower : AbstractVerbs
{
    static public string s_description = "被动：普通攻击使对方获得<color=#dd7d0e>花瓣</color>;\n主动：收回所有<color=#dd7d0e>花瓣</color>，并造成<color=#dd7d0e>花瓣</color>数 * 30 % <sprite name=\\\"psy\\\">的伤害";
    static public string s_wordName = "葬花";
    static public int s_rarity = 4;

    Dictionary<AbstractCharacter,int> aimDic=new Dictionary<AbstractCharacter, int>();
    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "葬花";
        bookName = BookNameEnum.HongLouMeng;
        description = "被动：普通攻击使对方获得<color=#dd7d0e>花瓣</color>;\n主动：收回所有<color=#dd7d0e>花瓣</color>，并造成<color=#dd7d0e>花瓣</color>数 * 30 % <sprite name=\"psy\">的伤害";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity = 4;
        needCD = 7;

       
        if (character == null) return;

        //被动攻击效果
        character.event_AttackA += GiveHuaban;
    
        aimDic.Clear();
    }

    public void GiveHuaban()
    {
        for (int x = 0; x < character.myState.aim.Count; x++)
        {
            character.myState.aim[x].gameObject.AddComponent<HuaBan>();
            if (!aimDic.ContainsKey(character.myState.aim[x]))
            {
                aimDic.Add(character.myState.aim[x], 1);
            }
            else
            {
                aimDic[character.myState.aim[x]]+= 1;
            }
        }

    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "HuaBan";
        return _s;
    }



    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
       

        var psy = useCharacter.psy * useCharacter.psyMul;
        // 主动：收回所有 < color =#dd7d0e>花瓣</color>，并造成<color=#dd7d0e>花瓣</color>数 * 30 % <sprite name=\"psy\">的伤害
        foreach (var _aim in aimDic)
        {
            var huabans = _aim.Key.GetComponents<HuaBan>();
            float count = _aim.Value * 0.3f * psy;
            if (!useCharacter.isNaiMa)
            {   //不是一个阵营的时候，才伤血（？）
                _aim.Key.BeAttack(AttackType.dir, count, true, 0, useCharacter);
            }
            for(int x=0; (x<huabans.Length)&&(x<_aim.Value);x++) 
            {
                Destroy(huabans[x]);
            } 
        }


        aimDic.Clear();
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
        if (character == null) return;

        character.event_AttackA -= GiveHuaban;
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n"+character.wordName+"将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
    
}
