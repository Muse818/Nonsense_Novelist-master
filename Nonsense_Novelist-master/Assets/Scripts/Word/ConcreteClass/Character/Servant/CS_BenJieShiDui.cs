
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BenJieShiDui : ServantAbstract
{
    static public string s_description = "普通攻击治疗队友";
    static public string s_wordName = "随从：本杰士堆";

    override public void Awake()
    {
        base.Awake();
        
        Destroy(attackA);
        attackA = gameObject.AddComponent<CureMode>();
    }

    AbstractCharacter[] aims;
    public override bool AttackA()
    {
   
        //代替平A
        if (myState.aim != null)
        {
            print("随从"+wordName+"的目标是"+ myState.aim[0].name);
            for (int x = 0; x < myState.aim.Count; x++)
            {
                    myState.character.CreateBullet(myState.aim[x].gameObject);
                attackA.UseMode(AttackType.heal, san * sanMul * 1f, myState.character, myState.aim[x], true, 0);
            }
        
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            //普通攻击目标为血量百分比最低的队友，恢复10血量（待定）
            //myState.aim.CreateFloatWord(
            //attackA.UseMode(myState.character, san * sanMul * 1f, myState.aim)
            //, FloatWordColor.heal, false);
           
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<ShiWuFengRong>() != null)
            Destroy(masterNow.GetComponent<ShiWuFengRong>());
    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "早已看见多了一个妹妹，细看形容，只见泪光点点，娇喘微微，闲静时如姣花照水，行动处似弱柳扶风，" + otherChara.wordName + "笑道：“这个妹妹，我曾见过的”";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "“我就知道，别人不挑剩下的也不给我。”林黛玉轻捻一朵花瓣，向" + otherChara.wordName + "飞去";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "黛玉对侍女喘息道：“笼上火盆罢。”便将一对帕子，一叠诗稿焚尽于火盆中。";
    }
    public override string DieText()
    {
        return "“宝玉…宝玉…你好……”黛玉没说完便合上了双眼。";
    }


}

