using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 各种类型墙壁
/// </summary>
/// 
public enum WallType
{
    addHP,
    move_X,
    move_Y,
    move_Rotate,
    showAndHide
}
public class OneWayMove : MonoBehaviour
{

    [Header("墙壁类型")]
    public WallType type;

    [Header("作用阵营")]
    public CampEnum camp;

    [Header("CD时间")]
    public float cdTime;
    private float cdTimer;
    private bool cdOK = true;

    [Header("MOVE相关设置")]
    /// <summary>水平方向</summary>
    private float hori_way;
    public float value1 = 10;
    /// <summary>垂直方向</summary>
    private float ver_way;
    public float value2 = 10;
    /// <summary>旋转角度</summary>
    public float angle = 1;
    /// <summary>消失时间</summary>
    public float disappearTime = 2;
    /// <summary>出口位置</summary>
    public Transform exitPoint;
    /// <summary>出口速度系数</summary>
    public float k = 0.5f;

    [Header("ADDHP相关设置")]
    public Animator animator;
    public SpriteRenderer cdshow;
    public float addRate;
    public float addAmount;
    public float AddingTime;//持续时间
    private float AddingTime_real;//持续时间计时器
    private float AddingTimer;//持续时间计时器
    private bool adding = false;



    /// <summary>判断</summary>
    [HideInInspector]public int or = 0;
    private void Start()
    {
        cdTimer = cdTime;//最开始没有cd
        AddingTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.instance.pause) return;
        if(!cdOK)cdTimer += Time.deltaTime;

        AddingTimer += Time.deltaTime;
        AddingTime_real -= Time.deltaTime;
        if (AddingTime_real <= 0)
        {
            if (adding)
            {
                //恢复结束
                animator.Play("end");
                adding = false;
            }
            AddingTime_real = 0;
        } 
        else//如果正在回血
        {
           
            if (AddingTimer > 1)//每秒回血;
            {
                 AddCampHP();
                AddingTimer = 0;
                
            }
        }

        if (cdTimer < cdTime) return;//cd没加载好，什么也没发生

        //cd触发：
        if (type == WallType.move_X)//水平方向
        {
            Move_XUpdate();
        }
        else if (type == WallType.move_Y)//竖直方向
        {
            Move_YUpdate();
        }
        else if (type == WallType.move_Rotate)//定点旋转
        {
            Move_RotateUpdate();
        }

        cdTimer = 0;//重置cd
        cdOK = true;
        if (cdshow == null) return;
        cdshow.color = Color.white;
    }


    #region update




    void Move_XUpdate()
    {
        hori_way = Mathf.PingPong(Time.time, value1);
        transform.position = new Vector3(hori_way, transform.position.y, transform.position.z);
    }
    void Move_YUpdate()
    {
 ver_way = Mathf.PingPong(Time.time, value2);
        transform.position = new Vector3(transform.position.x, ver_way, transform.position.z);
    }
    void Move_RotateUpdate()
    {
        transform.RotateAround(this.transform.position, Vector3.forward, angle);
    }

    #endregion



    /// <summary>
    /// 治疗机关，每秒恢复血量
    /// </summary>
    private void AddCampHP()
    {
        if (camp == CampEnum.left)
        {
            AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
            for (int i = 0; i < left.Length; i++)
            {
               
                left[i].BeCure(addRate * left[i].maxHp* left[i].maxHpMul + addAmount, true, 0, left[i]);
            }

        }
        else if (camp == CampEnum.right)
        {
            AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
            for (int i = 0; i < right.Length; i++)
            {
                
                right[i].BeCure(addRate * right[i].maxHp * right[i].maxHpMul + addAmount, true, 0, right[i]);
            }
        }
    }

    /// <summary>
    /// 墙壁消失后再出现
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!cdOK) return;
        if (collision.transform.tag == "bullet")
        {

            //Destroy(collision.gameObject);
            
            if (type==WallType.showAndHide)//消失后再出现
            {
                this.gameObject.SetActive(false);
                Invoke("EnableBack", disappearTime);
               
            }
            if (type == WallType.addHP)//治疗机关
            {
                //开始回血
                cdOK = false;
                animator.Play("start"); 
                adding = true;
                AddingTime_real += AddingTime;
                cdshow.color = Color.grey;
            }
        }
    }
    public void EnableBack()
    {
        this.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (or == 4)//单向传送门（出口无碰撞）
        {
            if (collision.transform.tag == "bullet")
            {
                Vector3 a = collision.GetComponent<Rigidbody2D>().velocity * k;

                while (a.magnitude <= 1)//防止速度过慢
                {
                    a = collision.GetComponent<Rigidbody2D>().velocity * 2;
                }
                collision.transform.position = exitPoint.position + a;
            }
        }
    }
}
