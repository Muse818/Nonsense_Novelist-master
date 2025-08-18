using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class TestShoot : MonoBehaviour
{
    /// <summary>发射位置</summary>
    private GameObject gang;
    /// <summary>词条</summary>
    public GameObject bullet;
    /// <summary>词条</summary>
    private GameObject bulletInstance;
    /// <summary>发射后词条的父物体</summary>
    private Transform afterShootTF;
    /// <summary>当前的力</summary>
    [SerializeField]
    private float crtForce = 0;
    /// <summary>最小力</summary>
    public float minForce = 0;
    /// <summary>最大力</summary>
    public float maxForce = 200;
    /// <summary>蓄力速度</summary>

    public float forceSpeed = 80;
    /// <summary>蓄力比值</summary>
    public float forceAmount = 1;
    /// <summary>有无发射</summary>
    private bool fired = false;
    /// <summary>蓄力Slider</summary>
    private Slider aimSlider;
    /// <summary>存储WordCollisionShoot的词条属性</summary>
    public static AbstractWord0 abs;
    /// <summary>手动，词条信息板 </summary>
    private WordInformation information;
    private Type clickWord;
    private void Start()
    {
       
        if (SceneManager.GetActiveScene().name == "CombatTest")
        {
            gang = GameObject.Find("shooter").transform.GetChild(0).gameObject;
            information = GameObject.Find("combatCanvas").GetComponentInChildren<WordInformation>();
            afterShootTF = GameObject.Find("AfterShootTF").transform;
            aimSlider = GameObject.Find("combatCanvas").transform.GetChild(2).GetComponent<Slider>();

        }
    }
    private void Update()
    {        
            if (CreateOneCharacter.isTwoSides)
            {
                aimSlider.value = 0; // 重置slider的值

                if (crtForce >= maxForce && !fired)// 蓄力到最大值
                {
                    crtForce = maxForce;
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    crtForce = minForce; // 重置力的大小
                    fired = false; // 设置开火状态为未开火                    
                }
                else if (Input.GetButton("Fire2") && !fired)// 一直按着
                {
                    crtForce += forceSpeed * Time.deltaTime; // 蓄力
                    aimSlider.value = crtForce / maxForce; // 更新slider的值
                }
                else if (Input.GetButtonUp("Fire2") && !fired)
                {
                    ShootWordBullet();
                }
            }        
    }
    public void ClickWord()
    {
        var buttonSelf = EventSystem.current.currentSelectedGameObject;
        clickWord = buttonSelf.GetComponent<AbstractWord0>().GetType();
    }
    /// <summary>
    /// 下一个词条小球准备[测试版本用]
    /// </summary>
    public void NextWordReady()
    {  
        var buttonSelf = EventSystem.current.currentSelectedGameObject;
        abs = this.GetComponent < AbstractWord0 > ();

        ReadyWordBullet();
        DestroyWordBullet();
        information.ChangeInformation(abs);

    }
    public void ReadyWordBullet()
    {

        bulletInstance = Instantiate(bullet);

        //预制体相关
        bulletInstance.transform.SetParent(gang.transform);
        bulletInstance.transform.localPosition = Vector3.zero;
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        bulletInstance.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        //给小球增加词条属性
        if (abs == null)
        {
            abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord = bulletInstance.AddComponent(AllSkills.CreateSkillWord()) as AbstractWord0;
        }
        else
        {
            bulletInstance.AddComponent(abs.GetType());
        }
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);

    }
    /// <summary>
    /// 产生词条实体
    /// </summary>
    void ShootWordBullet()
    {
        fired = true; // 设置开火状态为已开火
        //给词条添加一个初始的力
        if (bulletInstance == null) 
            bulletInstance = gang.transform.GetChild(0).gameObject; 
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * crtForce * forceAmount);
        bulletInstance.transform.SetParent(afterShootTF);
        bulletInstance.GetComponent<Collider2D>().isTrigger = false;
        bulletInstance.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        ReadyWordBullet();
        DestroyWordBullet();
    }
    private void DestroyWordBullet()
    {
        if (afterShootTF.childCount > 10)
        {
            Destroy(afterShootTF.GetChild(0).gameObject);
        }
        else if (gang.transform.childCount > 1)
        {
            Destroy(gang.transform.GetChild(0).gameObject);   
        }
    }
}
