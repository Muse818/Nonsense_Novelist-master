using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SeeWordDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOpen = false;
    private string adr_detail= "UI/WordInformation";
    private GameObject go;
    private Vector3 detailPos = Vector3.zero;
    private Vector3 detailScale = Vector3.one;
    private WordTable.Data mCardData = null;

    [HideInInspector] public string resTitleBg = "WordImage/wordTitle/";


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ShootCombat"|| SceneManager.GetActiveScene().name == "NewGame4")
        {
            detailPos = Vector3.zero-new Vector3(200,0,0);
            detailScale = Vector3.one*1.1f;
        }
        if (SceneManager.GetActiveScene().name == "BookDesk3")
        {
            detailPos = Vector3.zero ;
            detailScale = Vector3.one*1.3f;
        }
        if (SceneManager.GetActiveScene().name == "CombatTest")
        {
            detailPos = Vector3.zero;
            detailScale = Vector3.one * 0.7f;
        }
    }

    public void Setup(WordTable.Data data)
    {
        mCardData = data;
    }

    /// <summary>
    /// 012动名形
    /// </summary>
    /// <param name="word"></param>
    /// <param name=""></param>

    public void SetPic(AbstractWord0 word)
    {
        string resTBName = "";
        int type = 0;
        if (AllSkills.list_noun.Contains(word.GetType())) type = 1;
        else if (AllSkills.list_adj.Contains(word.GetType())) type = 2;
        else if (AllSkills.list_verb.Contains(word.GetType())) type = 0;
        else { print("在任何词语集合里都找不到" + word.wordName); return; }

        if (type == 0)//verb
        {
            //AbstractWord0 word;
            //if (!this.TryGetComponent<AbstractWord0>(out word)) return;
            resTBName = resTitleBg + (((AbstractVerbs)word).rarity).ToString() + "_" + (((AbstractVerbs)word).skillID % 10).ToString();
            
        }
        else if (type == 1)//noun
        {
            //AbstractWord0 word;
            //if (!this.TryGetComponent<AbstractWord0>(out word)) return;
            resTBName = resTitleBg + (((AbstractItems)word).rarity).ToString() + "_" + (((AbstractItems)word).itemID % 10).ToString();
           
        }
        else if(type==2)//adj
        {
            //AbstractWord0 word;
            //if (!this.TryGetComponent<AbstractWord0>(out word)) return;
            resTBName = resTitleBg + (((AbstractAdjectives)word).rarity).ToString() + "_" + (((AbstractAdjectives)word).adjID % 10).ToString();
            
        }
  
       
        if (this.TryGetComponent<Image>(out var _i))
        {
            var tepTBSprite = Resources.Load<Sprite>(resTBName);
            if (tepTBSprite == null)
            {
                _i.sprite = Resources.Load<Sprite>("WordImage/wordTitle/1_1");
            }
            else
            {
                _i.sprite = tepTBSprite;
            }
        }
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
        PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
        {
            go = obj;

            var worInfoComp = obj.GetComponentInChildren<WordInformation>();
            worInfoComp.SetIsDetail(false);
            worInfoComp.ChangeInformation(mCardData);

            obj.transform.parent = this.transform;
            obj.transform.localPosition = new Vector3(0, 0, 3);
            obj.transform.localScale = detailScale;
            obj.transform.GetChild(0).localPosition = detailPos;
            obj.GetComponent<Canvas>().overrideSorting = true;
        });

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;
        PoolMgr.GetInstance().PushObj(adr_detail, go);
    }
    public void ClickClose()
    {
        isOpen = false;
        PoolMgr.GetInstance().PushObj(adr_detail, go);
    }

    
}
