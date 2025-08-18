using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：荷鲁斯之眼
/// </summary>
class herusizhiyan : AbstractItems
{
    static public string s_description = "<sprite name=\"atk\">+4,每次复活攻击+2";
    static public string s_wordName = "荷鲁斯之眼";
    static public int s_rarity = 3;
    static public int s_useTimes = 2;
    int record = 0;
    public override void Awake()
    {
        base.Awake();

        itemID = 7;
        useTimes = 2;
        wordName = "荷鲁斯之眼";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"atk\">+4,每次复活攻击+2";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 3;

       
    }
    float _atk = 0;
    AI.NoHealthTrigger _nht=null;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        _atk = chara.atk+4;
        chara.atk += 4;

        //复活
        //也可能是idle状态变成死亡状态，考虑给idle加。
        var _list = chara.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
        foreach (var _t in _list)
        {
            if(_t.GetType()==typeof(AI.NoHealthTrigger))
            { (_t as AI.NoHealthTrigger).event_relife += RelifeStronger; _nht = (_t as AI.NoHealthTrigger); }
           
        }
       

    }


    void RelifeStronger(AbstractCharacter ac)
    { 
        GetComponent<AbstractCharacter>().atk += 2;
        record++;
    }


    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
        aim.atk -= 4;
        aim.atk -= record;

        if (_nht != null)
        { 
            _nht.event_relife -= RelifeStronger;
        }
      
    }
}
