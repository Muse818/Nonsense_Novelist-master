using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：下雨的
/// </summary>

public class XiaYuDe : AbstractAdjectives, IJiHuo
{
    static public string s_description = "未激活：<color=#dd7d0e>木讷</color>，持续10s；激活：净化全队1层减益效果";
    static public string s_wordName = "下雨的";
    static public int s_rarity = 1;
    /// <summary>是否激活共振 </summary>
    private bool jiHuo;

    public override void Awake()
    {

        adjID = 31;
        wordName = "下雨的";
        bookName = BookNameEnum.ElectronicGoal;
        description = "未激活：<color=#dd7d0e>木讷</color>，持续10s；激活：净化全队1层减益效果";


        skillEffectsTime = 10;

        rarity = 1;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();

    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "MuNe";
        _s[1] = "JiHuo";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo = value;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        if (jiHuo)
        {
            if (aimCharacter.Camp == CampEnum.left)
            {
                AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
                for (int i = 0; i < left.Length; i++)
                {
                    left[i].DeleteBadBuff(1);
                }

            }
            else if (aimCharacter.Camp == CampEnum.right)
            {
                AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
                for (int i = 0; i < right.Length; i++)
                {
                    right[i].DeleteBadBuff(1);
                }
            }
         
        }
        else
        {
            buffs.Add(aimCharacter.gameObject.AddComponent<MuNe>());
            buffs[0].maxTime = skillEffectsTime;
        }

       
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {


    }



    public override void End()
    {
        base.End();

    }

}
