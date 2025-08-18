using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CharacterDetail_t : MonoBehaviour
{
    private GameObject nowObj;
    private UnitViewBase nowCharacter;
    [Header("手动设置4个面板")]
    public Transform panel_state;
    public Transform panel_item;
    public Transform panel_skill;
    public Transform panel_bg;
    [Header("手动设置buffGourp父物体（1234）")]
    public Transform buffP;

    [Header("手动设置label父物体（1234）")]
    public Transform labelP;
    public Button label_state;
    [Header("手动设置角色基本信息")]
    public Image bookIcon;
    private string bookAdr = "WordImage/Book/";
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameTrait;
    public Image sprite;
    private string spriteAdr = "WordImage/Character/Detail/";

    [Header("生成词条的相关设置")]
    public GameObject itemPerfab;
    public GameObject buffPerfab;
    public GameObject skillPerfab;
    public GameObject energyPerfab;
    public Color colorHasEnergy;
    public Color colorNoEnergy;
    private float energyOffset = 60;//一行之间的每两个能量值中间的间隔大小
    private float energyOffsetWith = 150;//第一个能量值在x轴上向右的位移
    private Dictionary<string,int> itemDic=new Dictionary<string, int>();
    private Dictionary<string, int> buffDic = new Dictionary<string, int>();

    [Header("手动设置信息预制体")]
    public GameObject infoPerfab;
    private void OpenInit()
    {

        label_state.Select();
        CharacterManager.instance.pause = true;
        Time.timeScale = 0f;

        SetPanal(0);

        itemDic.Clear();

        var role = nowCharacter.Role;
        var asset = nowCharacter.Asset;

        //baseInfo
        Sprite _s1 = AssetManager.Load<Sprite>(bookAdr, role.Data.mBook.ToString());
        if (_s1 == null) 
            _s1 = AssetManager.Load<Sprite>(bookAdr, "HongLouMeng");
        bookIcon.sprite = _s1;

        nameText.text = asset.unitName;
        nameTrait.text = asset.roleName;
        
        sprite.sprite = AssetUtils.ToSprite(asset.sprite);

        //panel1
        panel_state.GetComponentInChildren<Slider>().value = role.Hp / role.MaxHp;
        panel_state.GetComponentsInChildren<Text>()[0].text = ((int)role.Hp).ToString() + "/" + ((int)role.MaxHp).ToString();

        //atk
        panel_state.GetComponentsInChildren<Text>()[1].text = ((int)role.GetAttributeValue(AttributeType.Attack)).ToString();
           // +" \n<color=#787878><size=25>( " + nowCharacter.atk.ToString() + " * "+(nowCharacter.atkMul*100).ToString()+ "% </size></color> )";

       //def
        panel_state.GetComponentsInChildren<Text>()[3].text = ((int)role.GetAttributeValue(AttributeType.Def)).ToString(); 
           // +" \n<color=#787878><size=25>( " + nowCharacter.def.ToString() + " * " + (nowCharacter.defMul * 100).ToString() + "%</size></color>  )";

        //san
        panel_state.GetComponentsInChildren<Text>()[4].text = ((int)role.GetAttributeValue(AttributeType.San)).ToString();
          //+ " \n<color=#787878><size=25>( " + nowCharacter.san.ToString() + " * " + (nowCharacter.sanMul * 100).ToString() + "%</size></color> )";

        //psy
        panel_state.GetComponentsInChildren<Text>()[2].text = ((int)role.GetAttributeValue(AttributeType.Psy)).ToString();
      //+"\n<color=#787878><size=25> ( " + nowCharacter.psy.ToString() + " * " + (nowCharacter.psyMul * 100).ToString() + "%</size></color> )";


        buffDic.Clear();
        foreach (var buff in nowCharacter.GetComponents<AbstractBuff>())
        {
            if (buffDic.ContainsKey(buff.buffName))
            {
                buffDic[buff.buffName] += 1;
            }
            else
            {   buffDic.Add(buff.buffName, 1);
                //生成对应的
                PoolMgr.GetInstance().GetObj(buffPerfab, (obj) =>
                {
                    obj.GetComponent<BuffDetail>().wordname = buff.GetType().ToString();
                    obj.transform.parent = buffP;
                    obj.transform.localScale = Vector3.one;
                    obj.name = buff.buffName;
                    Sprite buffSprite = Resources.Load<Sprite>("WordImage/Buffs/" + buff.GetType().ToString());
                    if (buffSprite == null)
                        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Buffs/Default");
                    else
                        obj.GetComponent<Image>().sprite = buffSprite;
                });
             
            }
            buffP.Find(buff.buffName).GetComponentInChildren<TextMeshProUGUI>().text = buffDic[buff.buffName].ToString();
        }
        
        
        //panel2
        //获取角色的名词列表
        var NounList = role.WordComponent.GetWordsByType(WordType.Noun);
        foreach (var item in NounList)
        {
            string wordName = item.mData.mName;
            if (itemDic.ContainsKey(wordName))
            {
                itemDic[wordName] += 1;
                panel_item.transform.Find(wordName).GetComponentInChildren<TextMeshProUGUI>().text = $"{wordName}    x{itemDic[wordName]}";
            }
            else
            {
                itemDic.Add(wordName, 1);
                
                //生成对应的
                PoolMgr.GetInstance().GetObj(itemPerfab, (obj) =>
                {
                    var wordDetail = obj.GetComponent<SeeWordDetail>();
                    wordDetail.Setup(item.mData);
                    
                    obj.name = wordName;
                    obj.transform.parent = panel_item;
                    obj.transform.localScale = Vector3.one;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = wordName;
                });
            }
           
        }
        
        
        //panel3
        //获取角色的动词列表
        var verbList = role.WordComponent.GetWordsByType(WordType.Verb);
        
        if (verbList.Count > 3) 
            print(role.Data.mName + "技能数超过3个");

        for (int x = 0; x < verbList.Count; x++)
        {
            var word = verbList[x];
            string wordName = word.mData.mName;
            
            PoolMgr.GetInstance().GetObj(skillPerfab, (obj) =>
            {
                var wordDetail = obj.GetComponentInChildren<SeeWordDetail>();
                wordDetail.Setup(word.mData);

                var cardAsset = AssetManager.Load<CardSO>("SO/Card", word.mData.mAssetName);
                
                obj.transform.parent = panel_skill;
                obj.transform.localScale = Vector3.one;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = wordName;
                obj.transform.Find("word_verb").GetComponent<Image>().sprite = AssetUtils.ToSprite(cardAsset.titleIcon);

                for (int i = 0; i < word.mData.mTriggerPower; i++)
                {
                    PoolMgr.GetInstance().GetObj(energyPerfab, (o) =>
                    {
                        o.transform.parent = obj.transform.GetChild(1);
                        o.transform.localScale = Vector3.one * 0.6f;
                        o.transform.localPosition = new Vector3(i * energyOffset + energyOffsetWith, 0, 0);
                        o.GetComponent<Image>().color = (i < word.mPower) ? colorHasEnergy : colorNoEnergy;
                        o.transform.GetChild(0).gameObject.SetActive((i < word.mPower) ? true : false);
                    });
                }
            });
        }
        
        //panel4
        panel_bg.GetComponentInChildren<Text>().text = asset.infoDetail;
    }

    private void SetPanal(int i)
    {
        switch (i)
        {
            case 0:
                {
                    panel_state.gameObject.SetActive(true);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(true);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(false);
                } break;
            case 2:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(true);
                    panel_bg.gameObject.SetActive(false);
                } break;
            case 3:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(true);
                } break;
        }
    }

    #region 关联外部函数

    public void CloseDetail()
    {
        Camera.main.GetComponent<MouseDown>().CloseDetail();
        Destroy(this.transform.parent.gameObject);
        CharacterManager.instance.pause = false;
        Time.timeScale = GameMgr.instance.timeSpeed;
    }
    public void SetCharacter(GameObject _ac)
    {
        nowObj = _ac;
        nowCharacter = _ac.GetComponentInChildren<UnitViewBase>();
        OpenInit();

    }
    public void ClickLabel_state()
    {
        SetPanal(0);
    }
    public void ClickLabel_item()
    {
        SetPanal(1);
    }
    public void ClickLabel_skill()
    {
        SetPanal(2);
    }
    public void ClickLabel_bg()
    {
        SetPanal(3);
    }

    public void ClickBuff()
    {
      
    }
    public void ClickTrait()
    {
        var a = Instantiate(infoPerfab, this.transform);
        a.GetComponent<DetailInfo>().SetInfo(nowCharacter.Asset.roleName,nowCharacter.Asset.roleInfo);
    }
    #endregion
}
