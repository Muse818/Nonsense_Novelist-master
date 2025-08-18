using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 社会化的
/// </summary>
public class SheHuiHua : AbstractAdjectives,IJiHuo
{

    /// <summary>是否激活共振 </summary>
    private bool jiHuo;
    static public string s_description = "未揭示，召唤2只<color=#dd7d0e>工蚁</color>；揭示，召唤2只<color=#dd7d0e>工蚁</color>和1个随机随从";
    static public string s_wordName = "社会化的";
    static public int s_rarity = 3;
    public override void Awake()
    {
        adjID = 18;
        wordName = "社会化的";
        bookName = BookNameEnum.FluStudy;
        description = "未揭示，召唤2只<color=#dd7d0e>工蚁</color>；揭示，召唤2只<color=#dd7d0e>工蚁</color>和1个随机随从";
        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 20;
        rarity = 3;
        
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
        
      
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "JiHuo";
        _s[1] = "CS_GongYi";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo = value;
    }
   
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        aimCharacter.AddServant("CS_GongYi");
        aimCharacter.AddServant("CS_GongYi");
        if (jiHuo)
        {
            aimCharacter.AddRandomServant();
        }


    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
    }

}
