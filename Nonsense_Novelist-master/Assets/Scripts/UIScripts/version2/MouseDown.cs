using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

///<summary>
///（挂在main camera上）鼠标点击物体 显示信息
///</summary>
class MouseDown : MonoBehaviour
{
    /// <summary>属性面板预制体</summary>
    public GameObject characterDetailPrefab;
    /// <summary>UIcanvas</summary>
    public Canvas otherCanvas;
    /// <summary>预制体的克隆体</summary>
    private static GameObject a;
    /// <summary>预制体中的文字</summary>
    private Text[] texts=new Text[2];
    /// <summary>抽象角色</summary>
    [HideInInspector]
    public UnitViewBase abschara;
    /// <summary>抽象角色身份</summary>
    [HideInInspector]
    public BattleUnit absRole;
    /// <summary>抽象形容词</summary>
    [HideInInspector]
    public AbstractAdjectives absAdj;
    /// <summary>抽象动词</summary>
    [HideInInspector]
    public AbstractVerbs absVerbs;
    /// <summary>抽象性格</summary>
    [HideInInspector]
    public AbstractTrait absTrait;
    /// <summary>判断当前是否有角色信息展示面板</summary>
    public bool isShow = false;
    /// <summary>音效</summary>
    public AudioSource audioSource;
    /// <summary>发射器</summary>
    public GameObject shoot;
   
    private void Update()
    {
        if (CharacterManager.instance.pause) return;
        if (isShow) return;
        MouseDownView();
        
    }
    bool[] downs = new bool[5];
    Type[] res = new Type[5];
    Dictionary<int,Type> word_=new Dictionary<int,Type>();
    
    /// <summary>
    /// 全局鼠标射线
    /// </summary>
    private void MouseDownView()
    {
        
        if (Input.GetMouseButtonDown(1))//按下右键
        {
            //
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero, Mathf.Infinity, (1 << 10) | (1 << 3));
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "character" && !isShow)
                {
                    //播放点击音效
                    //audioSource.Play();

                    //生成角色信息面板
                    a = Instantiate(characterDetailPrefab, otherCanvas.transform);

                    isShow = true;

                    //获取点击角色的脚本信息
                    abschara = hit.collider.GetComponentInChildren<UnitViewBase>();
                    absRole = abschara.Role;

                    a.GetComponentInChildren<CharacterDetail_t>().SetCharacter(hit.collider.gameObject);

                    //if (hit.collider.GetComponent<AbstractAdjectives>())
                    //{
                    //    absAdj = hit.collider.GetComponent<AbstractAdjectives>();
                    //}
                    //else if (hit.collider.GetComponent<AbstractVerbs>())
                    //{
                    //    absVerbs = hit.collider.GetComponent<AbstractVerbs>();
                    //}

                }
                //锁定发射槽位
                else if (hit.collider.gameObject.tag == "Slider" && hit.collider.gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name != "排队")
                {
                    print("sh"+Shoot.sumWordCount);
                    //锁定【表现上交换图片，逻辑上移除当前卡牌直到解锁时add】
                    GameObject tt = hit.collider.gameObject;
                    GameObject jj = GameObject.Find("combatCanvas");
                    if (tt.name == "Slider0")
                    {
                        if (downs[0] == false)
                        {
                            downs[0] = true;
                            print("SD0");
                            print(Shoot.sumWordCount - 1);
                            //暂存+移除数组中的
                            word_.Add(0, GameMgr.instance.wordGoingUseList[0]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(0);
                            //表现层替换0与Shoot.sumWordCount-1的图片，总开放槽位-1
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;

                            Shoot.sumWordCount--;

                        }
                        else
                        {//解锁
                            downs[0] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[0]);
                            word_.Remove(0);
                            print("JS");
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider1")
                    {
                        if (downs[1] == false)
                        {
                            downs[1] = true;
                            word_.Add(1, GameMgr.instance.wordGoingUseList[1]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(1);
                            //表现层替换1与Shoot.sumWordCount-1的图片
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetComponent<Image>().sprite = ss;
                            print("SD1");
                            Shoot.sumWordCount--;

                        }
                        else
                        {//true,解锁
                            downs[1] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[1]);
                            word_.Remove(1);
                            print("JW1");
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider2")
                    {
                        if (downs[2] == false)
                        {
                            downs[2] = true;
                            word_.Add(2, GameMgr.instance.wordGoingUseList[2]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(2);
                            //表现层替换2与Shoot.sumWordCount-1的图片
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            print("SD2");
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[2] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[2]);
                            word_.Remove(2);
                            print("JS2");
                            Shoot.sumWordCount++;

                        }

                    }
                    //其他两个槽位解锁后
                    else if (tt.name == "Slider3" && Shoot.sumWordCount >= 4)
                    {
                        if (downs[3] == false)
                        {
                            downs[3] = true;
                            word_.Add(3, GameMgr.instance.wordGoingUseList[3]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(3);
                            //表现层替换2与Shoot.sumWordCount-1的图片
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[3] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[3]);
                            word_.Remove(3);
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider4" && Shoot.sumWordCount == 5)
                    {
                        if (downs[4] == false)
                        {
                            downs[4] = true;
                            word_.Add(4, GameMgr.instance.wordGoingUseList[4]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(4);
                            //表现层替换2与Shoot.sumWordCount-1的图片
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[4] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[4]);
                            word_.Remove(4);
                            Shoot.sumWordCount++;

                        }
                    }
                }
            
            }
        }
    }

    public void CloseDetail()
    {
        isShow = false;
    }    
}
