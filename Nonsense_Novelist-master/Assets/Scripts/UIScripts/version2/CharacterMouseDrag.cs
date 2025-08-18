using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/// <summary>
/// 版本二：拖拽角色站位
/// before start
/// </summary>
public class CharacterMouseDrag : MonoBehaviour
{
    /// <summary>OnMouseDown相关参数</summary>
    private Vector3 targetScreenpos;//拖拽物体的屏幕坐标
    private Vector3 targetWorldpos;//拖拽物体的世界坐标
    private Transform target;//拖拽物体
    private Vector3 mouseScreenpos;//鼠标的屏幕坐标
    private Vector3 offset;//偏移量
    private int siblingBefore = 0;//最初的站位
    private GameObject black;

    /// <summary>记录目前所在的站位</summary>
    [HideInInspector] public Transform nowParentTF;
    /// <summary>记录上一个所在的站位</summary>
    [HideInInspector] public Transform lastParentTF;
    /// <summary>角色和站位position的Y偏移量</summary>
    public static float offsetY = 0f;

    //颜色
    private Color colorOnMouseOver = new Color((float)100 / 255, (float)100 / 255, (float)50 / 255, (float)255 / 255);
    private Color colorOnMouseExit = new Color((float)255 / 255, (float)255 / 255, (float)255 / 255, (float)255 / 255);

    private SpriteRenderer sr;
    private AudioYinXiao yinXiao;

    private string characterDetailPrefab = "UI/CharacterDetail";

    private Transform oriParent;
    private void Start()
    {
        //if (SceneManager.GetActiveScene().name == "CombatTest")
        //{
        //    beforeScale = 10;
        //}
        siblingBefore = this.transform.parent.GetSiblingIndex();

        nowParentTF = transform.parent;
        target = transform;
        sr = GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();
        oriParent = GameObject.Find("UICanvas").transform.Find("Panel").Find("charaPos");
        transform.localScale = Vector3.one * GameMgr.instance.beforeScale;
        yinXiao = GameObject.Find("yinxiaoSource").GetComponent<AudioYinXiao>();
        Debug.Log("右键单击卡片");
    }

    private void OnMouseEnter()
    {

    }

    #region OnMouseDrag()随鼠标移动(废弃)

    /* private void OnMouseDrag()
     {
         //随鼠标移动
         var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         transform.position = new Vector3(pos.x, pos.y, transform.position.z);
         
     }*/
    #endregion

    /// <summary>
    /// 鼠标悬停
    /// </summary>
    private void OnMouseOver()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return;
        //颜色变黄
        sr.color = colorOnMouseOver;

        //如果鼠标右键
        if (Input.GetMouseButtonDown(1))
        {
            if (GameObject.Find("combatCanvas").transform.Find("CharacterDetail(Clone)") == null)
            {
                var a = ResMgr.GetInstance().Load<GameObject>(characterDetailPrefab);
                a.transform.parent = GameObject.Find("combatCanvas").transform;
                a.transform.localPosition = Vector3.zero;
                a.transform.localScale = Vector3.one;
                //获取点击角色的脚本信息
                a.GetComponentInChildren<CharacterDetail>().Open(this.GetComponent<AbstractCharacter>());
            }
        }
    }
    private void OnMouseExit()
    {
        //颜色恢复
        //鼠标进入黄圈时也会执行此脚本。原因未知
        sr.color = colorOnMouseExit;

    }

    //被移动物体需要添加collider组件，以响应OnMouseDown()函数
    //基本思路。当鼠标点击物体时（OnMouseDown（），函数体里面代码只执行一次），
    //记录此时鼠标坐标和物体坐标，并求得差值。如果此后用户仍然按着鼠标左键，那么保持之前的差值不变即可。
    //由于物体坐标是世界坐标，鼠标坐标是屏幕坐标，需要进行转换。具体过程如下所示。
    IEnumerator OnMouseDown()
    {


        //先让点击物体中心与鼠标点击处同步。
        mouseScreenpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(target.position).z);
        target.position = Camera.main.ScreenToWorldPoint(mouseScreenpos)/*-new Vector3(offsetCenter.x/4,offsetCenter.y/4,0)*/;

        targetScreenpos = Camera.main.WorldToScreenPoint(target.position);
        offset = target.position - Camera.main.ScreenToWorldPoint(mouseScreenpos);


        //如果是从画布那边放过来，则
        if (nowParentTF.parent.TryGetComponent<UnityEngine.UI.HorizontalLayoutGroup>(out var _sdsd))
        {
            //画布上留下阴影
            ShowBlack();
        }

        //如果位置之间变换，则变大
        if (nowParentTF.GetComponent<Situation>() != null)
            this.transform.localScale = ScaleWithTure(GameMgr.instance.afterClickScale);


        while (Input.GetMouseButton(0))//鼠标左键被持续按下。
        {
            mouseScreenpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenpos.z);
            targetWorldpos = Camera.main.ScreenToWorldPoint(mouseScreenpos) + offset;
            target.position = targetWorldpos;
            yield return new WaitForFixedUpdate();
        }


    }
    private void OnMouseUp()
    {
        //加上了检测层级（忽略角色本身）
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("Situation"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Situation"))//角色拖拽到站位上且位置校准
            {
                if ((hit.collider.transform.childCount == 0))//如果站位上没有其它角色
                {
                    SetCharacterToSituation(hit.collider.transform.GetComponent<Situation>());
                    //成功放上去的音效
                    yinXiao.ClickPlay();
                }
                else //如果站位上有其它角色
                {
                    //如果是从画布那边放过来，则
                    if (nowParentTF.parent.TryGetComponent<UnityEngine.UI.HorizontalLayoutGroup>(out var _sdsd))
                    {
                        transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
                        DeleteBlack();
                    }
                    else
                    {
                        transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
                        this.transform.localScale = ScaleWithTure(GameMgr.instance.afterScale);
                    }
                    //弹回原位置的音效
                    yinXiao.TanHui();
                }


            }

            else//没有检测到站位
            {
                transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
                this.transform.localScale = ScaleWithTure(GameMgr.instance.afterScale);
                if (nowParentTF.parent.TryGetComponent<UnityEngine.UI.HorizontalLayoutGroup>(out var _sdsd))
                {
                    this.transform.localScale = ScaleWithTure(GameMgr.instance.beforeScale);
                    DeleteBlack();
                }
                yinXiao.TanHui();

            }
        }
        else if (Camera.main.WorldToViewportPoint(target.position).y < 0.4)
        {
            yinXiao.TanHui();

            DeleteBlack();
            this.transform.parent = oriParent.GetChild(siblingBefore);
            nowParentTF = transform.parent;

            transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
            transform.localScale = Vector3.one * GameMgr.instance.beforeScale;

            var chara = this.GetComponent<AbstractCharacter>();
            chara.Camp = CampEnum.left;
            if (CharacterManager.charas_left.Contains(chara)) CharacterManager.charas_left.Remove(chara);
            if (CharacterManager.charas_right.Contains(chara)) CharacterManager.charas_right.Remove(chara);
        }
        else//没有检测到站位
        {
            yinXiao.TanHui();

            transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
            this.transform.localScale = ScaleWithTure(GameMgr.instance.afterScale);
            if (nowParentTF.parent.TryGetComponent<UnityEngine.UI.HorizontalLayoutGroup>(out var _sdsd))
            {
                DeleteBlack(); this.transform.localScale = ScaleWithTure(GameMgr.instance.beforeScale);
            }
        }

    }


    public void SetCharacterToSituation(Situation s)
    {
        var chara = GetComponent<AbstractCharacter>();

        lastParentTF = nowParentTF;

        if (s == null)
        {

            int i = 0;
            for (; i < oriParent.childCount && oriParent.GetChild(i).childCount >= 1; i++)
            {
            }
            nowParentTF = oriParent.GetChild(i);
            this.transform.SetParent(nowParentTF);

        }
        else
        {
            nowParentTF = s.transform;
            this.transform.SetParent(nowParentTF);
        }


        if (lastParentTF.GetComponent<Situation>() == null && s != null)//如果是从放置后过来，则变大
        {
            this.transform.localScale = ScaleWithTure(GameMgr.instance.afterScale);
            this.transform.position = nowParentTF.position + GameMgr.instance.charaPosOffset;
        }
        else if (s != null)//位置之间切换
        {
            this.transform.localScale = ScaleWithTure(GameMgr.instance.afterScale);
            this.transform.position = nowParentTF.position + GameMgr.instance.charaPosOffset;
        }

        else if (lastParentTF.GetComponent<Situation>() != null && s == null)  //其它情况，则保持小
        {
            this.transform.localScale = ScaleWithTure(GameMgr.instance.beforeScale);
            this.transform.position = nowParentTF.position;
        }



        // transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);


        //隐藏/恢复站位颜色（透明度为0

        if (lastParentTF.gameObject.GetComponent<SpriteRenderer>())
            lastParentTF.gameObject.GetComponent<SpriteRenderer>().color = colorOnMouseExit;
        if (nowParentTF.gameObject.GetComponent<SpriteRenderer>())
            nowParentTF.gameObject.GetComponent<SpriteRenderer>().color = Vector4.zero;


        //根据站位给角色站位赋值
        if (s == null)//安排在初始位
        {
            chara.situation = null;
        }
        else
        {
            chara.situation = s;
        }



        //根据站位给角色阵营赋值
        if (s == null)
        {
            chara.Camp = CampEnum.left;
            if (CharacterManager.charas_left.Contains(chara)) CharacterManager.charas_left.Remove(chara);
            if (CharacterManager.charas_right.Contains(chara)) CharacterManager.charas_right.Remove(chara);
        }
        else if (s.number < 5)
        {
            //图片翻转方向
            if (chara.Camp == CampEnum.right)
            {
                this.GetComponent<AbstractCharacter>().turn();
            }
            chara.Camp = CampEnum.left;
            //去重
            if (CharacterManager.charas_right.Contains(chara))
            {
                CharacterManager.charas_right.Remove(chara);
            }
            //加入阵营
            if (s != null)
            {
                if (!CharacterManager.charas_left.Contains(chara)) CharacterManager.charas_left.Add(chara);
            }

        }
        else
        {
            if (chara.Camp != CampEnum.right)
            {
                this.GetComponent<AbstractCharacter>().turn();

            }
            chara.Camp = CampEnum.right;
            if (CharacterManager.charas_left.Contains(chara)) CharacterManager.charas_left.Remove(chara);
            if (!CharacterManager.charas_right.Contains(chara)) CharacterManager.charas_right.Add(chara);
        }

    }

    Vector3 ScaleWithTure(float muti)
    {
        return new Vector3(this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x),
                    this.transform.localScale.y / Mathf.Abs(this.transform.localScale.y),
                    this.transform.localScale.z / Mathf.Abs(this.transform.localScale.z)) * muti;
    }


    void ShowBlack()
    {

        if (black != null) return;


        var _obj = Instantiate<GameObject>(this.gameObject);

        _obj.transform.parent = nowParentTF;
        _obj.transform.localScale = Vector3.one * GameMgr.instance.beforeScale;
        _obj.transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);
        _obj.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>().color = Color.black;
        // print(_obj.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>().sortingOrder);
        _obj.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>().sortingOrder -= 1;

        _obj.GetComponentInChildren<AI.MyState0>().GetComponent<Animator>().speed = 0;

        //删除身上不必要的组件
        Destroy(_obj.GetComponentInChildren<CharacterMouseDrag>());
        Destroy(_obj.GetComponent<AbstractCharacter>());
        Destroy(_obj.GetComponentInChildren<AI.MyState0>());

        black = _obj;
    }


    void DeleteBlack()
    {
        if (black == null) return;

        Destroy(black);
        black = null;
    }
}

