using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DebugUi : MonoBehaviour
{
    Text text1;
    Text text2;
    public GameObject FangKeObj;
    public GameObject weijiObj;
    public GameObject button;
    private void Awake()
    {
       
        Time.timeScale = 0;

        text1 = transform.Find("Time1").GetComponent<Text>();
        text2 = transform.Find("Time2").GetComponent<Text>();
        text1.text = GameMgr.instance.time1.ToString();
        text2.text = GameMgr.instance.time2.ToString();
    }

    #region 外部点击事件
    public void Exit()
    {
        
        Time.timeScale = GameMgr.instance.timeSpeed;
        Destroy(this.gameObject);
       
    }

    public void AddTouzi()
    {
        GameMgr.instance.AddDice(100);
    }
    public void AddSentence1()
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
        GameMgr.instance.draftUi.AddContent("冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                  "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                  "衣衫破损的人惊惶四顾、人人自疑，" +
              "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
              "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
              "这该死的一无所有的时代！冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                  "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                  "衣衫破损的人惊惶四顾、人人自疑，" +
              "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
              "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
              "这该死的一无所有的时代冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                  "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                  "衣衫破损的人惊惶四顾、人人自疑，" +
              "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
              "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
              "这该死的一无所有的时代");
        GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
         "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
         "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
         "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
         "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
         "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
         "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
         "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
         "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");
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
    }

    public void AddSentence2()
    {
        GameMgr.instance.draftUi.AddContent("嗨你好！！第一行");
        GameMgr.instance.draftUi.AddContent("第二行行行行行");
       
       
    }
    public void EndGame()
    {
       
        CharacterManager.instance.EndGame();
        Exit();
    }

    bool hasShowCharaButton = false;

    public void ShowOrHideCharaButton(GameObject _show)
    {
        if (!hasShowCharaButton)
        {
            if (_show.name == "chara")
            {
                var _charas = GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().charaPrefabs;
                for (int i = 0; i < _charas.Length;i++)
                {
                    var _b=Instantiate(button, _show.transform.GetChild(0));
                    _b.transform.localScale = Vector3.one;
                    _b.GetComponentInChildren<Text>().text = _charas[i].name;
                    var _bET= _b.AddComponent<EventTrigger>();
                    AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { AddCharacter(_bET.transform); });

                    print("0++" + i);
                    hasShowCharaButton = true;
                }
            }
            if (_show.name == "monster")
            {
                var _charas = GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().monsterPrefabs;
                for (int i = 0; i < _charas.Length; i++)
                {
                    var _b = Instantiate(button, _show.transform.GetChild(0));
                    _b.transform.localScale = Vector3.one;
                    _b.GetComponentInChildren<Text>().text = _charas[i].name;
                    var _bET = _b.AddComponent<EventTrigger>();
                    AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { AddMonster(_bET.transform); });
                   
                    hasShowCharaButton = true;
                }
            }
        }
        else
        {
            for (int _t = _show.transform.GetChild(0).childCount -1; _t >=0; _t--)
            {
                Destroy(_show.transform.GetChild(0).GetChild(_t).gameObject);
            }
            hasShowCharaButton = false;
        }
    }
    public void DeleteCharaButton(GameObject _show)
    {
        var _charas = CharacterManager.instance.charas;
        for (int i = 0; i < _charas.Length; i++)
        {
            var _b = Instantiate(button, _show.transform.GetChild(0));
            _b.transform.localScale = Vector3.one;
            _b.GetComponentInChildren<Text>().text = _charas[i].name;
            var _bET = _b.AddComponent<EventTrigger>();
            AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { DeleteCharacter(_bET.transform); });
            hasShowCharaButton = true;


        }

    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="eventTrigger">需要添加事件的EventTrigger</param>
    /// <param name="eventTriggerType">事件类型</param>
    /// <param name="callback">回调函数</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
       
    }



    #endregion
    public void AddCharacter(Transform _chara)
    {

        //创建固定危机事件
        GameMgr.instance.gameProcess.CreateFangke(false, _chara.GetSiblingIndex() + 1);
        //PoolMgr.GetInstance().GetObj(FangKeObj, (a) =>
        //{

        //    a.transform.SetParent(GameMgr.instance.characterCanvas.transform);
        //    a.transform.localPosition = Vector3.zero;

        //    print("debugUI" +( _chara.GetSiblingIndex() + 1).ToString());
        //    a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, _chara.GetSiblingIndex() + 1);
         
        //});
        Exit();
    }

    public void ClickXiWang_A()
    {
        GameMgr.instance.gameProcess.CreateXiWang(false);
        Exit();
    }
    public void ClickXiWang_B()
    {
        GameMgr.instance.gameProcess.CreateXiWang(true);
        Exit();
    }
    public void ClickSetting_A()
    {

        GameMgr.instance.gameProcess.CreateSetting(true);
        Exit();
    }
    public void ClickSetting_B()
    {
        GameMgr.instance.gameProcess.CreateSetting(false);
        Exit();
    }
    public void ClickYiwai_A()
    {

        GameMgr.instance.gameProcess.CreateYiwai(true);
        Exit();
    }
    public void ClickJiaoyi_A()
    {

        GameMgr.instance.gameProcess.CreateJiaoYi(true);
        Exit();
    }
    public void ClickChangjing_A()
    {

        GameMgr.instance.gameProcess.CreateChangJing(true);
        Exit();
    }
    public void AddMonster(Transform _chara)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(weijiObj, (a) =>
        {

            a.transform.SetParent(GameMgr.instance.characterCanvas.transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.WeiJi, false, _chara.GetSiblingIndex()) ;
            
        });
        Exit();
    }

    public void DeleteCharacter(Transform _s)
    {
        var _c = CharacterManager.instance.charas[_s.GetSiblingIndex()];
        _c.Camp = CampEnum.left;
        CharacterManager.instance.RefreshStanger();
        Destroy(_c.gameObject);
        Exit();
    }

    public void ChangeSpeed(float _speed)
    {
        GameMgr.instance.timeSpeed = _speed;
    }
   
}
