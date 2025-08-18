using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class EventCg : MonoBehaviour
{
    bool beforeCGPlay = false;
    Animator anim;
    TextMeshProUGUI text;
    GameObject skipButton;
    private PlayableDirector director = null;
    public List<PlayableAsset> cgs;
    void Awake()
    {
        skipButton = this.transform.Find("Skip").gameObject;
        skipButton.SetActive(false);
           anim = this.GetComponent<Animator>();
        text = this.GetComponentInChildren<TextMeshProUGUI>();

        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            director = gameObject.AddComponent<PlayableDirector>();
        }
        director.Stop();
        director.playableAsset = null;

        this.gameObject.SetActive(false);
    }



    public void PlayEventCG(string playName)
    {
        skipButton.SetActive(true);
        //播放的时候 暂停游戏
        beforeCGPlay = CharacterManager.instance.pause;
        CharacterManager.instance.pause = true;
        GameMgr.instance.HideGameUI();
        Ele_Kc(playName);
        //anim.Play(playName);
        //text.text = textContent;
        int x = -1;
        for (int i = 0; i < cgs.Count; i++)
        {
            if (cgs[i].name == playName)
            {
                x = i;
                break;
            }

        }
        if (x == -1)
        {
            print("有错");
            return;
        }

        this.gameObject.SetActive(true);
        director.playableAsset = cgs[x];
        director.Play();
    }


    /// <summary>
    /// 和外部按钮绑定，跳过当前的动画
    /// </summary>
    public void Skip()
    {
        PlayEventCGEnd();
    }


    public void HideGameUI()
    {
        GameMgr.instance.HideGameUI();
    }
    #region CG结束

    public void PlayEventCGEnd()
    {
        print("PlayEventCGEnd");
        CharacterManager.instance.pause = beforeCGPlay;
        director.Stop();
        director.playableAsset = null; 
        this.gameObject.SetActive(false);
        skipButton.SetActive(false);
        GameMgr.instance.ShowGameUI();
  
    }
    public void GetChara_JC1()
    {
        print("GetChara_JC1");
        var _jc1=this.transform.Find("剧场1");
        var _cs = CharacterManager.instance.charas;
        if (_cs[0] != null)
        {
           
            _jc1.Find("a1").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[0].wordName);
            _jc1.Find("a1").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            
            _jc1.Find("a1").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[1] != null)
        {
            _jc1.Find("a2").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[1].wordName);
            _jc1.Find("a2").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("a2").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[2] != null)
        {
            _jc1.Find("b1").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[2].wordName);
            _jc1.Find("b1").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("b1").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[3] != null)
        {
            _jc1.Find("b2").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[3].wordName);
            _jc1.Find("b2").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("b2").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }
    }
    #endregion


    #region 文章
    public void Ele_Kc(string playName)
    {
        switch (playName)
        {
            case "ElecSheep_start1":
                {
                    GameMgr.instance.draftUi.AddContent("冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                        "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                        "衣衫破损的人惊惶四顾、人人自疑，" +
                    "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
                    "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
                    "这该死的一无所有的时代！");
                    GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
                     "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
                     "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
                     "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
                     "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
                     "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
                     "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
                     "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
                     "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");
                    foreach (var _chara in CharacterManager.instance.charas)
                    {
                        GameMgr.instance.draftUi.AddContent(_chara.ShowText(_chara));
                    }
                }
                break;

            case "":
                {
                    GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
                        "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
                        "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
                        "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
                        "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
                        "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
                        "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
                        "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
                        "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");
                }
                break;
        }
      
    }




    
    #endregion
}
