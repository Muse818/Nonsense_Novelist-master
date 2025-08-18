using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：清晰的
/// </summary>
public class QingXi : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>共振，诗情</color>，持续10s，结束后净化3层减益效果";
    static public string s_wordName = "清晰的";
    static public int s_rarity = 2;
    public override void Awake()
    {
        adjID = 12;
        wordName = "清晰的";
        bookName = BookNameEnum.CrystalEnergy;
        description = "<color=#dd7d0e>共振，诗情</color>，持续10s，结束后净化3层减益效果";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 10;
        rarity = 2;
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[3];
        _s[0] = "GongZhen";
        _s[1] = "ShiQing";
        _s[2] = "XuWu_YunSu";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        buffs.Add(aimCharacter.gameObject.AddComponent<GongZhen>());
        buffs[0].maxTime = Mathf.Infinity;
        buffs.Add(aimCharacter.gameObject.AddComponent<ShiQing>());
        buffs[1].maxTime = skillEffectsTime;
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(skillEffectsTime);
        if (this.GetComponent<AbstractCharacter>() != null)
        {
            this.GetComponent<AbstractCharacter>().DeleteBadBuff(3);
        }
    }

    protected override void Update()
    {
        base.Update();

    }

    public override void End()
    {
        base.End();
        
    }

}
