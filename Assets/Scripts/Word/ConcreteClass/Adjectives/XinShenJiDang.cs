using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：心神激荡的
/// </summary>
public class XinShenJiDang : AbstractAdjectives,IChongNeng
{
    static public string s_description = "每次碰撞获得2<sprite name=\"psy\">，并获得<color=#dd7d0e>颠倒</color>";
    static public string s_wordName = "心神激荡的";
    static public int s_rarity = 2;
    private int psyAdd;

    public override void Awake()
    {
        adjID = 9;
        wordName = "心神激荡的";
        bookName = BookNameEnum.Salome;
        description = "每次碰撞获得2<sprite name=\"psy\">，并获得<color=#dd7d0e>颠倒</color>";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity = 2;
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChongNeng>();
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ChongNeng";
        _s[1] = "DianDao";
        return _s;
    }


    /// <summary>
    /// 每次弹射，增加2精神，持续10s（这个叠加只加精神 持续时间续不增加|| 无碰撞直接发射 就是0）
    /// </summary>
    /// <param name="times"></param>
    public void ChongNeng(int times)
    {
        psyAdd += times;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
  
        buffs.Add(aimCharacter.gameObject.AddComponent<DianDao>());
        buffs[0].maxTime = skillEffectsTime;
      aimCharacter.psy += 2 * psyAdd;
        BasicAbility(aimCharacter);

    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
 
    }

    public override void End()
    {
        aim.psy -= psyAdd* 2  ;
        base.End(); 
       
    }
    
}
