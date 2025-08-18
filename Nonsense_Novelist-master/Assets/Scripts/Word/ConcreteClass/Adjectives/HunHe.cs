using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词：混合的
/// </summary>
public class HunHe : AbstractAdjectives
{
    static public string s_description = "获得1个随机随从";
    static public string s_wordName = "混合的";
    static public int s_rarity = 2;
    public override void Awake()
    {
        adjID = 4;
        wordName = "混合的";
        bookName = BookNameEnum.ZooManual;
        description = "获得1个随机随从";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 12;
        rarity = 2;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        var _obj = this.GetComponent<AbstractCharacter>();
        int count = _obj.servants.Count;
        _obj.AddRandomServant();
        //if (count < 3)
        //{
        //    _obj.AddRandomServant();

        //}
        ////若有三个，合并
        //else if (count == 3)
        //{
        //    //var _s1 = this.GetComponentsInChildren<ServantAbstract>()[0];
        //    //var _s2 = this.GetComponentsInChildren<ServantAbstract>()[2];
        //    //var _s3 = this.GetComponentsInChildren<ServantAbstract>()[1];
        //    ////删除所有随从并且生成一个混养笼
        //    //_obj.DeleteServant(_s1.gameObject);
        //    //_obj.DeleteServant(_s2.gameObject);
        //    //_obj.DeleteServant(_s3.gameObject);

        //    //_obj.AddServant("CS_HunYangLong");
        //    //this.GetComponent<CS_HunYangLong>().SetInitNumber(_s1, _s2, _s3);
        //    _obj.ServantMerge();
        //}
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {

    }

}

