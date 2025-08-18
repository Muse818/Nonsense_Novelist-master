using System;
using AI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum AttackType
{ 
    psy,//精神
    atk,//物理
    dir,//真实
    heal//治愈
}
public enum GrowType
{
    psy,//精神
    atk,//物理
    san,//真实
    def//治愈
}
//[RequireComponent(typeof(CharaAnim))]
//[RequireComponent(typeof(AI.MyState0))]
//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]


/// <summary>
/// 抽象角色类（启用，会在Awake自动关上，需要外部脚本再启用）
/// </summary>
abstract public class AbstractCharacter : AbstractWord0
{
    public MyState0 myState;

    /// <summary>序号</summary>
    [HideInInspector] public int characterID;
    /// <summary>AudioSource</summary>
    [HideInInspector] public AudioSource source;
    /// <summary>平A音效(手动拖拽）</summary>
    public AudioClip aAttackAudio;
    /// <summary>走路音效（手动拖拽）</summary>
    public AudioClip walkAudio;

    /// <summary>特效</summary>
    [HideInInspector] public TeXiao teXiao;
    /// <summary>子弹(手动挂）</summary>
    public GameObject bullet;

    /// <summary>阵营</summary>
    private CampEnum camp;
    public CampEnum Camp
    {
        get
        {
            return camp; 
        }
        set
        { 
            camp = value;
            if (wordName == "怀疑主义") return;
            Sprite _sp = ResMgr.GetInstance().Load<Sprite>("UI/hpbar_A");
            if (Camp == CampEnum.left)
            {
                _sp = ResMgr.GetInstance().Load<Sprite>("UI/hpbar_A");
            }
            else if (camp == CampEnum.right)
            {
                _sp = ResMgr.GetInstance().Load<Sprite>("UI/hpbar_B");
            }
            else if (camp == CampEnum.stranger)
                _sp = ResMgr.GetInstance().Load<Sprite>("UI/hpbar_Monster");
            if (hpSlider.transform.Find("FillArea") == null) return;
            if (_sp != null)
                hpSlider.transform.Find("FillArea").Find("Fill").GetComponent<Image>().sprite = _sp;
            
        }

    }

    /// <summary>身份名</summary>
    [HideInInspector] public string roleName;
    [HideInInspector] public string roleInfo;

    public bool isNaiMa = false;

    #region 血量

    private Slider hpSlider;

    /// <summary>当前血量</summary>
    private float HP = 0;
    virtual public float hp
    {
        get { return HP; }
        set
        {

            HP = value;
            HPSetting();
        }
    }


    /// <summary> 总血量 </summary>
    private float MaxHp = 0;
    virtual public float maxHp
    {
        get { return MaxHp; }
        set
        {
            float _o = MaxHp;
            MaxHp = value; 
            HPSetting();
            if (value > _o)
            {
               hp += (value - _o);
            }
        }
    }

    private float MaxHpMul = 1;
    virtual public float maxHpMul
    {
        get { return MaxHpMul; }
        set
        {
            MaxHpMul = value;
            HPSetting();
        }
    }

    IEnumerator DelayAttack(float _delayTime, float _value, AttackType _at, bool _hasFloat, AbstractCharacter _whoDid)
    {
        yield return new WaitForSeconds(_delayTime);
        if (_hasFloat)
        {
            if (_at == AttackType.atk) CreateFloatWord(_value, FloatWordColor.physics, true);
            if (_at == AttackType.psy) CreateFloatWord(_value, FloatWordColor.psychic, true);
            if (_at == AttackType.dir) CreateFloatWord(_value, FloatWordColor.physics, true);
            if(_value<0) CreateFloatWord(_value, FloatWordColor.heal, true);
        }

        hp -= _value;
        //执行外部委托
        if (_value >= 0)
        {
            if (event_BeAttack != null)
            event_BeAttack(_value, _whoDid);
        }
        else
        {
            if (event_BeCure != null)
                event_BeCure();
        }

        //触发文本
        if (!(GameMgr.instance.AttackHDList.Contains(_whoDid.characterID * 100 + this.characterID * 1)))
        {
            string _s = EventCharWord.HD_Attack(_whoDid, this);
            if (_s != null)
            {
                GameMgr.instance.PopupEvent(this.transform.position, _s, _s);
                GameMgr.instance.draftUi.AddContent(_s);
                GameMgr.instance.AttackHDList.Add(_whoDid.characterID * 100 + this.characterID * 1);
            }
        }
    }




    //外部可以增加每秒检测的委托入口
    [HideInInspector] public delegate void Event_BeAttack(float _damage, AbstractCharacter _whoDid);
    [HideInInspector] public Event_BeAttack event_BeAttack;


    /// <summary>
    /// 伤害系数。有些技能伤害减半，使用
    /// </summary>
    private float AttackAmount = 1;
    virtual public float attackAmount
    {
        get { return AttackAmount; }
        set
        {
            AttackAmount = value;
        }
    }

    /// <summary>
    /// 角色收到攻击时计算伤害
    /// </summary>
    /// <param name="_at"></param>
    /// <param name="_value">使用者的atk、psy或者直接伤害的数值</param>
    public void BeAttack(AttackType _at, float _value, bool _hasFloat, float _delayTime, AbstractCharacter _whoDid)
    {
        //计算伤害
       
        float value = 0;
        switch (_at)
        {
            case AttackType.atk: //物理
                {
                   
                    value = ((_value * GameMgr.instance.attackAmount) / (def + GameMgr.instance.attackAmount))*_whoDid.attackAmount;
                    
                }
                break;
            case AttackType.psy: //精神
                {
                    value = ((_value * GameMgr.instance.attackAmount) / (san + GameMgr.instance.attackAmount))*_whoDid.attackAmount;
                } break;
            case AttackType.dir: //真实
                {
                    value =( _value)*_whoDid.attackAmount;
                } break;
        }
        //优先随从受击
        if (servants.Count > 0)
        {
            servants[0].GetComponent<AbstractCharacter>().hp -= value;

        }
        //本身受击
        else
        {
            if (_delayTime == 0)
            {//如果没有延时
                hp -= value;
                if (_hasFloat)
                {
                    if (_at == AttackType.atk) CreateFloatWord(value, FloatWordColor.physics, true);
                    if (_at == AttackType.psy) CreateFloatWord(value, FloatWordColor.psychic, true);
                    if (_at == AttackType.dir) CreateFloatWord(value, FloatWordColor.physics, true);
                }
                //执行外部委托
                if (event_BeAttack != null)
                    event_BeAttack(value, _whoDid);

                //触发文本
                if (!(GameMgr.instance.AttackHDList.Contains(_whoDid.characterID * 100 + this.characterID * 1)))
                {
                    string _s = EventCharWord.HD_Attack(_whoDid, this);
                    if (_s != null)
                    {
                        GameMgr.instance.PopupEvent(this.transform.position, _s, _s);
                        GameMgr.instance.draftUi.AddContent(_s);
                        GameMgr.instance.AttackHDList.Add(_whoDid.characterID * 100 + this.characterID * 1);
                    }
                }
                    
                    
            }
            else
            {//如果延时，则携程
                StartCoroutine(DelayAttack(_delayTime, value, _at, _hasFloat, _whoDid));
            }
        }

    }




    //外部可以增加每秒检测的委托入口
    [HideInInspector] public delegate void Event_BeCure();
    [HideInInspector] public Event_BeCure event_BeCure;


    /// <summary>
    /// 角色收到攻击时计算伤害
    /// </summary>
    /// <param name="_at"></param>
    /// <param name="_value">使用者的atk、psy或者直接伤害的数值</param>
    public void BeCure(float _value, bool _hasFloat, float _delayTime,AbstractCharacter _whoDid)
    {
        //无延时
        if (_delayTime == 0)
        {
            if (_hasFloat)
            {
             
                CreateFloatWord(_value, FloatWordColor.heal, true);
            } 
            hp += _value;
            //触发文本
            if (!(GameMgr.instance.CureHDList.Contains(_whoDid.characterID * 100 + this.characterID * 1)))
            {
                string _s = EventCharWord.HD_Cure(_whoDid, this);
                if (_s != null)
                {
                    GameMgr.instance.PopupEvent(this.transform.position, _s, _s);
                    GameMgr.instance.draftUi.AddContent(_s);
                    GameMgr.instance.CureHDList.Add(_whoDid.characterID * 100 + this.characterID * 1);
                }
            }
        }
        else
        {
            //如果延时，则携程
            StartCoroutine(DelayAttack(_delayTime, -_value, AttackType.heal, _hasFloat, _whoDid));
        }

        if (event_BeCure != null) event_BeCure();
    }


    /// <summary>
    /// 检测上下限；修改血条数值
    /// </summary>
    void HPSetting()
    {
        if (HP < 0)
            HP = 0;
        if (HP > MaxHp * MaxHpMul)
            HP = MaxHp * MaxHpMul;
        if (hpSlider != null)
            hpSlider.value = HP / (MaxHp * MaxHpMul);
    }



    /// <summary>
    /// boss用。返回hpsplder
    /// </summary>
    /// <returns></returns>
    public Slider GetHpSlider()
    {
        return hpSlider;
    }

    /// <summary>
    /// boss用。skill物体们
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetSkillText()
    {
        return new GameObject[3] { skillText[0].transform.parent.gameObject, skillText[1].transform.parent.gameObject, skillText[2].transform.parent.gameObject };
    }
    #endregion

    #region 恢复

    //固定数值形式的恢复
    private float CURE = 0;
    virtual public float cure
    {
        get { return CURE; }
        set
        {

            CURE = value;
            if (CURE < 0) CURE = 0;
        }
    }
    //生命比值形式的恢复
    private float CUREHpRate = 0;
    virtual public float cureHpRate
    {
        get { return CUREHpRate; }
        set
        {

            CUREHpRate = value;
            if (CUREHpRate < 0) CUREHpRate = 0;
        }
    }
    float cureTime;
    bool cureOpen;
    void CureHp()
    {

        if (cure != 0)
        {
            BeCure(cure, true, 0,this);
        }
        if (cureHpRate != 0)
        {
             BeCure(maxHp* cureHpRate, true, 0,this);
        }

    }



    #endregion

    #region 基础四维数值


    /// <summary>攻击力</summary>
    protected float ATK = 0;
    virtual public float atk
    {
        get { return ATK; }
        set
        {
            ATK = value;
            if (ATK* ATKmul >= 100) 
                GrowText(GrowType.atk);
            if (ATK <= 0)
                ATK = 0;
            CaculateValue();
        }
    }

    /// <summary>攻击力的独立乘区 </summary>
    protected float ATKmul = 1;
    virtual public float atkMul
    {
        get { return ATKmul; }
        set
        {
            ATKmul = value;
            if (ATK * ATKmul >= 100)
                GrowText(GrowType.atk);
            if (ATKmul < 0) ATKmul = 0;
            CaculateValue();
        }
    }



    /// <summary>防御力</summary>
    protected float DEF = 0;
    virtual public float def
    {
        get { return DEF; }
        set
        {
            DEF = value; 
            if (DEF* DEFmul >= 100)
                GrowText(GrowType.def);
            if (DEF < 0 /*-19*/)
                DEF = 0;
            CaculateValue();
        }
    }

    /// <summary>防御力的独立乘区 </summary>
    protected float DEFmul = 1;
    virtual public float defMul
    {
        get { return DEFmul; }
        set
        {
            DEFmul = value; 
            if (DEF * DEFmul >= 100)
                GrowText(GrowType.def);
            if (DEFmul < 0) 
                DEFmul = 0;
            CaculateValue();
        }
    }



    /// <summary>精神力</summary>
    protected float PSY = 0;
    virtual public float psy
    {
        get { return PSY; }
        set
        {
            PSY = value;
            if (PSY * PSYmul >= 100)
                GrowText(GrowType.psy);
            if (PSY <= 0)
                PSY = 0;
            CaculateValue();
        }
    }

    /// <summary>精神力的独立乘区 </summary>
    protected float PSYmul = 1;
    virtual public float psyMul
    {
        get { return PSYmul; }
        set
        {
            PSYmul = value;
            if (PSY * PSYmul >= 100)
                GrowText(GrowType.psy);
            if (PSYmul <= 0)
                PSYmul = 0;
            CaculateValue();
        }
    }




    /// <summary>意志力</summary>
    protected float SAN = 0;
    virtual public float san
    {
        get { return SAN; }
        set
        {
            SAN = value;
            if (SAN * SANmul >= 100)
                GrowText(GrowType.san);
            if (SAN </* -19*/0)
                SAN = 0;
            CaculateValue();
        }
    }

    /// <summary>意志力的独立乘区 </summary>
    protected float SANmul = 1;
    virtual public float sanMul
    {
        get { return SANmul; }
        set
        {
            SANmul = value;
            if (SAN * SANmul >= 100)
                GrowText(GrowType.san);
            if (SANmul < 0)
                SANmul = 0;
            CaculateValue();
        }
    }


    /// <summary>四维之和，节省性能 </summary>
    public float allValue { get; private set; }
    public void CaculateValue() { allValue = atk * atkMul + def * defMul + psy * psyMul + san * sanMul; }




    #endregion



    #region 弃用
    /// <summary>人物暴击默认台词（弃用）</summary>
    //[HideInInspector] public string criticalSpeak;
    /// <summary>人物死亡默认台词（弃用）</summary>
    //[HideInInspector] public string deadSpeak;
    /// <summary>性别(弃用)</summary>
    [HideInInspector] public GenderEnum gender;
    /// <summary>主属性(弃用)</summary>
    public Dictionary<string, string> mainProperty = new Dictionary<string, string>();
    /// <summary>性格（弃用）</summary>
    [HideInInspector] public AbstractTrait trait;
    /// <summary>暴击几率(弃用)</summary>
    [HideInInspector] public float criticalChance = 0;
    /// <summary>暴击倍数(弃用）</summary>
    [HideInInspector] public float multipleCriticalStrike = 2;
    #endregion


    #region buff

    /// <summary>所有buff《buffID，是否有buff》</summary>
    public Dictionary<AbstractBuff, int> buffs;
    /// <summary>剩余眩晕时间</summary>
    public float dizzyTime;
    [HideInInspector] public delegate void Event_GetRelife(AbstractCharacter chara);
    [HideInInspector] public Event_GetRelife event_GetRelife;
    /// <summary>是否有复活状态(仍不可叠加，但数量为0则不可复活)</summary>
    private int relifes;
    public int reLifes
    {
        get { return relifes; }
        set
        {
            if (relifes <= value)
            {
                if (event_GetRelife != null) event_GetRelife(this);
            }
            relifes = value;
            if (relifes < 0) relifes = 0;
        }
    }

    [HideInInspector] public delegate void Event_AddBuff(AbstractBuff _buff);
    [HideInInspector] public Event_AddBuff event_AddBuff;

    /// <summary>
    /// 判断该角色是否有该buff
    /// </summary>
    public bool HasBuff(AbstractBuff buffID)
    {
        if (!buffs.ContainsKey(buffID))
            return false;
        else if (buffs[buffID] <= 0)
            return false;
        else
            return true;

  
    }


    /// <summary>
    /// 加个buff
    /// </summary>
    /// <param name="buffID"></param>
    public void AddBuff(AbstractBuff buffID)
    {
        if (!buffs.ContainsKey(buffID))
            buffs.Add(buffID, 1);
        else
            buffs[buffID]++;

        if (event_AddBuff != null)
            event_AddBuff(buffID);
    }


    /// <summary>
    /// 去个buff
    /// </summary>
    /// <param name="buffID"></param>
    public void RemoveBuff(AbstractBuff buffID)
    {
        buffs[buffID]--;
    }



    /// <summary>
    /// 清除负面状态
    /// </summary>
    public void DeleteBadBuff(int _count)
    {
        var x = 0;
        var _buffs = GetComponents<AbstractBuff>();
        foreach (var _buff in _buffs)
        {
            if (x >= _count) return;
            if (_buff.isBad)
            {
                Destroy(_buff); x++;
            }
        }
    }

    /// <summary>
    /// 清除正面状态
    /// </summary>
    public void DeleteGoodBuff(int _count)
    {
        var x = 0;
        var _buffs = GetComponents<AbstractBuff>();
        foreach (var _buff in _buffs)
        {
            if (x >= _count) return;
            if (!_buff.isBad)
            {
                Destroy(_buff); x++;
            }
        }
    }



    public void AddRandomBuff(bool _isBad, int _count,int _effectTime)
    {
        if (_isBad)
        {
            for (int i = 0; i < _count; i++)
            {
                int _r = Random.Range(0, AllSkills.BadBuff.Count);
                var _b = gameObject.AddComponent(AllSkills.BadBuff[_r]);
                (_b as AbstractBuff).maxTime = _effectTime;
                AddBuff(_b as AbstractBuff);
            }
        }
        else
        {
            for (int i = 0; i < _count; i++)
            {
                int _r = Random.Range(0, AllSkills.GoodBuff.Count);
                var _b = gameObject.AddComponent(AllSkills.GoodBuff[_r]);
                (_b as AbstractBuff).maxTime = _effectTime;
                AddBuff(_b as AbstractBuff);
            }
        }
    }

    #endregion


    #region 给角色[增加]技能或随从等等


    /// <summary>拥有技能（所挂组件,自带技能/身份 在初始赋值）</summary>
    public List<AbstractVerbs> skills;

    /// <summary>拥有的随从（最多3个）</summary>
    public List<GameObject> servants;


    ///<summary>这个角色身上可以挂载的最大技能数 </summary>
    public int maxSkillsCount = 3;





    [HideInInspector] public delegate void Event_AddVerb(AbstractVerbs _verb);
    [HideInInspector] public Event_AddVerb event_AddVerb;
    /// <summary>
    /// 增加verb技能时调用
    /// </summary>
    public void AddVerb(AbstractVerbs _av)
    {
        if (maxSkillsCount == 0)
        {
            print(this.gameObject.name + "maxSkillsCount==0");
            return;
        }

        if ((skills.Count != 0) && (skills.Count >= maxSkillsCount))
        {
            //技能数超出，移除最前面的（此处先写成，如果是xx技能，则不排除。后续可以考虑别的方法）
            if (skills[0].wordName == "认知固化")
            {
                if (skills[1].wordName == "猜疑链")
                {
                    myState.character.CreateFloatWord("<s>" + skills[2].wordName + "</s>", FloatWordColor.removeWord, false);
                    Destroy(skills[2]); skills.RemoveAt(2);
                }
                else
                {
                    myState.character.CreateFloatWord("<s>" + skills[1].wordName + "</s>", FloatWordColor.removeWord, false);
                    Destroy(skills[1]); skills.RemoveAt(1);
                }

            }
            else
            {
                myState.character.CreateFloatWord("<s>" + skills[0].wordName + "</s>", FloatWordColor.removeWord, false);
                Destroy(skills[0]); skills.RemoveAt(0);
            }
        }
        skills.Add(_av);

        for (int i = 0; i < 3; i++)
        {
            skillText[i].transform.parent.gameObject.SetActive(i < skills.Count ? true : false);
        }
        //刷新short列表

        GetComponentInChildren<AfterStart>().GetNewVerbs();

        if (event_AddVerb != null)
        {
            event_AddVerb(_av);
        }
    }

    [HideInInspector] public delegate void Event_UseVerb(AbstractVerbs _buff);
    [HideInInspector] public Event_UseVerb event_UseVerb;
    public void UseVeb_Chara(AbstractVerbs _av)
    {
        if (event_UseVerb != null)
        {
            event_UseVerb(_av);
        }
    }


    ///<summary>这个角色身上可以挂载的最大随从数 </summary>
    private int maxServantsCount = 3;



    [HideInInspector] public delegate void Event_AddServant(ServantAbstract _ser);
    [HideInInspector] public Event_AddServant event_AddServant;
    /// <summary>
    /// 增加随从时调用
    /// </summary>
    public void AddServant(string _a)
    {
        if (wordName == "怀疑主义") return;



        GameObject _servant = Instantiate<GameObject>(Resources.Load<GameObject>("Servants/" + _a));
        var _sa = _servant.GetComponent<ServantAbstract>();
        //将随从的主人设置成这个
        _sa.masterNow = this;
        _sa.Camp = this.Camp;
        _sa.enabled = true;
     
        _servant.GetComponentInChildren<AI.MyState0>().enabled = true;
        //_servant.gameObject.AddComponent(typeof(AfterStart));

        //给随从增加一个目标，使其立刻进入攻击状态
        _servant.GetComponentInChildren<AI.MyState0>().aim = this.myState.aim;
        _sa.situation = this.situation;

        //移除最先得的随从
        if ((servants.Count >= maxServantsCount))
        {
            print(this.gameObject.name + "随从数超出，移除" + servants[0].name);
            //技能数超出，移除最前面的（此处可能有问题）
            var _s = servants[0];
            servants.RemoveAt(0);
            Destroy(_s.gameObject);
        }

        if(servants.Count>0)
        {
            if (servants[0].GetComponent<CS_HunYangLong>())
            {
                print(this.gameObject.name + "随从数超出，移除" + servants[0].name);
                //技能数超出，移除最前面的（此处可能有问题）
                servants.RemoveAt(0);
            }
        }



        servants.Add(_servant);

  

        //生成随从
        //if (Resources.Load<GameObject>("Servants/" + _av.GetType().Name) == null) print("Resources.Load<GameObject>(Servants /  +_av.GetType().Name)==null");

        ServantRefresh();

        if (event_AddServant != null)
            event_AddServant(_sa);
    }


    /// <summary>
    /// 随机生成1个混养笼以外的随从
    /// </summary>
    public void AddRandomServant()
    {
        var _random = UnityEngine.Random.Range(0, 3);
        if (_random == 0)
            this.AddServant("CS_BenJieShiDui");
        if (_random == 1)
            this.AddServant("CS_DuMoGu");
        if (_random == 2)
            this.AddServant("CS_Mao");

        //为角色增加一个随从
        //var _random = UnityEngine.Random.Range(0, 7);
        //if (_random == 0)
        //    this.AddServant("CS_BenJieShiDui");
        //if (_random == 1)
        //    this.AddServant("CS_YiZhiWeiShiQi");
        //if (_random == 2)
        //    this.AddServant("CS_GongYi");
        //if (_random == 3)
        //    this.AddServant("CS_DuMoGu");
        //if (_random == 4)
        //    this.AddServant("CS_Bing");
        //if (_random == 5)
        //    this.AddServant("CS_MG42gun");
        //if (_random == 6)
        //    this.AddServant("CS_Mao");


    }


    /// <summary>
    ///当前的所有仆从合成混养笼。数量不限定。
    /// </summary>
    public void ServantMerge()
    {
        var _s = new ServantAbstract[servants.Count];
        for (int i = servants.Count - 1; i >= 0; i--)
        {
            _s[i] = servants[i].GetComponent<ServantAbstract>();
            DeleteServant(servants[i]);
        }

        if (servants.Count != 0) Debug.Log("servants.Count!=0,混养笼出错");

        AddServant("CS_HunYangLong");
        for (int i = 0; i < _s.Length; i++)
        {
            this.GetComponent<CS_HunYangLong>().SetInitNumber(_s[i]);
        }

    }
    /// <summary>
    /// 删除随从时调用
    /// </summary>
    public void DeleteServant(GameObject _a)
    {
        if (servants.Contains(_a))
            servants.Remove(_a);
        Destroy(_a);
        ServantRefresh();
    }
    void ServantRefresh()
    {
        for (int i = 0; i < servants.Count; i++)
        {
            servants[i].transform.parent = this.transform.Find("Servants").GetChild(i);
            servants[i].transform.localPosition = Vector3.zero;
        
            servants[i].transform.localScale = Vector3.one*1.5f;
            //if (this.camp==CampEnum.right)
            //{
            //    servants[i].GetComponent<ServantAbstract>().turn();
            //}
        }
    }

    #endregion


    #region adj $ noun
    [HideInInspector] public delegate void Event_AddAdj(AbstractAdjectives _ser);
    [HideInInspector] public Event_AddAdj event_AddAdj;

    public void AddAdj(AbstractAdjectives _ser)
    {
        if (event_AddAdj != null)
        {
            event_AddAdj(_ser);

        }
    }
    [HideInInspector] public delegate void Event_AddNoun (AbstractItems _ser);
    [HideInInspector] public Event_AddNoun event_AddNoun;

    public void AddNoun(AbstractItems _ser)
    {
        if (event_AddNoun != null)
        {
            event_AddNoun(_ser);

        }
    }

    #endregion

    #region 平A

    //伤害的阵营
    public bool hasBetray = false;


    //ui
    private Canvas energyCanvas;
    private Slider energySlider;
    private Text energyText;
    public delegate void energyFull(AbstractCharacter a);
    public event energyFull OnEnergyFull;
    public Text[] skillText = new Text[3];


    /// <summary>攻击间隔(检定攻击的次序，以及每两次攻击间隔时长)</summary>
    public float attackInterval = 2.2f;
    virtual public float attackSpeedPlus 
        {

        get { return AttackSpeedPlus; }
        set
        {
            AttackSpeedPlus = value;
            attackSpeedSetting();
        }
    }
    private float AttackSpeedPlus = 1;
    private float AttackSpeedTemp = -1;
    void attackSpeedSetting()
    {
        if(charaAnim!=null)charaAnim.SetSpeed(AnimEnum.attack, AttackSpeedPlus);
    }
    public void AttackSpeedPause(bool _b)
    {
        if (_b)//设置成暂停
        {
            AttackSpeedTemp = attackSpeedPlus;
            attackSpeedPlus = 0;
        }
        else//解除暂停
        {
            if(AttackSpeedTemp!=-1)
                attackSpeedPlus = AttackSpeedTemp;

        }
    }

    /// <summary>站位</summary>
    public Situation situation;
    /// <summary>攻击射程</summary>
    public int attackDistance = 99;
    /// <summary>角色动画</summary>
    public CharaAnim charaAnim;


    /// <summary>
    /// 随机目标
    /// </summary>
    /// <param name="_bool"></param>
    public void SetAimRandom(bool _bool)
    {
        myState.isAimRandom = _bool;
    }

    /// <summary>能量充能</summary>
    public float energy;

    /// <summary>平A模式</summary>
    [HideInInspector] public AbstractSkillMode attackA;

    /// <summary>能用的技能个数 </summary>
    private int _canUseSkills;
    public int canUseSkills
    {
        get
        {
            return _canUseSkills;
        }
        set
        {
            _canUseSkills = value;
            if (energyText != null)
                energyText.text = _canUseSkills.ToString();
        }
    }

    //外部可以增加每秒检测的委托入口
    [HideInInspector] public delegate void Event_AttackA();
    [HideInInspector] public Event_AttackA event_AttackA;
    /// <summary>
    /// 平A
    /// </summary>
    /// <returns>是否平A成功（影响AttackState平A冷却重置）</returns>
    virtual public bool AttackA()
    {

        if (myState.aim != null)
        {
            source.clip = aAttackAudio;
            source.Play();
            for (int i = 1; i <= AttackTimes; i++)
            {

                Invoke("AttackAOnce", CharacterManager.instance.DelayAccount(this.situation.number, myState.aim[0].situation.number));

            }
            return true;
        }
        return false;
    }

    //攻击段数
    public int AttackTimes=1;

    /// <summary>
    /// i是攻击的段数
    /// </summary>
    /// <param name="i"></param>
    private void AttackAOnce()
    {

        //攻击
        // myState.character.charaAnim.Play(AnimEnum.attack);

        if (myState.aim ==null) return;

        for (int x = 0; x < myState.aim.Count; x++)
        {
             attackA.UseMode(AttackType.atk, myState.character.atk/ AttackTimes, myState.character, myState.aim[x], true, 0);
        }
           


        //执行外部委托
        if (event_AttackA != null)
                event_AttackA();
        // myState.character.charaAnim.Play(AnimEnum.idle);
    }

    /// <summary>发出子弹 </summary>
    public virtual void CreateBullet(GameObject aimChara)
    {
        
        Vector3 _createpos= this.transform.position;
        if (this.transform.Find("BulletPos") != null)
        {
            print("bulletPos");
            _createpos = this.transform.Find("BulletPos").position;
        }
        
        DanDao danDao = GameObjectPool.instance.CreateObject(bullet.gameObject.name, bullet.gameObject, _createpos, aimChara.transform.rotation)
            .GetComponent<DanDao>();
        danDao.aim = aimChara;
        danDao.bulletSpeed = 0.5f;
        danDao.SetOff(_createpos);
    }

    Vector3[] pos =new Vector3[] 
    { new Vector3(-300, 150, 0) , new Vector3(0, 0, 0) , new Vector3(300, -150, 0), new Vector3(0, 300, 0) };
    int floatCount = 0;

    /// <summary>漂浮文字 </summary>
    public void CreateFloatWord(float value, FloatWordColor color, bool direct)
    {
        PoolMgr.GetInstance().GetObj("SecondStageLoad/floatWord", (obj) =>
         {
    
             obj.transform.localScale = Vector3.one * 20 / 8000f;
             obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
             //obj.transform.parent = energyCanvas.transform;
             obj.transform.SetParent(energyCanvas.transform);
             obj.transform.localPosition = this.transform.position + pos[(floatCount++)%4];
          
             obj.GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);

             if(color==FloatWordColor.heal) teXiao.PlayTeXiao("hpAdd");
             else if(color==FloatWordColor.healMax) teXiao.PlayTeXiao("hpmaxAdd");
             else if (color == FloatWordColor.getWord) teXiao.PlayTeXiao("getWord");
         });
        //Instantiate<GameObject>(Resources.Load("SecondStageLoad/floatWord") as GameObject, this.transform.position + pos, Quaternion.Euler(Vector3.zero), energyCanvas.transform)
        //    .GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);
    }

    /// <summary>漂浮文字 </summary>
    public void CreateFloatWord(string text, FloatWordColor color, bool direct)
    {
       
        PoolMgr.GetInstance().GetObj("SecondStageLoad/floatWord", (obj) =>
        {
 obj.transform.localScale = Vector3.one * 20 / 8000f;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //obj.transform.parent = energyCanvas.transform;
            obj.transform.SetParent(energyCanvas.transform);
            obj.transform.localPosition = this.transform.position + pos[(floatCount++) % 4];
           
            obj.GetComponent<FloatWord>().InitPopup(text, this.Camp == CampEnum.stranger, color, direct);

            if (color == FloatWordColor.heal) teXiao.PlayTeXiao("hpAdd");
            else if (color == FloatWordColor.healMax) teXiao.PlayTeXiao("hpmaxAdd");
            else if (color == FloatWordColor.getWord)teXiao.PlayTeXiao("getWord");
        });
        //Instantiate<GameObject>(Resources.Load("SecondStageLoad/floatWord") as GameObject, this.transform.position + pos, Quaternion.Euler(Vector3.zero), energyCanvas.transform)
        //    .GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);
    }
    #endregion


    virtual public void Awake()
    {
        energyCanvas = this.GetComponentInChildren<Canvas>(); if (energyCanvas == null) return;
        energyCanvas.worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        
        if (energyCanvas.worldCamera == null) print(" energyCanvas.worldCamera==null");
        energySlider = energyCanvas.transform.Find("CD").GetComponent<Slider>();

        hpSlider = energyCanvas.transform.Find("HP").GetComponent<Slider>();
       


        energyText = this.GetComponentInChildren<Text>();
        energyCanvas.gameObject.SetActive(false);

        skillText[0] = energyCanvas.transform.Find("skill1").GetComponentInChildren<Text>();
        skillText[1] = energyCanvas.transform.Find("skill2").GetComponentInChildren<Text>();
        skillText[2] = energyCanvas.transform.Find("skill3").GetComponentInChildren<Text>();
        skillText[0].transform.parent.gameObject.SetActive(false);
        skillText[1].transform.parent.gameObject.SetActive(false);
        skillText[2].transform.parent.gameObject.SetActive(false);


        myState = GetComponentInChildren<MyState0>();
        myState.character = this;
        myState.enabled = false;

        attackA = gameObject.AddComponent<DamageMode>();//平A是伤害类型
        attackA.attackRange = new SingleSelector();

        teXiao = GetComponentInChildren<TeXiao>();
        source = this.GetComponent<AudioSource>();
        buffs = new Dictionary<AbstractBuff, int>();
        charaAnim = GetComponentInChildren<CharaAnim>();

        if (GameObject.Find("AllCharacter") != null)
        {
            AbstractCharacter[] a = GameObject.Find("AllCharacter").GetComponentsInChildren<AbstractCharacter>();
            AbstractCharacter b = CollectionHelper.Find(a, p => p.wordName != wordName);
            AbstractBook.beforeFightText += ShowText(b);
        }

        this.enabled = false;

        cureTime = 0;
        cureOpen = true;


        if (isNaiMa)
        {
            Destroy(attackA);
            attackA = gameObject.AddComponent<CureMode>();
        }

        CharaInfoExcelItem data = null;
        for (int i = 0; (i < AllData.instance.charaInfo.items.Length) && (data == null); i++)
        {
            var _data = AllData.instance.charaInfo.items[i];
            if (_data.typeName == this.GetType().Name)
            {
                data = _data;
            }
        }
        if (data == null)
            return;

        //数值
        hp = maxHp = data.maxhp;
        atk = data.atk;
        def = data.def;
        psy = data.psy;
        san = data.san;
        wordName = data.name;
        characterID = data.charaID;
        description = data.bg;
        bookName = data.book;
        roleName = data.roleName;
        roleInfo = data.roleInfo;


        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;
    }
    
    public void Start()
    {
        
    }

    private void OnEnable()
    {
        if (energyCanvas == null) return;
        energyCanvas.gameObject.SetActive(true);
    }

  
    private void Update()
    {
        if (CharacterManager.instance.pause) 
            return;
        
        if(myState == null) 
            return;
        
        if (myState.nowState == myState.allState.Find(p => p.id == AI.StateID.dead)) 
            return;

        float deltaTime = Time.deltaTime;
        
        //角色的能量条积攒
        energy += deltaTime;
        
        if (energy > 5)//每5秒恢复一点能量
        {
            energy = 0;
            _canUseSkills = 0;
            if (OnEnergyFull != null)
                OnEnergyFull(this);
        }


        //更新CD条
        for (int x = 0; x < skills.Count; x++)
        {
            if (skills[x].needCD == 0) skillText[x].text = "猜";
            else
                skillText[x].text = ((int)(skills[x].needCD - skills[x].CD)).ToString();
            // print("needCd" + skills[x].needCD + "CD" + skills[x].CD);

        }

        //旧版本CD条
        //energySlider.value = energy;


        //【恢复】计时
        if (cureOpen)
            cureTime += deltaTime;
        if (cureTime >= 10)
        {
            cureTime = 0;
            CureHp();
        } 
    }


    /// <summary>
    /// 翻转
    /// </summary>
    public void turn()
    {
        
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //角色的canvas子物体不转向
        energyCanvas.transform.localScale = new Vector3(-energyCanvas.transform.localScale.x, energyCanvas.transform.localScale.y, energyCanvas.transform.localScale.z);
    }

    #region 文本
    /// <summary>
    /// 人物出场文本(加到AbstractBook.beforeFightText)
    /// </summary>
    abstract public string ShowText(AbstractCharacter otherChara);


    /// <summary>
    /// 暴击文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string CriticalText(AbstractCharacter otherChara);

    /// <summary>
    /// 成长文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string GrowText(GrowType type);


    /// <summary>
    /// 低血量文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string LowHPText();


    /// <summary>
    /// 死亡文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string DieText();
    #endregion
}

