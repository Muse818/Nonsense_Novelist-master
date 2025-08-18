using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：茶杯
/// </summary>
class ChaBei : AbstractItems
{
    static public string s_description = "<sprite name=\"psy\">+2";
    static public string s_wordName = "茶杯";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();

        itemID = 1;
        wordName = "茶杯";
        bookName = BookNameEnum.HongLouMeng;
        rarity = 1;
        useTimes = 6;
        VoiceEnum = MaterialVoiceEnum.Ceram;

        description = "<sprite name=\"psy\">+2";
     
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);  
        chara.psy += 2;
     
   
    }

    public override void UseVerb()
    {
        base.UseVerb();

    }

    public override void End()
    {
        base.End();
        aim.psy-= 2;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

            return character.wordName+"拿出了一个上大下小的碧绿方形茶杯。";
        
    }
}
