using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：快速成长的
/// </summary>
public class QuicklyGrowing : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"hp\">恢复30";
    static public string s_wordName = "快速成长的";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 22;
        wordName = "快速成长的";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"hp\">恢复30";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 1;
        time = skillEffectsTime;
        base.Awake();


    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter); aimCharacter.BeCure(30, true, 0, aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
       
    }

    float time;
    protected override void Update()
    {
        base.Update();
       
    }

    public override void End()
    {

        base.End();
    }

}
