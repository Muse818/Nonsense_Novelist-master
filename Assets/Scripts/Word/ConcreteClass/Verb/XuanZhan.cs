using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：宣战
/// </summary>
public class XuanZhan : AbstractVerbs
{
    static public string s_description = "使目标获得<color=#dd7d0e>嘲讽</color>，并使我方所有随从攻击速度提升30%，持续10s";
    static public string s_wordName = "宣战";
    static public int s_rarity = 3;
    public override void Awake()
    {
        base.Awake();
        skillID = 19;
        wordName = "宣战";
        bookName = BookNameEnum.PHXTwist;
        description = "使目标获得<color=#dd7d0e>嘲讽</color>，并使我方所有随从攻击速度提升30%，持续10s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange = new SingleSelector();

        skillEffectsTime = 10;
        rarity = 3;
        needCD = 6;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChaoFeng";
        return _s;
    }

    List<AbstractCharacter> servants = new List<AbstractCharacter>();
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);

        servants.Clear();

        //使目标或的人嘲讽
        if (useCharacter)
        {
            var _aims = skillMode.CalculateAgain(200, useCharacter);
            int x = 0;
            for (int i = 0; (i < _aims.Length) && (x < useCharacter.myState.aimCount); i++)
            {
              
                buffs.Add(_aims[i].gameObject.AddComponent<ChaoFeng>());
                buffs[0].maxTime = skillEffectsTime;
                x++;
            }

            //使我方所有随从加速
            if (useCharacter.Camp == CampEnum.left)
            {
                AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
                for (int i = 0; i < left.Length; i++)
                {
                    foreach (var _s in left[i].servants)
                    {
                        _s.GetComponent<AbstractCharacter>().attackSpeedPlus += 0.3f;
                        servants.Add(_s.GetComponent<AbstractCharacter>());
                    }
                }

            }
            else if (useCharacter.Camp == CampEnum.right)
            {
                AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
                for (int i = 0; i < right.Length; i++)
                {
                    foreach (var _s in right[i].servants)
                    {
                        _s.GetComponent<AbstractCharacter>().attackSpeedPlus += 0.3f;
                        servants.Add(_s.GetComponent<AbstractCharacter>());
                    }
                }
            }

            StartCoroutine(DeleteEffect());
           
            return;


        }

   



    }
    WaitForSeconds waitTen = new WaitForSeconds(10);
    IEnumerator DeleteEffect()
    {
        yield return waitTen;
        foreach (var _s in servants)
        {
            _s.GetComponent<AbstractCharacter>().attackSpeedPlus -= 0.3f;
            servants.Add(_s.GetComponent<AbstractCharacter>());
        }
    }
    public override void BasicAbility(AbstractCharacter useCharacter)
    {
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
        //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
