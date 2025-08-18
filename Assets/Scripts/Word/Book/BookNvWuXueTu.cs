using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 女巫学徒转范文（挂摄像机上,每次打斗结束后调用）
/// </summary>
class BookNvWuXueTu : AbstractBook
{

    /// <summary>
    /// 调用此方法即可
    /// </summary>
    /// <param name="character">章节数1.2.(0为开场介绍)</param>
    /// <param name="part">第1.2.幕(0为章节标题)</param>
    /// <param name="phase">1开场2战场3结束</param>
    /// <returns></returns>
    public override string GetText(int character,int part,int phase)
    {
         if (character == 0)
                return StartText();
         if (character==1)
            {
                if (part == 0)
                    return "第一章 不速之客";
                if(part == 1)
                {
                    switch(phase)
                    {
                        case 1:
                            return First_1_Start();
                        case 2:
                            return First_1_Fight();
                        case 3:
                            return First_1_End();
                    default:
                        return null;
                    }
                }
                if(part == 2)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
                else
                     return null;
            }
        if (character == 2)
        {
            if (part == 0)
                return "第二章 密特拉";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Second_1_Start();
                    case 2:
                        return Second_1_Fight();
                    case 3:
                        return Second_1_End();
                    default:
                        return null;
                }
            }
            if (part == 2)
            {
                switch (phase)
                {
                    case 1:
                        return Second_2_Start();
                    case 2:
                        return Second_2_Fight();
                    case 3:
                        return Second_2_End();
                    default:
                        return null;
                }
            }
            if (part == 3)
            {
                switch (phase)
                {
                    case 1:
                        return Second_3_Start();
                    case 2:
                        return Second_3_Fight();
                    case 3:
                        return Second_3_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 3)
        {
            if (part == 0)
                return "第三章 沉默的森林";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Third_1_Start();
                    case 2:
                        return Third_1_Fight();
                    case 3:
                        return Third_1_End();
                    default:
                        return null;
                }
            }
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Second_1_Start();
                    case 2:
                        return Second_1_Fight();
                    case 3:
                        return Second_1_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 4)
        {
            if (part == 0)
                return "第四章 梦境探索";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 5)
        {
            if (part == 0)
                return "第五章 初临尖塔";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 6)
        {
            if (part == 0)
                return "第六章 书塔修炼";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 7)
        {
            if (part == 0)
                return "第七章 德洛瑞斯的归来";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 8)
        {
            if (part == 0)
                return "第八章 大闹金库";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 9)
        {
            if (part == 0)
                return "第九章 缄口地窖";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        else
            return null;
    }

    /// <summary>
    /// 开场介绍
    /// </summary>
    private string StartText()
    {
        return File.ReadAllText("Assets/StreamingAssets/女巫学徒/0.txt");
    }
    /// <summary>
    /// 第一章第一幕剧情
    /// </summary>
    private string First_1_Start()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/女巫学徒/1_1_1.txt");
        FindLeadingChara();
        string result = a[0] + leadingChara.wordName + a[1];

        if (leadingChara.gender == GenderEnum.girl)
            result += "她";
        else if (leadingChara.gender == GenderEnum.boy)
            result += "他";
        else
            result += "它";

        result += a[2];
        return result;
    }

    /// <summary>
    /// 第一章第一幕战场
    /// </summary>
    private string First_1_Fight()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/女巫学徒/1_1_2.txt");
        AbstractVerbs[] verbs=fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = a[0];
        foreach(AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText())) 
                continue;
            else
            result+=verb.UseText();
        }
        result += a[1];
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText ="";
        return result;
    }
    /// <summary>
    /// 第一章第一幕结束
    /// </summary>
    private string First_1_End()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/女巫学徒/1_1_3.txt");
        FindLeadingChara();
        string result = a[0] + leadingChara.wordName + a[1];

        if (leadingChara.gender == GenderEnum.girl)
            result += "噗嗤一笑";
        else
            result += "哈哈大笑";
        result += a[2];
        result += leadingChara.wordName;
        result += a[3];
        result += leadingChara.wordName;
        result += a[4];
        return result;
    }

    /// <summary>
    /// 第一章第二幕剧情
    /// </summary>
    private string First_2_Start()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/女巫学徒/1_2_1.txt");
        FindLeadingChara();
        //二号友方
        AbstractCharacter[] z = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara = null;
        if (a.Length >= 2) secondChara = z[1];

        if (secondChara == null) leadingChara = secondChara;//防止空
        string result = "同" +secondChara.wordName + a[0]+leadingChara.wordName+a[1]+leadingChara.name+a[2];
        if (secondChara.trait.traitName != "敏感")
        {
            result += "放肆";
            result += a[3];
            result+=secondChara.wordName;
            result += "厉声呵斥";
            result += a[4];
        }
        else
        {
            result += "可笑";
            result += a[3];
            result+=secondChara.wordName;
            result += "小声嘲讽";
            result += a[4];
        }

        return result;
    }
    /// <summary>
    /// 第一章第二幕战场
    /// </summary>
    private string First_2_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result="";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// 第一章第二幕结束
    /// </summary>
    private string First_2_End()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/女巫学徒/1_2_3.txt");
        FindLeadingChara();
        string result = a[0]+"\n"+a[1]+leadingChara.wordName+a[2]+leadingChara.wordName+a[3]+leadingChara.wordName+a[4];;
        return result;
    }

    /// <summary>
    /// 第二章第一幕开场
    /// </summary>
    private string Second_1_Start()
    {
        FindLeadingChara();
        //二号友方
        AbstractCharacter[] a = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara=null;
        if(a.Length >=2)secondChara = a[1];

        string result;
        if (secondChara == null || secondChara == leadingChara) //防止空
            result = leadingChara.wordName;
        else
            result = leadingChara.wordName + "和" + secondChara.wordName;

        result += "站在大书柜面前一一扫视。“《地狱辞典》…的脾气是不是有点太火爆了，还是先不打扰它了吧。”《地狱辞典》斜躺在书柜一角里，时不时传出呼噜声。\n";
        result += "“嗯…《所罗门的老钥匙》里好像也是记录着关于地狱方面的知识，我可不想去召唤那些滑头的火焰妖精出来，千万别把林子给点燃了。”" + leadingChara.wordName + "看着这本颇为厚重的书，每次扫灰到它这里都最麻烦了。\n";
        result += "“解梦…茶占卜…魔药…这些书也没法拿来教训他们”林黛玉快速扫视着，然后视线落在了一本有金线装帧的精美书籍。“平时德洛瑞丝喜欢用《密特拉魔典》里的杂技法术来戏耍村民，我还蛮喜欢那些花哨好看的小焰火的，要是能学一学就好了。”" + leadingChara.wordName + "抽出了《密特拉魔典》，放了四个点燃的蜡烛在周围，轻抚书脊，并将手掌平放在书本封面，这是唤醒魔法书的简易仪式。\n";
        result += "“是" + leadingChara.wordName + "啊，怎么今天是你唤醒的我？”密特拉看起来有些不满。“瑞丝呢，叫她过来。”“尊敬的密特拉，我请求你的原谅”" + leadingChara.name + "谦卑的说“神秘的德洛瑞丝留言说去山谷里处理水晶蘑菇了，还不知道什么时候才能归来。”\n";
        result += "“奥，那倒是估计得忙一阵了”密特拉说道“那" + leadingChara.wordName + " ，你找我做什么呢？偷偷打开魔典可是要被严惩的哦～”密特拉坏笑得看着名字。\n";
        result += "“尊敬的密特拉，莱宁城银行的人要来找我们的麻烦，竟敢扬言要推平咱们的房子呢”" + leadingChara.wordName + "气愤地说道。\n";
        result += "“害，那破银行还没倒闭啊”密特拉听完十分扫兴“你可别告诉我，是要学我的魔法来对付他们。”密特拉哈哈大笑起来。" + leadingChara.wordName + "皱了皱眉头，因被看扁了而稍有些生气。“尊敬的密特拉，那请问除了你那些玄奥的魔法以外，能否告诉我一些简单的魔法呢？”\n";
        result += "“嗯…你还别说”密特拉来回扭了扭，从书本中飞出来了一张小纸条“可恶的德洛瑞丝竟然把一个颇为碍眼的简单法术当做书签夹在了我这里，我怎么弄都弄不出来，卡得我痒死了！”\n";

        result += "#";

        result += "“要不是你把我从那拥挤的书架里弄出来，我还得难受好久”密特拉看起来生气极了“这个垃圾小杂技应该够你用了，赶紧拿的离我越远越好！”" + leadingChara.wordName + "从地上捡起那张小纸片，是德洛瑞丝从笔记本里撕下来的一个不用的小法术，稍作研修就可以学会。“感谢尊敬的密特拉”林黛玉对其施礼，并准备伸手去拿它“那我将您放回书架？”\n";
        result += "“不不不不，怎么可能！”密特拉急躁地说道“你带着我出去玩一玩，我现在不要回去”“啊，密特拉果然好麻烦啊…”" + leadingChara.wordName + "苦恼地想着“那就先放着它吧，我先练一下这个小法术，这还是我第一个学会的咒语呢！”想到这里" + leadingChara.wordName + "又开始激动起来。\n";
        result += "“阿侬依～阿鼻西～诺依塔撒！”" + leadingChara.wordName + "拿着纸片读着，并且试着将魔法的力量引导出身体“诺伊塔拉～西碧拉～西碧撒！”说完从手中就蹦出了三个小火球，互相旋绕着。林黛玉将手一挥，火球便飞出将一大堆药罐打碎了。\n";
        result += "“哟～你学习魔法多久啦，看起来天赋还不错啊”密特拉对林黛玉产生起了兴趣“说不定你努力几十年，也能学会一两个我的魔法呢～”\n";
        result += "“快找点东西炸一下，就那群老鼠吧”密特拉开心的说“把它们炸碎！”\n";

        return result;
    }

    /// <summary>
    /// 第二章第一幕战场
    /// </summary>
    private string Second_1_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// 第二章第一幕结局
    /// </summary>
    private string Second_1_End()
    {
        string result = "杂技火球在接触到老鼠的一瞬间，那只肥老鼠就因身体内部的油脂被快速气化膨胀，从而变得肿胀，最终因皮囊被撕扯到了极限而炸裂开来。\n";
        result += "“不错不错，这些老鼠死有余辜”密特拉兴奋地说“带上我出去走走吧，去森林里面。”\n";

        return result;
    }

    /// <summary>
    /// 第二章第二幕开始
    /// </summary>
    private string Second_2_Start()
    {
        string result = "“嗯…居然还有些老鼠也溜进来了吗？”密特拉发现几个银行职员鬼鬼祟祟地溜进了小屋里“"+leadingChara.wordName+ "，就拿刚才的法术击败他们！”“可恶，被发现了…”银行职员正准备伸手去拿水晶球“"+leadingChara.wordName+ "，那就别怪我们动手了！”银行职员们围了上来。\n";
        return result;
    }
    /// <summary>
    /// 第二章第二幕战场
    /// </summary>
    private string Second_2_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// 第二章第二幕结局
    /// </summary>
    private string Second_2_End()
    {
        
        string result="";

        return result;
    }
    /// <summary>
    /// 第二章第三幕开始
    /// </summary>
    private string Second_3_Start()
    {
        return "“可恶，都给我上！”更多的银行打手们正在往里涌“不还钱还不准我们来拿东西吗！”";
    }

    /// <summary>
    /// 第二章第三幕战场
    /// </summary>
    private string Second_3_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }
    /// <summary>
    /// 第二章第三幕结局
    /// </summary>
    private string Second_3_End()
    {
        string result = "一群西装革履的银行职员们挤破头从小屋往外钻“快跑，这小学徒也会魔法！”\n";
        result += "“哈哈哈哈，这些银行的就只有这副德行吗？”密特拉发出了尖锐的笑“林黛玉，追上去把他们干掉吧。”“这……这就没必要了吧。”林黛玉苦笑着：“没必要把事情闹大吧，教训一下他们就好了吧。”“那就再追上去给他们屁股上再来一下，快！”密特拉坏笑着：“你要是能让那些假正经的捂着屁股跑，我就指点你两招。”“真的嘛，好耶。”林黛玉听到了两眼放光，便抱着《密特拉魔典》冲出了小屋“别跑！”\n";

        return result;
    }
    /// <summary>
    /// 第三章第一幕剧情
    /// </summary>
    private string Third_1_Start()
    {
        FindLeadingChara();
        //二号友方
        AbstractCharacter[] a = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara = null;
        if (a.Length >= 2) secondChara = a[1];

        string result = "在苍绿的林间小道里，"+leadingChara.wordName+ "追着用杂技火球砸向狼狈逃走的银行职员们。“哈哈哈，你看到前面那个家伙了吗，他的屁股都着火了！”密特拉开心地说：“喂喂喂，你的屁股都烧焦啦！””那里那里，那个家伙，给他一下！”"+leadingChara.wordName+ "便瞄准了一个格外滑稽姿势的银行职员扔去了一个小焰火球，结果打歪了，把他抹的油光的头发炸成了蘑菇状。“尊敬的密特拉，您满意了吗？”林黛玉问道：“我可不想用这火焰魔法把林子点着啦。”“好啦好啦，行吧。让他们逃吧，我们回去。”密特拉突然脸色凝重：“是谁？”";
        if (secondChara != null && secondChara == leadingChara)
            result += leadingChara.wordName + "和" + secondChara.wordName;
        else
            result+=leadingChara.wordName;

        result += "一脸疑惑地看向四周：“这附近还有藏着的家伙吗？”“学习禁忌之术的家伙，速速给我现身！”密特拉魔典突然从" + leadingChara.wordName + "手中飞起汇聚着力量，书页快速地翻动着，浑身冒着淡淡紫蓝色的光芒。强大的气流将林间厚厚的落叶扫开，纷纷碰撞到树干树枝，发出细密的簌簌声。但强劲的气流在靠近一棵粗壮的树不远处便似乎消失停止了“哼……嗯”从这棵粗壮的树干后走出了一个体态畸形，浑身缠满绷带的怪人，浑身散发着诡谲的气息。“肮脏的沉默者，看来从地窖里爬出了一只格外丑陋肮脏的绷带耗子。”密特拉表情严肃地看向它。“沉默者？”"
            + leadingChara.wordName + "虽然不解，也察觉到了对方绝非善类：“看来今天找我们麻烦的不止银行。”\n"
            + leadingChara.wordName + "说完便试图用全力释放出一颗火球，但发现自己的体内虽翻涌着力量，却一逸出指尖便化为乌有。”"
            + leadingChara.wordName + "，抱着我快跑。”密特拉严肃地说：“现在的你无法打败它，若非足够强大的魔法在它面前是不会奏效的。”“好。”林黛玉抱紧了《密特拉魔典》准备转身跑回小屋，却突然发现它已经到了回到小屋的路上“哼哼？”沉默者低声发出了嘲讽的声音。“看来我们没有退路了，只能和他拼了！”";
        if (secondChara != null && secondChara == leadingChara)
            result += leadingChara.wordName + "和" + secondChara.wordName;
        else
            result += leadingChara.wordName;

        result += "招架好准备迎敌。“" + leadingChara.wordName + "，要小心，我没法给你提供帮助。”密特拉说道。\n";

        return result;
    }
    /// <summary>
    /// 第三章第一幕战场
    /// </summary>
    private string Third_1_Fight()
    {
            AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
            string result = "";
            foreach (AbstractVerbs verb in verbs)
            {
                if (string.IsNullOrEmpty(verb.UseText()))
                    continue;
                else
                    result += verb.UseText();
            }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// 第三章第一幕结局
    /// </summary>
    private string Third_1_End()
    {
        FindLeadingChara();
        //二号友方
        AbstractCharacter secondChara = fatherObject.transform.Find("SelfCharacter").GetComponentInChildren<AbstractCharacter>();
        if (secondChara == null) secondChara = leadingChara;
        string result = "看着散落在地上的绷带，支离破碎的沉默者，" + leadingChara.wordName + "疲惫地倚靠着一棵树，看着破碎在空中的绷带：“它……它死了吗？”"
            + secondChara.wordName + "说：“我觉得恐怕不会这么简单，我们趁现在快走。”“好。”"
            + leadingChara.wordName + "抱起《密特拉魔典》朝着女巫小屋走。在林黛玉的背后，无声地，地上的绷带慢慢又飘起，慢慢形成了一个离散的人形，并且悄无声息地积蓄着力量。林黛玉还未走远，感受到风正徐徐向后刮，落叶碎枝也都躁动不安。“看来它还没死绝。”密特拉说道：“快走，它的力量很强大！”林黛玉回头，看到沉默者支离破碎的身躯正聚集着能量，周围的空气似乎要将他们拽回去，于是决定开始全力逃命。突然一阵安静，周围不再躁动，几片破碎的绷带如轻舞的蝴蝶般，缓缓从背后进入正全力逃跑的"
            + leadingChara.wordName + "的视线。" + leadingChara.wordName + "知道沉默者来了，跑的更快了，但绷带轻飘飘的却无法甩开，反而是越来越多地从背后追上。越来越多，越来越多的绷带，已经有不少已经超越了他们。突然一下，如同一只无形的大手攥住拳头，绷带们死死地挤压住林黛玉的身体，将她攥得无法动弹，而"
            + leadingChara.wordName + "由于突然的力量重重地飞向空中，却没有落下。“被抓住了！”密特拉急忙地说道。而"
            +leadingChara.wordName + "虽然想回答却只能感觉自己肺里的空气疯狂的逸出，难以形成一句完整的话。随着力量越来越强，"
            + leadingChara.wordName + "感觉到自己的肋骨快要断裂，身上的关节嘎吱作响，快要被这股巨大的压力压扁了。林黛玉的视野慢慢昏沉黑暗，并最后失去了意识。\n";

        return result;
    }
}
