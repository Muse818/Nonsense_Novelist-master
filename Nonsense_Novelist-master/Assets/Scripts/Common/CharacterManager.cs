using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 挂在父物体上.负责角色和situation的单例
/// </summary>
public class CharacterManager : MonoSingleton<CharacterManager>
{
    public GameObject endGame;

    public static GameObject father;
    /// <summary>当下全部角色</summary>
    private AbstractCharacter[] Charas;
    private List<AbstractCharacter> Strangers=new List<AbstractCharacter>();
    bool hasEnd = false;
    public AbstractCharacter[] charas
    {
        get
        {
            //获取全部角色
            Charas = GetComponentsInChildren<AbstractCharacter>();
            return Charas;
        }
        set
        {
            Charas = value;
        }
    }
    /// <summary>左侧角色</summary>
    public  static List<AbstractCharacter> charas_left = new List<AbstractCharacter>();
    /// <summary>右侧角色</summary>
    public static List<AbstractCharacter> charas_right = new List<AbstractCharacter>();

    /// <summary>
    /// int=situation的number。方便快捷寻找situation的自典
    /// </summary>
    public static Dictionary<float, Situation> situationDic = new Dictionary<float, Situation>();
    public void RefreshStanger()
    {
        Strangers.Clear();

        foreach (var _m in charas)
        {
            if (_m.Camp == CampEnum.stranger)
            {
                Strangers.Add(_m);
            }
        }
     
    }

    public AbstractCharacter[] GetStranger()
    {
      
        if (Strangers .Count!= 0)
            return Strangers.ToArray();
        else
            return null;
    }

    [HideInInspector] public static SpriteRenderer[] spSituations = new SpriteRenderer[12];

    #region pauseSetting

    public void EndGame()
    {
        if (hasEnd) return;
        //if (pause) return;
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        Instantiate(endGame);
        hasEnd = true;
        pause = true;
    }
    private bool PAUSE;
    /// <summary>
    /// 手动设置设置。同时，发射器弃用禁用、子弹速度也会被同时设置。所有的角色都会脱离攻击状态
    /// true暂停false开始
    /// </summary>
    public bool pause
    {
        get { return PAUSE; }
        set
        {
            PAUSE = value;
            SetShooterTo(PAUSE);
            WordBallPause(PAUSE);
            AllCharaAnimPause(PAUSE);
        }
    }
    private Transform shooter;
    private void SetShooterTo(bool _b)
    {//true,暂停，此时关闭
        if (SceneManager.GetActiveScene().name == "ShootCombat")
        {
            if(shooter==null) shooter = GameObject.Find("shooter").transform;
            shooter.GetComponent<Shoot>().enabled = (!_b);
        } 
        // else shooter.GetComponent<TestShoot>().enabled = (!_b);
        // shooter.GetComponent<RollControler>().enabled = (!_b);
    }

    Vector2[] wordVtemp = new Vector2[100];
    float[] wordAVtemp = new float[100];
    private void WordBallPause(bool _b)
    {

        if (_b)//暂停的时候
        {
            var obj = GameObject.Find("AfterShootTF").GetComponentsInChildren<Rigidbody2D>();
            for (int i = 0; i < obj.Length; i++)
            {
                wordVtemp[i] = obj[i].velocity;
                wordAVtemp[i] = obj[i].angularVelocity;
                obj[i].velocity = Vector2.zero;
                obj[i].angularVelocity = 0;
            }
        }
        else//开始的时候
        {
            var obj = GameObject.Find("AfterShootTF").GetComponentsInChildren<Rigidbody2D>();
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].velocity = wordVtemp[i];
                obj[i].angularVelocity = wordAVtemp[i];
            }
        }

    }



    private void AllCharaAnimPause(bool _b)
    {
        foreach(var _c in this.GetComponentsInChildren<AbstractCharacter>())
        {
            if (_b)
            {
                _c.AttackSpeedPause(true);
            }
            else
            {
                _c.AttackSpeedPause(false);
            }
        }
    }
    #endregion


    public override void Awake()
    {
        base.Awake();
        Charas = GetComponentsInChildren<AbstractCharacter>();
        father = this.gameObject;
        shooter = GameObject.Find("shooter").transform;



    }


    private void Start()
    {
        GetAllSituation();
        if (situationDic.Count == 0) print("初始化Situation字典失败");
    }


    /// <summary>
    /// 获取所有的situation，存入字典
    /// </summary>
    static private void GetAllSituation()
    {
        Situation[] _sits;
        _sits = GameObject.Find("AllCharacter").GetComponentsInChildren<Situation>();

        for (int i = 0; i < _sits.Length; i++)
        {
            spSituations[i] = _sits[i].GetComponent<SpriteRenderer>();
            if (!situationDic.ContainsKey(_sits[i].number))
                situationDic.Add(_sits[i].number, _sits[i]);

        }


    }

    Coroutine coroutineColor = null;
    public void SetSituationColorClear(int _speed)
    {
        if (coroutineColor != null) StopCoroutine(coroutineColor);
        coroutineColor = StartCoroutine(ColorClear(_speed));
    }
    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    IEnumerator ColorClear(int _speed)
    {
        int count = 0;
        while(spSituations[0].color.a>0.05f&&count<120)
        {
       
            yield return wait;
            foreach (var _sp in spSituations)
            {
                _sp.color -= Color.white * 1 / 100 * _speed;
            }
            count++;
        }
    }

   


    /// <summary>
    /// 返回与输入的Situation相邻的situation的数值
    /// </summary>
    /// <param name="a">需要计算相邻situation的点</param>
    /// <returns>Situation[0]和Situation[1]（和Situation[2]）</returns>
    public AbstractCharacter[] GetNearBy_C(Situation a)
    {
        AbstractCharacter[] _resSits = new AbstractCharacter[3];

        switch (a.number)
        {
            case 1: { _resSits[0] = situationDic[2].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[3].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 2: { _resSits[0] = situationDic[1].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[4].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 3: { _resSits[0] = situationDic[1].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[4].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = situationDic[4.5f].GetComponentInChildren<AbstractCharacter>(); } break;
            case 4: { _resSits[0] = situationDic[2].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[3].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 5: { _resSits[0] = situationDic[6].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[7].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = situationDic[4.5f].GetComponentInChildren<AbstractCharacter>(); } break;
            case 6: { _resSits[0] = situationDic[5].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[8].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 7: { _resSits[0] = situationDic[5].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[8].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 8: { _resSits[0] = situationDic[6].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[7].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
        }
        return _resSits;
    }



    float bulletSpeed=3.3f;
    public float DelayAccount(float _startP,float _endP)
    {
        float count =( (Mathf.Abs(_startP - _endP)) / bulletSpeed);

        return count;
    }

    public List<AbstractCharacter> GetEnemy(CampEnum _your)
    {
        if (_your == CampEnum.left) return charas_right;
        else if (_your == CampEnum.right) return charas_left;
        else return charas_left;
    }

    public List<AbstractCharacter> GetFriend(CampEnum _your)
    {
        if (_your == CampEnum.left) return charas_left;
        else if (_your == CampEnum.right) return charas_right;
        else return charas_left;//中立
    }



}
