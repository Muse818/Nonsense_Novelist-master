using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词： 紫水晶
/// </summary>
class ZiShuiJIng: AbstractItems,IJiHuo
{

    static public string s_description = "未激活，<sprite name=\"psy\">-1；\n激活，<sprite name=\"psy\"> + 3，获得<color=#dd7d0e>共振</color>";
    static public string s_wordName = "紫水晶";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    /// <summary>是否激活共振 </summary>
    private bool jiHuo;
    private float record;


    public override void Awake()
    {
        base.Awake();
        itemID = 10;
        wordName = "紫水晶";
        bookName = BookNameEnum.CrystalEnergy;
        description = "未激活，<sprite name=\"psy\">-1；\n激活，<sprite name=\"psy\"> + 3，获得<color=#dd7d0e>共振</color>";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 1;
        useTimes = 6;
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "GongZhen";
        _s[1] = "JiHuo";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo = value;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (jiHuo)
        {
            record = 3;
            chara.psy += record;
            buffs.Add(gameObject.AddComponent<GongZhen>());
            buffs[0].maxTime = Mathf.Infinity;
        }
        else
        {
            record = -1;
            chara.psy += record;
        }
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.psy -= record;
    }

    
}
