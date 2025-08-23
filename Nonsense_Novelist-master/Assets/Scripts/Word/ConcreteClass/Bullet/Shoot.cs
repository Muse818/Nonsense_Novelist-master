using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Unity.Burst.CompilerServices;
using TMPro;
using UnityEngine.EventSystems;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

/// <summary>
/// 发射词弹
/// </summary>
public class Shoot : MonoBehaviour
{
    /// <summary>发射位置</summary>
    public Transform gang;
    /// <summary>词条</summary>
    public GameObject bullet;
    /// <summary>词条</summary>
    private GameObject bulletInstance;
    /// <summary>发射后词条的父物体</summary>
    public Transform afterShootTF;
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
    public Slider aimSlider;
    /// <summary>存储WordCollisionShoot的词条属性</summary>
    public static AbstractWord0 abs;
    /// <summary>手动，词条信息板 </summary>
    public WordInformation information;
    /// <summary>手动，轨迹脚本 </summary>
    public Track track;
    public float dotScale = 0.15f;
    private Vector2 normall;
    public static Vector2 pointt;
    private LayerMask mask = 9;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    public float ra = 1f;
    //新词条预制体
    public GameObject new_Word;
    Transform shootChild;

    WordCollisionShoot wcs;
    private void Start()
    {
        shootChild = GameObject.Find("combatCanvas").transform.Find("ShootTime");
        wcs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>();
    }
    private void Update()
    {
        if (CharacterManager.instance.pause) return;
        if(wcs==null) wcs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>();
        if (CreateOneCharacter.isTwoSides && CreateOneCharacter.isAllCharaUp)
        {
            aimSlider.value = 0; // 重置slider的值
            WordGrid();
            if (crtForce >= maxForce && !fired)// 蓄力到最大值
            {
                crtForce = maxForce;
            }

            if (!Input.GetButton("Fire1"))
            {
                track.Hide();
            }


            if (Input.GetButtonDown("Fire1"))
            {
                crtForce = minForce; // 重置力的大小
                fired = false; // 设置开火状态为未开火
                track.Show();
            }
            else if (Input.GetButton("Fire1") && !fired)// 一直按着
            {
                crtForce += forceSpeed * Time.deltaTime; // 蓄力
                aimSlider.value = crtForce / maxForce; // 更新slider的值


                //2d射线检测需要指定参与碰撞的layer 并且添加射线长度，否则会失效
                LayerMask layer = 1 << 9;
                if (gang.childCount == 0) { print("gang.GetChild(0)=null"); ReadyWordBullet(); };
                hit = Physics2D.Raycast(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), gang.GetChild(0).transform.up, Mathf.Infinity, layer);
                //Debug.DrawLine(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), hit.point, Color.black);
                hit2 = Physics2D.CircleCast(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), ra, gang.GetChild(0).transform.up, Mathf.Infinity, layer);
                if (hit2.collider != null)
                {

                    pointt = hit2.point;
                    normall = hit2.normal;
                }
                Vector3 re = Track.Reflectt(gang.GetChild(0).transform.up, normall);
                track.UpDateDots0(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), dotScale * gang.GetChild(0).transform.up * crtForce, pointt, dotScale * re * crtForce);
            }
            else if (Input.GetButtonUp("Fire1") && !fired&&wordCount!=0)
            {
                ShootWordBullet();
                track.Hide();
            }
        }
    }


    private Vector3 oriScale;
    /// <summary>
    /// 下一个词条小球准备
    ///点击start后，在CreateOneCharacter 中调用一次
    /// </summary>
    public void ReadyWordBullet()
    {
       
        bulletInstance = Instantiate(bullet);

        //预制体相关
        bulletInstance.transform.SetParent(gang);
        bulletInstance.transform.localPosition = Vector3.zero;
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        oriScale = bulletInstance.transform.localScale;
        bulletInstance.transform.localScale = Vector3.zero;
        bulletInstance.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        /*//给小球增加词条属性【原版】
        abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord = 
            bulletInstance.AddComponent(GameMgr.instance.GetNowListOne()) as AbstractWord0;*/
        
        
       
        if (!CreateOneCharacter.firstUseCardlist)//true，开始之后抽一个
        {
            //给小球增加词条属性【测试】
            abs = wcs.absWord =
                bulletInstance.AddComponent(GameMgr.instance.GetGoingUseListOne()) as AbstractWord0;
        }
        else//开始时抽3个
        {
            if (GameMgr.instance == null) print("null!!!");
            if (wcs == null) wcs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>();
            if (wcs.absWord == null) print("null3333");
            if (bulletInstance == null) print("null3asdadasd");
            if (GameMgr.instance.GetGoingUseList() == null) print("null3a555555d");
            //给小球增加词条属性【测试】
            abs = wcs.absWord =
                bulletInstance.AddComponent(GameMgr.instance.GetGoingUseList()) as AbstractWord0;
        }
        //小球信息
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);

        //

    }
    bool isShoot = false;
    /// <summary>
    /// 产生词条实体
    /// </summary>
    void ShootWordBullet()
    {
        fired = true; // 设置开火状态为已开火
        //给词条添加一个初始的力
        bulletInstance.transform.localScale = oriScale;
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * crtForce * forceAmount);
        bulletInstance.transform.SetParent(afterShootTF);
        bulletInstance.GetComponent<Collider2D>().isTrigger = false;

        bulletInstance.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);

        //if (bulletInstance.TryGetComponent<AbstractVerbs>(out var _v))
        //{
        //    print("_v" + _v.wordName);
        //    GameMgr.instance.DeleteCardList(_v);
        //}

        //清空待使用牌库第一个
        GameMgr.instance.UseCard(GameMgr.instance.wordGoingUseList[0]);
        GameMgr.instance.wordGoingUseList.RemoveAt(0);

        print("ReadyWordBullet");
        GameMgr.instance.RefreshNowList();

        //槽位变量
        wordCount--;
        isShoot = true;

        ReadyWordBullet();
        DestroyWordBullet();
    }
    float gridTime = 3f;
    float timer = 0f;
    int wordCount = 3;
    public static int sumWordCount = 3;
    /// <summary>
    /// 槽位更新
    /// </summary>
    public void WordGrid()
    {
        if (isShoot)//发射1次刷新1次
        {
            timer += Time.deltaTime;
            if (timer > gridTime)
            {
                timer = 0;
                wordCount++;
                if (wordCount == sumWordCount)
                {
                    isShoot = false;
                }
            }
        }
        //开始时解锁的三个初始化
        shootChild.GetChild(0).GetComponent<Slider>().value = 1;
        shootChild.GetChild(1).GetComponent<Slider>().value = 1;
        shootChild.GetChild(2).GetComponent<Slider>().value = 1;
        shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
        shootChild.GetChild(1).GetChild(3).gameObject.SetActive(true);
        shootChild.GetChild(2).GetChild(3).gameObject.SetActive(true);
        GetTree(0);
        GetTree(1);
        GetTree(2);
        if (wordCount == 3 && wordCount <= sumWordCount)
        {

            /*          a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
                        a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
                        a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[2] + "");
            */
            //CreatePrefab(3);
            shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
            shootChild.GetChild(1).GetChild(3).gameObject.SetActive(true);
            shootChild.GetChild(2).GetChild(3).gameObject.SetActive(true);
            GetTree(0);
            GetTree(1);
            GetTree(2);
        }
        else if (wordCount == 2 && wordCount <= sumWordCount)
        {
            if (sumWordCount == 3)//3号在加载
            {
                shootChild.GetChild(2).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();
                shootChild.GetChild(2).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(true);
                GetTree(0);
                GetTree(1);
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
                //CreatePrefab(3);

            }
            else if (sumWordCount == 2)//全部展现
            {
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
                // CreatePrefab(2);
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(true);
                GetTree(0);
                GetTree(1);
            }
        }
        else if (wordCount == 1 && wordCount <= sumWordCount)
        {
            if (sumWordCount == 3)//2号加载，3号排队
            {
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
                shootChild.GetChild(1).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();
                shootChild.GetChild(2).GetComponent<Slider>().value = 0;
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(2).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
                GetTree(0);
                //加载排队的图片
                //a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>("");

            }
            else if (sumWordCount == 2)//2号加载
            {
                shootChild.GetChild(1).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
                GetTree(0);
            }
            else if (sumWordCount == 1)//全部展现
            {
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(true);
                GetTree(0);
                
            }
        }
        else if (wordCount == 0 && wordCount <= sumWordCount)
        {
            if (sumWordCount == 3)//1号加载，2、3号排队
            {
                shootChild.GetChild(0).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();
                shootChild.GetChild(1).GetComponent<Slider>().value = 0;
                shootChild.GetChild(2).GetComponent<Slider>().value = 0;
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(2).GetChild(3).gameObject.SetActive(false);
                //排队
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
                //a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");

            }
            else if (sumWordCount == 2)//1号加载，2号排队
            {
                shootChild.GetChild(0).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();                shootChild.GetChild(1).GetComponent<Slider>().value = 0;
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(false);
                shootChild.GetChild(1).GetChild(3).gameObject.SetActive(false);
                //排队
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");

            }
            else if (sumWordCount == 1)//1号加载
            {
                shootChild.GetChild(0).GetComponent<Slider>().value = (float)(timer / 3f);
                DestroyComponent();
                shootChild.GetChild(0).GetChild(3).gameObject.SetActive(false);

            }
        }
    }
    void GetTree(int i) {
        if (GameMgr.instance.wordGoingUseList[i] == null)
        {//可能原因：
            shootChild.GetChild(i).GetChild(3).GetComponentInChildren<UnityEngine.UI.Text>().text = "无";
            return;
        }
        if (shootChild.GetChild(i).GetChild(3).gameObject.GetComponent<AbstractWord0>()==null)
        {
            AbstractWord0 abs00 = shootChild.GetChild(i).GetChild(3).gameObject.AddComponent(GameMgr.instance.wordGoingUseList[i]) as AbstractWord0;
            shootChild.GetChild(i).GetChild(3).GetComponentInChildren<UnityEngine.UI.Text>().text = abs00.wordName;
        }
    }
    void DestroyComponent()
    {
        for(int i = 0; i < 3; i++)
        {
            if (shootChild.GetChild(i).GetChild(3).gameObject.GetComponent<AbstractWord0>() != null)
            {
                Destroy(shootChild.GetChild(i).GetChild(3).gameObject.GetComponent<AbstractWord0>());
            }
            if (shootChild.GetChild(i).GetChild(3).gameObject.GetComponent<AbstractSkillMode>() != null)
            {
                Destroy(shootChild.GetChild(i).GetChild(3).gameObject.GetComponent<AbstractSkillMode>());

            }
        }
    }
    public void CreatePrefab(int mm)
    {
        for (int i = 0; i < mm; i++)
        {
            PoolMgr.GetInstance().GetObj(new_Word, (obj) =>
            {
                var word = obj.AddComponent(GameMgr.instance.wordGoingUseList[i]) as AbstractWord0;

                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = shootChild.GetChild(i);
                obj.transform.localScale = Vector3.one;
                obj.GetComponentInChildren<Image>().SetNativeSize();

                if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                    _s.SetPic(word);

            });
        }
    }
    private void DestroyWordBullet()
    {
        if (afterShootTF.childCount > 10)
        {
            Destroy(afterShootTF.GetChild(0).gameObject);
        }
    }
}
