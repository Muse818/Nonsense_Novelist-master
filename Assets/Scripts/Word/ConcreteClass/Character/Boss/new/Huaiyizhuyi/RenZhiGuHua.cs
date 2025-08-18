using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenZhiGuHua : AbstractVerbs
{
    static public string s_description = "随机让几个人在一定时间内有收益，并在收益结束时伤害施法者。";
    static public string s_wordName = "认知固化";

    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "认知固化";
        bookName = BookNameEnum.HongLouMeng;
        description = "随机让几个人在一定时间内有收益，并在收益结束时伤害施法者。";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 3;
        needCD = 4;
        attackDistance = 100;
      
    }

    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
      

        //随机_aimCount个施法目标
        int _aimCount = 3;
        AbstractCharacter[] _randomCharacter= skillMode.CalculateRandom(attackDistance, useCharacter,true);

        if (_aimCount <_randomCharacter.Length)
        {//场上角色大于3个时，随机抽取
            //确保施法目标不重复
            List<int> _array = new List<int>();
            for (int i = 0; i < _aimCount; i++)
            {
                int number = Random.Range(0, _randomCharacter.Length);
                while (_array.Contains(number))
                {
                    number = Random.Range(0, _randomCharacter.Length);
                }
                _array.Add(number);
                var _b = _randomCharacter[_array[i]].gameObject.AddComponent<Renzhiguhua_buff>();
                buffs.Add(_b);
                _b.maxTime = 10;
            }
        }
        else 
        { //场上角色小于三个时，全部赋予。
         
            for (int i = 0; i < _randomCharacter.Length; i++)
            {
                var _b = _randomCharacter[i].gameObject.AddComponent<Renzhiguhua_buff>();
                buffs.Add(_b);
                _b.maxTime = 10;
            }
        }
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n" + character.wordName + "将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
}
