
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MG42gun : ServantAbstract
{
    static public string s_description = "攻速更快，需要换弹";
    static public string s_wordName = "随从：MG-42机枪";

    //连续攻击的次数
    private int attackCountMax=0;
    private int attackCountNow = 0;
    private int attackWait = 0;
    private float attackWaitTime = 0;
    override public void Awake()
    {
        base.Awake();
      

        //攻击间隔为1s，每进行8次攻击，停止攻击5s
        attackInterval = 1f;
        attackCountMax = 8;
        attackWaitTime = 5f;

        Destroy(attackA);
        attackA = gameObject.AddComponent<DamageMode>();
    }



    //替代平A
    public override bool AttackA()
    {

        if (attackWait >= attackWaitTime)
        {
            attackCountNow = 0;
            attackWait = 0;
            return true;
        }

        if (attackCountNow >= attackCountMax)
        {
            attackWait += 1;
            return true;
        }
        //base:
        if (myState.aim != null)
        { if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            for (int x = 0; x < myState.aim.Count; x++)
            {
                 myState.character.CreateBullet(myState.aim[x].gameObject); 
               
            //myState.aim.CreateFloatWord(
            //    attackA.UseMode(myState.character, myState.character.atk * (1 - myState.aim.def / (myState.aim.def + 20)), myState.aim)
            //    , FloatWordColor.physics, false);
            attackA.UseMode(AttackType.atk, myState.character.atk, myState.character, myState.aim[x], true, 0);
            
            }
           attackCountNow += 1;
           
            return true;
        }
        return false;
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
