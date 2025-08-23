using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WordInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("【手动】对应的碰撞类型")] public TextMeshProUGUI textCollider;

    [Header("【手动】词条信息对应的组件")] [Tooltip("卡牌底图")]
    public Image wordkindBg;

    [Tooltip("卡牌种类文字")] public Text wordkindText;
    [HideInInspector] public string textVerb = "动";
    [HideInInspector] public string textNoun = "名";
    [HideInInspector] public string textAdj = "形";

    [Tooltip("显示CD的文字")] public Text needCD;
    [Tooltip("词条名称")] public Text title;
    public Image titleBg;
    [Tooltip("词条文字")] public TextMeshProUGUI description;
    [Tooltip("是needCD的父物体")] public Image energy;
    [Tooltip("词条图像")] public Image wordImage;

    /// <summary>手动：词条图像读取路径前缀（后加id）</summary>
    [HideInInspector] public string resAdrNoun = "WordImage/Noun/";

    [HideInInspector] public string resAdrAdj = "WordImage/Adj/";
    [HideInInspector] public string resAdrVerb = "WordImage/Verb/";
    [HideInInspector] public Sprite tepSprite;
    [HideInInspector] public string resName;
    [HideInInspector] public string resTitleBg = "WordImage/wordTitle/";
    [HideInInspector] public Sprite tepTBSprite;
    private string resTBName;

    [Header("默认的词语图像")] public Sprite defaultWordImage;
    public Sprite defaultWordTitleImage;

    [Header("机制解释面板")] public GameObject detailInfoPrefab;
    public Transform detailParent;
    private WordTable.Data nowWord;
    private CardSO mCardAsset;
    private bool isDetail = false;

    private bool isCombatScene = false;
    private AbstractWord0 abs;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ShootCombat")
            isCombatScene = true;
        titleBg = title.transform.parent.GetComponent<Image>();

    }

    public void ChangeInformation(AbstractWord0 word)
    {
        if (titleBg == null)
            titleBg = title.transform.parent.GetComponent<Image>();

        switch (word.wordKind)
        {
            case WordKindEnum.adj:
            {

                wordkindText.text = textAdj;
                resName = resAdrAdj + "adj_" + ((AbstractAdjectives)word).adjID;
                tepSprite = Resources.Load<Sprite>(resName);

                resTBName = resTitleBg + (((AbstractAdjectives)word).rarity).ToString() + "_" +
                            (((AbstractAdjectives)word).adjID % 10).ToString();
                tepTBSprite = Resources.Load<Sprite>(resTBName);

                if (tepSprite == null)
                {
                    wordImage.sprite = defaultWordImage;
                }
                else
                    wordImage.sprite = tepSprite;

                if (tepTBSprite == null)
                    titleBg.sprite = defaultWordTitleImage;
                else
                    titleBg.sprite = tepTBSprite;
            }
                break;
            case WordKindEnum.noun:
            {

                wordkindText.text = textNoun;

                resName = resAdrNoun + "n_" + ((AbstractItems)word).itemID;
                tepSprite = Resources.Load<Sprite>(resName);

                resTBName = resTitleBg + (((AbstractItems)word).rarity).ToString() + "_" +
                            (((AbstractItems)word).itemID % 10).ToString();
                tepTBSprite = Resources.Load<Sprite>(resTBName);

                if (tepSprite == null)
                    wordImage.sprite = defaultWordImage;
                else
                    wordImage.sprite = tepSprite;


                if (tepTBSprite == null)
                    titleBg.sprite = defaultWordTitleImage;
                else
                    titleBg.sprite = tepTBSprite;
            }
                break;
            case WordKindEnum.verb:
            {

                wordkindText.text = textVerb;

                resName = resAdrVerb + "v_" + ((AbstractVerbs)word).skillID;
                tepSprite = Resources.Load<Sprite>(resName);

                resTBName = resTitleBg + (((AbstractVerbs)word).rarity).ToString() + "_" +
                            (((AbstractVerbs)word).skillID % 10).ToString();

                tepTBSprite = Resources.Load<Sprite>(resTBName);

                if (tepSprite == null)
                    wordImage.sprite = defaultWordImage;
                else
                    wordImage.sprite = tepSprite;

                if (tepTBSprite == null)
                    titleBg.sprite = defaultWordTitleImage;
                else
                    titleBg.sprite = tepTBSprite;

            }
                break;
        }


        description.text = word.description;

        if (word.wordKind == WordKindEnum.verb)
        {
            energy.gameObject.SetActive(true);

            title.text = "      " + word.wordName;

            needCD.text = /*word.wordName*/((AbstractVerbs)word).needCD.ToString();

        }
        else
        {
            title.text = word.wordName;
            energy.gameObject.SetActive(false);
        }

        if (isDetail)
        {
            ReturnDetailInfo();
            ChangeDetailInfo();
        }

        //碰撞词条
        textCollider.text = "";
        var _s = word.DetailLable();
        int _once = 0;
        if (_s == null)
        {
        }
        else
        {
            for (int i = 0; (_once == 0) && i < _s.Length; i++)
            {
                if (_s[i] == "JiHuo")
                {
                    textCollider.text = "激活";
                    _once = 1;
                }

                if (_s[i] == "ChongNeng")
                {
                    textCollider.text = "充能";
                    _once = 1;
                }

                if (_s[i] == "SanShe")
                {
                    textCollider.text = "散射";
                    _once = 1;
                }

                if (_s[i] == "ChuanBoCollision")
                {
                    textCollider.text = "传播";
                    _once = 1;
                }

                if (_s[i] == "XuWu_YunSu")
                {
                    textCollider.text = "虚无";
                    _once = 1;
                }
            }
        }


    }


    public void ChangeInformation(WordTable.Data data)
    {
        if (titleBg == null)
            titleBg = title.transform.parent.GetComponent<Image>();

        nowWord = data;
        mCardAsset = AssetManager.Load<CardSO>("SO/Card", data.mAssetName);

        switch (data.mType)
        {
            case WordType.Noun:
                wordkindText.text = textNoun;
                break;
            
            case WordType.Verb:
                wordkindText.text = textVerb;
                break;
            
            case WordType.Adjective:
                wordkindText.text = textAdj;
                break;
            
            default:
                wordkindText.text = "";
                break;
        }
        
        wordImage.sprite = AssetUtils.ToSprite(mCardAsset.mainIcon);

        if (mCardAsset.titleIcon == null)
        {
            titleBg.sprite = defaultWordTitleImage;
        }
        else
        {
            titleBg.sprite = AssetUtils.ToSprite(mCardAsset.titleIcon);
        }

        description.text = mCardAsset.desc;

        if (data.mType == WordType.Verb)
        {
            energy.gameObject.SetActive(true);

            title.text = "      " + mCardAsset.wordName;

            needCD.text = data.mTriggerPower.ToString();

        }
        else
        {
            title.text = mCardAsset.wordName;
            energy.gameObject.SetActive(false);
        }

        if (isDetail)
        {
            ReturnDetailInfo();
            ChangeDetailInfo();
        }

        //碰撞词条
        textCollider.text = BattleHelper.GetShootTypeString(data.mShootType);
    }


    public void SetIsDetail(bool _bool)
    {
        isDetail = _bool;
        this.transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = _bool;
    }


    string[] info = new string[2];

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCombatScene) return;

        if (CharacterManager.instance.pause)
        {
            // if(this.transform.parent.parent.gameObject.name != "CardGroup")
            return;
        }

        isDetail = true;
        ChangeDetailInfo();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        // if (CharacterManager.instance.pause) return;
        isDetail = false;
        ReturnDetailInfo();
    }


    void ChangeDetailInfo()
    {
        //从词语中取到其lable的名称
        var _s = nowWord.mTags;
        var commonAsset = AssetManager.GetCommonAsset();
        if (_s == null || _s.Count == 0 || commonAsset == null) 
            return;
        
        for (int i = 0; i < _s.Count; i++)
        {
            //根据取到的名称 获取静态成员参数的值
            var detail = commonAsset.GetTagDetail(_s[i]);
            if (detail != null)
            {
                info[0] = detail.tagName;
                info[1] = detail.tagDesc;
                
                PoolMgr.GetInstance().GetObj(detailInfoPrefab, (obj) =>
                {
                    //生成面板的位置在这改没用，直接修改物品DetailWordInfo的位置
                    obj.transform.parent = detailParent;
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                    obj.transform.localScale = Vector3.one;
                    obj.GetComponent<DetailWordInfo>().ChangeInformation(info[0], info[1]);
                });
            }
        }

        if (detailParent != null)
        {
            if (detailParent.GetComponent<RectTransform>() != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(detailParent.GetComponent<RectTransform>());
        }

    }

    void ReturnDetailInfo()
    {
        if (detailParent == null) 
            return;
        
        foreach (var s in detailParent.GetComponentsInChildren<DetailWordInfo>())
        {
            PoolMgr.GetInstance().PushObj(detailInfoPrefab.name, s.gameObject);
        }
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }

}
