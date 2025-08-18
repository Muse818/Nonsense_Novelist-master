using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物：放射性微尘
/// </summary>
public class SaiBoFengZi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 111;
        wordName = "赛博疯子";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "暂无文案";
        description = "暂无文案";

        //读取数据
        MonsterExcelItem dataD = null;
        MonsterExcelItem data = null;
        for (int i = 0; (i < AllData.instance.monsterDate.items.Length) && (data == null); i++)
        {
            print("sdsd");
            var _data = AllData.instance.monsterDate.items[i];
            if (_data.Mid == characterID)
            {
                print(_data.Mid + ":" + _data.name+"=?" + GameMgr.instance.GetStage());
                if (dataD == null) dataD = _data;
                if (_data.name == GameMgr.instance.GetStage())
                {
                    print(_data.name + "==" + GameMgr.instance.GetStage());
                    data = _data;
                }
            }
        }
        if (data == null)
            data = dataD;
        if (data == null)
            return;
        print(GameMgr.instance.GetStage());
        print("读取成功：" + wordName + data.name);

        //数值
        hp = maxHp = data.hp;
        atk = data.atk;
        def = data.def;
        psy = data.psy;
        san = data.san;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "战士";
        roleInfo = "不分敌我地快速攻击";//攻击间隔1s，常驻“疯狂”状态，不会被移除，每次不分敌我攻击随机目标
        cureHpRate += 0.05f;

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
            return "阿努比斯出场文本";
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
