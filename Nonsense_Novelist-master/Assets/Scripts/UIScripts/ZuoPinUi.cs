using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

using System.IO;
public class ZuoPinUi : MonoBehaviour
{
    [Header("（手动设置）")]
    public GameObject bookIconPrefabs;
    public Transform bookIconParent;
    public GameObject bookNew;
    public Transform bookChosen;
    private TextMeshProUGUI bookChosenText;
    public TextMeshProUGUI tipText;
    public ReadZuoPin readBook;
    private string poolNameBook;
    private Vector3 newICONoffset = new Vector3(65,-59,0);
    private void Awake()
    {
        poolNameBook = bookIconPrefabs.name;
        bookChosenText = bookChosen.GetComponentInChildren<TextMeshProUGUI>();
        bookChosen.localScale = Vector3.zero;
        tipText.gameObject.SetActive(false);
        readBook.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        bookChosen.localScale = Vector3.zero;
        tipText.gameObject.SetActive(false);
        readBook.gameObject.SetActive(false);
        InitBookIcon();
    }
    public void InitBookIcon()
    {
        int _numberCount = 0;
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets");
        var _files = di.GetFiles("*");
     
        if (_files.Length == 0)
        {
            tipText.gameObject.SetActive(true);
            tipText.text = "作品库现在空空如也";
            return;
        }

        for (int _i = 0; _i < _files.Length; _i++)
        {

            if (_files[_i].Name.EndsWith(".meta"))
            {
                continue;
            }
            if (_numberCount >= 10) continue;

            var _temp = RecordMgr.instance.LoadByJson(_files[_i].FullName);
            if (bookIconPrefabs == null) print("bookIconPrefabs==null");
            if (PoolMgr.GetInstance() == null) print("PoolMgr==null");
            PoolMgr.GetInstance().GetObj(bookIconPrefabs, (obj) =>
            {
                obj.transform.SetParent(bookIconParent, false);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                obj.GetComponentInChildren<TextMeshProUGUI>().text = _temp.title;
                obj.name = _files[_i].FullName;
                if (!_temp.hasRead)//第一本
                {
                    PoolMgr.GetInstance().GetObj(bookNew, (_new) =>
                    {
                        _new.transform.parent = obj.transform;
                        _new.name = "new";
                        _new.transform.localPosition = Vector3.zero + newICONoffset;
                        _new.transform.localScale = Vector3.one * 0.3f;
                    });
                }


                var et = obj.GetComponent<EventTrigger>();
                AddPointerEvent(et, EventTriggerType.PointerClick, (_obj) => { ClickBookIcon(obj); });
            });

            _numberCount++;
          
        }
        #region 废弃
        //if (RecordMgr.instance.recordList2.Count == 0)
        //{
        //    tipText.gameObject.SetActive(true);
        //    tipText.text = "作品库现在空空如也";
        //    return;
        //}
        //tipText.gameObject.SetActive(false);
        //int i = RecordMgr.instance.recordList2.Count - 1;
        //int j = 0;
        ////读取最新的10个存档，生成icon和名称
        ////第一个生成的一定是最新的
        //for (; (i >= 0) && (j < 10); i--, j++)
        //{
        //    var _temp = RecordMgr.instance.recordList2[i];
        //    PoolMgr.GetInstance().GetObj(bookIconPrefabs, (obj) =>
        //     {
        //         obj.transform.parent = bookIconParent;
        //         obj.transform.localPosition = Vector3.zero;
        //         obj.transform.localScale = Vector3.one;

        //         obj.GetComponentInChildren<TextMeshProUGUI>().text = RecordMgr.instance.recordList2[i].title;
        //         obj.name = i.ToString() ;
        //         if (!_temp.hasRead)//第一本
        //         {
        //             PoolMgr.GetInstance().GetObj(bookNew, (_new) =>
        //             {
        //                 _new.transform.parent = obj.transform;
        //                 _new.name = "new";
        //                 _new.transform.localPosition = Vector3.zero+ newICONoffset;
        //                 _new.transform.localScale = Vector3.one*0.3f;
        //             });
        //         }


        //         var et = obj.GetComponent<EventTrigger>();
        //         AddPointerEvent(et, EventTriggerType.PointerClick, (_obj) => { ClickBookIcon(obj); });
        //     });


        //}
        #endregion
    }


    public void ClickBookIcon(GameObject obj)
    {

      
        //删除【新】标签
        if (obj.transform.Find("new")!=null)
        {
            Destroy(obj.transform.Find("new").gameObject);
        }
        //如果已选中，取消选中
        if (obj.GetComponentInChildren<Button>()!=null)
        {
          
            bookChosen.parent=this.transform;
            bookChosen.localScale = Vector3.zero;
            return;
        }
        //将选中表现换为当前
        string _i = obj.name;
        if (RecordMgr.instance.LoadByJson(_i).hasRead == false)
        {
            RecordMgr.instance.ChangeReadJson(_i);
   
        }
           


        if (bookChosen == null) return;

        var _s= obj.GetComponentInChildren<TextMeshProUGUI>().text;
        bookChosen.parent = obj.transform;
        bookChosen.localScale = Vector3.one;
        bookChosen.localPosition = Vector3.zero;
        bookChosenText.text = _s;


    }

    public void ExitUI()
    {
        for (int x = bookIconParent.childCount - 1; x >= 0; x--)
        {
            if (bookIconParent.GetChild(x).transform.Find("new") != null)
            {
                Destroy(bookIconParent.GetChild(x).transform.Find("new").gameObject);
            }
            if (bookIconParent.GetChild(x).transform.Find("chosen") != null)
            {
                bookChosen.parent = this.transform;
                bookChosen.localScale = Vector3.zero;
            }
            //移除所有监听后放回池子
            var _t = bookIconParent.GetChild(x).GetComponent<EventTrigger>();
            _t.triggers.RemoveRange(0, _t.triggers.Count);
            bookIconParent.GetChild(x).gameObject.name = poolNameBook;
            PoolMgr.GetInstance().PushObj(poolNameBook,bookIconParent.GetChild(x).gameObject);
        }
        StudyMouseOn.hasOpenUI = false;
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ExitUI();
    }

    public void CheckMasterPiece(Transform _this)
    {
        readBook.gameObject.SetActive(true);
        
        var _p = _this.parent;
     
        readBook.SetContent(RecordMgr.instance.LoadByJson(_p.gameObject.name));
        readBook.openDraft() ;
    }

    public void RemoveMasterPiece(Transform _this)
    {    
        var _p=_this.parent;
        bookChosen.parent = this.transform;
        bookChosen.localScale = Vector3.zero;
        string  _i = _p.gameObject.name;

        File.Delete(_i);

        for (int x = bookIconParent.childCount - 1; x >= 0; x--)
        {
            if (bookIconParent.GetChild(x).transform.Find("new") != null)
            {
                Destroy(bookIconParent.GetChild(x).transform.Find("new").gameObject);
            }
            if (bookIconParent.GetChild(x).transform.Find("chosen") != null)
            {
                bookChosen.parent = this.transform;
                bookChosen.localScale = Vector3.zero;
                return;
            }
            //移除所有监听后放回池子
            var _t = bookIconParent.GetChild(x).GetComponent<EventTrigger>();
            _t.triggers.RemoveRange(0, _t.triggers.Count);
            bookIconParent.GetChild(x).gameObject.name = poolNameBook;
            PoolMgr.GetInstance().PushObj(poolNameBook, bookIconParent.GetChild(x).gameObject);
        }
   
        
        InitBookIcon();
        return;
    }
    public void ExitReadPanal()
    {
        readBook.closeDraft();
        readBook.gameObject.SetActive(false);
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
        Debug.Log("AddPointerEnterEvent");
    }

    
}
