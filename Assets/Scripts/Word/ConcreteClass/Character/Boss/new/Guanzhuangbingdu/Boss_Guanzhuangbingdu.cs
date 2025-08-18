using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// boss：冠状病毒
/// </summary>
public class Boss_Guanzhuangbingdu : AbstractCharacter
{
    private bool hasLowThanHalf = false;

    public float HuanbingBuffCount = 0;


    override public void Awake()
    {
        base.Awake();
        characterID = 0;
        wordName = "冠状病毒";
        bookName = BookNameEnum.FluStudy;
        gender = GenderEnum.noGender;
        Camp = CampEnum.stranger;

        hp = maxHp = 600;//600
        atk = 20;//20
        def = 30;//30
        psy = 20;//20
        san = 30;//30
        maxSkillsCount = 3;

        trait = gameObject.AddComponent<Sentimental>();
        roleName = "呼吸道传播";

        attackInterval = 2.2f;
        attackDistance = 200;

        brief = "";
        description = "";


        situation = GameObject.Find("Circle4.5").GetComponentInChildren<Situation>();
        if (situation == null)
            print("situation5.5==null");
    }


    AbstractSkillMode skillMode;
    private void Start()
    {
        skillMode = gameObject.AddComponent<DamageMode>();

     
        //增加初始的技能
        this.AddVerb(gameObject.AddComponent<JiXingHuXi>());
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
