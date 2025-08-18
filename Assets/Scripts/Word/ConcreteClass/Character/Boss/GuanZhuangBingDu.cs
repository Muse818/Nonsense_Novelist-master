using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class GuanZhuangBingDu : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 0;
        wordName = "冠状病毒";
        bookName = BookNameEnum.allBooks;
        gender = GenderEnum.noGender;
        Camp = CampEnum.stranger;
        hp =maxHp  = 600;
        atk = 30;
        def = 60;
        psy = 25;
        san = 60;
        trait=gameObject.AddComponent<Vicious>();
        roleName = "思潮";
        attackInterval = 2.2f;

        nowTime = 0;
    }

    private void OnEnable()
    {
        cool = false;
        skillMode = gameObject.AddComponent<DamageMode>();
        allEnemy = skillMode.CalculateAgain(100, this);
        randomEnemy = allEnemy[Random.Range(0, allEnemy.Length)];   
    }

    AbstractSkillMode skillMode;

    float nowTime;
    bool cool;
    AbstractCharacter[] allEnemy;
    AbstractCharacter randomEnemy;
    private void Update()
    {
        nowTime += Time.deltaTime;
        if(cool && nowTime>1)
        {
            nowTime = 0;
            cool = true;
            if (randomEnemy != null)
            {
                //myState.aim.CreateFloatWord(
                //    skillMode.UseMode(myState.character, 10 * (1 - myState.aim.def / (myState.aim.def + 20)), myState.aim)
                //    ,FloatWordColor.physics,false);
                skillMode.UseMode(AttackType.atk, 10, myState.character, myState.aim[0], true, 0);
            }
            else
            {
                cool = true;
            }
        }
        if(!cool && nowTime>30)
        {
            nowTime = 1;
            allEnemy = skillMode.CalculateAgain(100, this);
            randomEnemy = allEnemy[Random.Range(0, allEnemy.Length)];
            cool = false;
        }
    }
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
