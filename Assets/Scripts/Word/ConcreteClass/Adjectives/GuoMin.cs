using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：过敏的
/// </summary>
public class GuoMin : AbstractAdjectives,IChongNeng
{
    static public string s_description = "color=#dd7d0e>充能</color>，每次弹射，让角色获得一层随机减益状态，持续10s";
    static public string s_wordName = "过敏的";
    static public int s_rarity = 2;

    private float dizzyAdd;

    public override void Awake()
    {        
        base.Awake();
        adjID = 16;
        wordName = "过敏的";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>充能</color>，每次弹射，让角色获得一层随机减益状态，持续10s";

        skillMode = gameObject.AddComponent<SelfMode>();

        skillEffectsTime = 0;
        rarity = 2;


        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChongNeng>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChongNeng";
        return _s;
    }

    int times;
    public void ChongNeng(int _times)
    {
        dizzyAdd += 0.5f*_times;
        times = _times;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        for (int i = 0; i < times; i++)
        {        
            var count = UnityEngine.Random.Range(0,AllSkills.BadBuff.Count);
            Type _t = AllSkills.BadBuff[count];
            var buff=aimCharacter.gameObject.AddComponent(_t) as AbstractBuff; ;
       
            buff.maxTime=10f;
        }
       
        
        //buffs.Add();
        //_b.maxTime =9 /*skillEffectsTime + dizzyAdd*/;
      
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }
    

    public override void End()
    {
        base.End();
    }

    
}
