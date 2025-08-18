
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_YiZhiWeiShiQi : ServantAbstract
{
    static public string s_description = "周期给队友提供“亢奋”";
    static public string s_wordName = "随从：益智喂食器";

    AbstractCharacter[] aims;
    Coroutine coroutineAttack = null;


    override public void Awake()
    {
        base.Awake();
       
        Destroy(attackA);
        attackA = gameObject.AddComponent<CureMode>();
        if (coroutineAttack != null) StopCoroutine(coroutineAttack);
        coroutineAttack = StartCoroutine(GiveBuffs());
    }
    WaitForSeconds wait = new WaitForSeconds(10);
    IEnumerator GiveBuffs()
    {
        while (true)
        {
             yield return wait;
            useBuff();
        }
      
    }


    public void useBuff()
    {
        print("useBuff");
        //每10s，给一个队友提供15s的“亢奋”
        //代替平A
        if (myState.aim != null)
        {
            for (int i = 0; i < myState.aim.Count; i++)
            {
                print("myState.aim != null"+myState.aim[i].wordName); 
                var buff=  myState.aim[i].gameObject.AddComponent<KangFen>();
                buff.maxTime = 15;
            }
                
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
          
         
        }
        print("myState.aim ==== null");
    }

    public override bool AttackA()
    {


        return false;
    }



    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<CS_YiZhiWeiShiQi>() != null)
            Destroy(masterNow.GetComponent<CS_YiZhiWeiShiQi>());
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
