using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuBian : AbstractVerbs
{
    //只用一次。
    bool isUsed = false;

    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "突变";
        bookName = BookNameEnum.HongLouMeng;
        description = "";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 3;
        needCD = 0;

        //重置所有技能的一次性开关
        for (int _i = 0; _i < hasUsed.Length; _i++)
        {
            hasUsed[_i] = false;
        }
    }

    ///随机发生以下中的一种，boss获得永久提升，已有的不重复触发
    #region Boss的四种技能

    /// <summary>0免疫逃逸-1超级传播-2严重伴随症-3致死率提升 </summary>
    bool[] hasUsed = new bool[4];
    bool ignoredDEF = false;
    bool chuanranTwice = false;
    bool dizzyDelay = false;

    /// <summary>
    /// 0免疫逃逸：冠状病毒造成的所有伤害将无视防御力
    /// </summary>
    private void E_MianYiTaoYi()
    {
        if (hasUsed[0])
        {
            return;
        }
        ignoredDEF = true;
        hasUsed[0] = true;
    }


    /// <summary>
    /// 1超级传播：急性呼吸综合症将进行第二次扩散，第三轮被扩散的角色才会不继续扩散，且过程中可以反复感染
    /// </summary>
    private void E_ChaoJiChuanBo()
    {
        if (hasUsed[1])
        {
            return;
        }
        chuanranTwice = true;
        hasUsed[1] = true;
    }


    /// <summary>
    /// 2严重伴随症：急性综合呼吸症，将从第4s开始晕眩，持续到第8s，增加了2s
    /// </summary>
    private void E_YanZhongBanSuiZheng()
    {
        if (hasUsed[2])
        {
            return;
        }
        dizzyDelay = true;
        hasUsed[2] = true;
    }


    /// <summary>
    /// 3致死率提升：场上有“患病”的角色，每3s有10%几率受到10点物理伤害*/
    /// </summary>
    private void E_ZhiSiLvTiSheng()
    {

    }

    #endregion


    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {

        base.UseVerb(useCharacter);
        if (isUsed)
            return;
        print("caiyilian  UseVerb");

        //随机找一个施法目标
        AbstractCharacter[] _aim = skillMode.CalculateRandom(attackDistance, useCharacter, true);


        //为这个施法目标增加CAIYI的buff
        buffs.Add(_aim[0].gameObject.AddComponent<CaiYi>());
        buffs[0].maxTime = skillEffectsTime;

        //打开只用一次的开关
        isUsed = true;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n" + character.wordName + "将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
}
