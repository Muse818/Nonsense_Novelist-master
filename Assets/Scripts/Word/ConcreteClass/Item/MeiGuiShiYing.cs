using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：玫瑰石英
/// </summary>
class MeiGuiShiYing : AbstractItems,IJiHuo
{
    static public string s_description = "未激活，<sprite name=\"def\">-2；\n激活，<sprite name=\"def\"> + 6，获得<color=#dd7d0e>共振</color>";
    static public string s_wordName = "玫瑰石英";
    static public int s_rarity = 2;
    static public int s_useTimes = 4;

    /// <summary>是否激活共振 </summary>
    private bool jiHuo;
    private float record;
    public override void Awake()
    {
        base.Awake();
        itemID = 12;
        wordName = "玫瑰石英";
        bookName = BookNameEnum.CrystalEnergy;
        description = "未激活，<sprite name=\"def\">-2；\n激活，<sprite name=\"def\"> + 6，获得<color=#dd7d0e>共振</color>";

        VoiceEnum = MaterialVoiceEnum.Ceram;
        useTimes = 4;
        rarity = 2;
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "JiHuo";
        _s[1] = "GongZhen";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo= value;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (jiHuo)
        {
            record = 6;
            chara.def += record;

            buffs.Add(gameObject.AddComponent<GongZhen>());
            buffs[0].maxTime = Mathf.Infinity;
        }
        else
        {
            record = -2;
            chara.def += record;
        }
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.def -= record;
    }

   
}
