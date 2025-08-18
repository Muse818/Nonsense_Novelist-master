using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//huaiyizhuyiBOSS专属动词
//在boss血量过半时此脚本挂在boss上。
//效果是：对随机的角色A释放，让其不断攻击随机角色B，持续5秒，伤害结果降低50%
//结束后，被攻击者B随机攻击角色C（角色可以反复获得，即C也可以是A），效果一致，依据此循环，直到boss死亡
//简单来说，就是一个会无限传导的仇恨
//触发方式是：boss血量为50%时
public class Caiyilian : AbstractVerbs
{

    //只用一次。
    bool isUsed = false;
    
    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "猜疑链";
        bookName = BookNameEnum.HongLouMeng;
        description = "让被释放此技能的角色，不断攻击友方或敌方任意角色，持续5秒，伤害结果降低50%";
        skillMode = gameObject.AddComponent<DamageMode>();
        skillEffectsTime = 5;
        rarity = 3;
        needCD = 0;
    }

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
        AbstractCharacter[] _aim= skillMode.CalculateRandom(attackDistance, useCharacter,true);


        //为这个施法目标增加CAIYI的buff
        var _b = _aim[0].gameObject.AddComponent<CaiYi>();
        buffs.Add(_b);
        _b.maxTime = skillEffectsTime;

        //打开只用一次的开关
        isUsed = true;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n"+character.wordName+"将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
}