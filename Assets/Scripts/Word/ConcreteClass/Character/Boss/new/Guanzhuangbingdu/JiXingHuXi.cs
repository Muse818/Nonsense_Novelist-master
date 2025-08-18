using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiXingHuXi : AbstractVerbs
{ 
   
    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "急性呼吸综合征";
        bookName = BookNameEnum.FluStudy;
        
        description = "对随机的角色释放，使其获得“患病”，持续8s，并在第6秒时，向周围邻近的所有角色扩散该效果，" +
            "并使自身“晕眩”2s" +
            "被扩散的角色，同样获得8s患病，及2s晕眩，不再次扩散" +
            "PS：“患病”是可以叠加的";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;

        needCD = 3;//触发：3能量
    }

    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {

        base.UseVerb(useCharacter);
  
        print("急性呼吸  UseVerb");

        //随机找一个施法目标
        AbstractCharacter[] _aim = skillMode.CalculateRandom(attackDistance, useCharacter, true);


        //为这个施法目标增加患病的buff
        HuanBing_x _hb = gameObject.AddComponent<HuanBing_x>();
        buffs.Add(_hb);
        _hb.maxTime = 9f;
        _hb.effectTime = 8f;

        //累计使用患病的次数
        float _count = character.GetComponent<Boss_Guanzhuangbingdu>().HuanbingBuffCount;
        _count += 1;
        print("累计" + _count + "次患病效果了！");

        //增加8s和6s的计时

    }

    #region 8s和6s倒计时


    /// <summary>
    /// 8s倒计时
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFor8Seconds()
    {
        
            yield return new WaitForSeconds(8);
          
        
    }


    /// <summary>
    /// 6s倒计时
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFor6Seconds(AbstractCharacter _aim)
    {
        
            yield return new WaitForSeconds(8);
        //向周围邻近的所有角色扩散该效果，自身“晕眩”2s"
        Dizzy _hb = _aim.gameObject.AddComponent<Dizzy>();
        buffs.Add(_hb);
        _hb.maxTime = 2f;

    }


    #endregion

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n" + character.wordName + "将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
}
