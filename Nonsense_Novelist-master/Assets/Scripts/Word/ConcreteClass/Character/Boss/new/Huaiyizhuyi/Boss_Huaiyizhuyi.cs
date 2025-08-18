using AI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

class Boss_Huaiyizhuyi : AbstractCharacter
{
    private bool hasLowThanHalf = false;

    private GameObject hpslider;
    private GameObject[] skillText=new GameObject[3];
    private GameProcessSlider gameProcess;
override public void Awake()
    {
        base.Awake();
        

        situation = GameObject.Find("Circle4.5").GetComponentInChildren<Situation>();
        if (situation == null)
            print("situation5.5==null");
    }

    
    private void Start()
    {
       


        //将boss的血条和技能条移动到主界面来
        Transform _cc = GameObject.Find("combatCanvas").transform;
        hpslider = GetHpSlider().gameObject;
        hpslider.transform.parent = _cc;
        hpslider.transform.localScale = Vector3.one * 0.6f;
        hpslider.transform.localPosition = Vector3.zero+new Vector3(-10,470,0);

        skillText = GetSkillText();
        for (int _st=0;_st<skillText.Length;_st++)
        {
            skillText[_st].transform.parent = _cc;
            skillText[_st].transform.localScale = Vector3.one * 0.3f;
            skillText[_st].transform.localPosition = Vector3.zero + new Vector3(-80+80*_st, 375, 0);
        }
        gameProcess = _cc.GetComponentInChildren<GameProcessSlider>();



        skillMode = gameObject.AddComponent<DamageMode>();
        
        //增加半血检测
        myState.event_EveryZeroOne += IsHpLowThanHalf;

        //增加自带技能
       this.AddVerb(gameObject.AddComponent<RenZhiGuHua>());

        var _aims  = CharacterManager.instance.GetComponentsInChildren<AbstractCharacter>();
        //夺取意志
        //夺取场上每个敌人3点意志力，加在自己身上
        foreach (var aim in _aims)
        {
            if(aim.transform.parent.parent.GetComponent<Situation>() != null)
            { 
                if ( aim.transform.parent.parent.GetComponent<Situation>().number!=5.5f)
                {
       
                    if (aim.san >= 3)
                    {
                        record = aim.san;
                        aim.san -= 3;
                        record = record - aim.san;
                        san += record;
                    }
                    else
                    {    
                        record = aim.san;
                        aim.san =0;
                        record = record - aim.san;
                        san += record;
                    } 
                }
            }
           
        }
    }

     AbstractSkillMode skillMode;
    AbstractCharacter[] aims;
    
    float record;

    public void IsHpLowThanHalf()
    {
        if (hasLowThanHalf)
        {
            myState.event_EveryZeroOne -= IsHpLowThanHalf;
            return;
        }
       
        if (hp <= (maxHp/2 ))
        {
            print("IsHpLowThanHalf++trigger");
            hasLowThanHalf = true;

            //血量过半时，为自身增加caiyilian的技能
            this.AddVerb(gameObject.AddComponent<Caiyilian>());

        }
    }



    private void OnDestroy()
    {
        print("1");
        gameProcess.BossDie();
        Destroy(CharacterManager.instance.gameObject.GetComponentInChildren<CaiYi>());
        Destroy(hpslider);
        Destroy(skillText[0]);
        Destroy(skillText[2]);
        Destroy(skillText[1]);

    }
    //public override bool AttackA()
    //{
    //    if (base.AttackA())
    //    {
    //        aims = skillMode.CalculateAgain(100, this);
    //        //夺取场上每个敌人5点意志力，加在自己身上
    //        foreach (AbstractCharacter aim in aims)
    //        {
    //            record = aim.san;
    //            aim.san -= 5;
    //            record = record - aim.san;
    //            san += record;
    //        }
    //        return true;s
    //    }
    //    return false;
    //}


    //public override bool AttackA()
    //{

    //    if (base.AttackA())
    //    {
    //        print("boss怀疑主义的大招释放了");
    //        /*对随机的角色A释放，让其不断攻击随机角色B，持续5秒，伤害结果降低50%
    //        结束后，被攻击者B随机攻击角色C（角色可以反复获得，即C也可以是A），效果一致，依据此循环，直到boss死亡
    //        简单来说，就是一个会无限传导的仇恨*/
    //        //随机找一个aim
    //        aims = skillMode.CalculateAgain(100, this);
    //        //给aim一个特殊buff，让其攻击B

    //        AbstractAdjectives adj = aims[0].gameObject.AddComponent<Caiyilian>();
    //        adj.UseAdj(aims[0].gameObject.GetComponent<AbstractCharacter>());
    //        //在增加了猜疑链buff后，角色寻找目标的机制会变成随机
    //        return true;
    //    }
    //    return false;

    //}
    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) && (type == GrowType.psy))
        {
            hasAddGrow.Add(GrowType.psy);
            string it = "那天渐渐的黄昏，且阴的沉重，兼着那雨滴竹梢，更觉凄凉，黛玉不觉心有所感，亦不禁发于章句，遂成诗一首。";
            GameMgr.instance.draftUi.AddContent(it);
            return it;
        }



        return null;
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
