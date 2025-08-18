using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 自己状态（抽象角色脚本挂上时，此脚本自动跟着挂上）
    /// </summary>
    public class MyState0 : MonoBehaviour
    {
        /// <summary>角色</summary>
        [HideInInspector] public AbstractCharacter character;
        /// <summary>拥有的所有状态</summary>
        public List<AbstractState> allState = new List<AbstractState>();
        /// <summary>当前状态</summary>
        public AbstractState nowState;
        /// <summary>默认状态</summary>
        [HideInInspector] public AbstractState defaultState;


        /// <summary>关注的目标</summary>
        public List<AbstractCharacter> aim=new List<AbstractCharacter>();
        /// <summary>关注的目标</summary>
        public List<AbstractCharacter> aimTemp = new List<AbstractCharacter>();
        /// <summary>关注的目标最大数量</summary>
        [HideInInspector] public int aimCount = 1;


        /// <summary>移速</summary>
        public float speed = 0.1f;

        public bool isAimRandom=false;

        //外部可以增加每秒检测的委托入口
        [HideInInspector] public  delegate void Event_EveryZeroOne();
        [HideInInspector] public Event_EveryZeroOne event_EveryZeroOne;

        public void Awake()
        {
            allState.Add(gameObject.AddComponent<IdleState>());
            allState.Add(gameObject.AddComponent<AttackState>());
            allState.Add(gameObject.AddComponent<DeadState>());
            allState.Add(gameObject.AddComponent<DizzyState>());
        }
        public void Start()
        {
            //character = this.GetComponent<AbstractCharacter>();//在角色那边已写
            nowState = defaultState = allState.Find(p => p.id == StateID.idle);
            nowState.EnterState(this);
            if (aim == null)
            {
                aim = new List<AbstractCharacter>();
            }

            StartCoroutine(Every1Seconds());
            StartCoroutine(EveryZeroOne());
        }

        public void FixedUpdate()
        {
            nowState.Action(this);
        }

        #region 角色攻击动画关键帧调用
        public void BulletOut()
        {

            
        }
        //public bool hasAttackAAnim = false;
        public void AttackA()
        {

            character.AttackA();
            
            for (int x = 0; x < aim.Count; x++)
            {
                character.CreateBullet(aim[x].gameObject);
            }

                
        }
        #endregion


        IEnumerator EveryZeroOne()
        {
            while (true)
            {
                nowState.CheckTrigger(this);//更新状态
                yield return new WaitForSeconds(0.1f);
                if(event_EveryZeroOne!=null)
                    event_EveryZeroOne();
            }

        }

        ServantAbstract sa=null;
        AI.MyState0 masterState = null;
        IEnumerator Every1Seconds()
        {
            sa = transform.parent.GetComponentInChildren<ServantAbstract>();
            if(sa!=null) masterState= sa.masterNow.GetComponentInChildren<MyState0>();

            while (true)
            {
                if (!CharacterManager.instance.pause)
                {
                    if (aim == null)
                    {
                        nowState.Action(this);
                        if (aim == null)
                        {
                            print(character.wordName);
                            aim = new List<AbstractCharacter>();
                        }
                        else
                        {
                            aim.Clear();
                        }
                       
                    } 

                    else 
                    {  
                        aim.Clear();
                    }
                 
                    aim .AddRange( FindAim());//不断寻找更近的敌人
                }
                 
                yield return new WaitForSeconds(1);
            }
        }
        /// <summary>
        /// 寻找目标
        /// </summary>
        public AbstractCharacter[] FindAim()
        {




            //如果已有不变目标：
            if (unchangeAim != null)
                return new AbstractCharacter[1] { unchangeAim };

            if (isAimRandom)
            {
                //所有目标，返回随机一个(除了BOSS以外)
                AbstractCharacter[] b = character.attackA.CalculateRandom(character.attackDistance, character, true);
                print("returnRandomAim");
                int _r = Random.Range(0, b.Length);

                //异常报备
                if (b.Length <= 1)
                {
                    print("在" + character.wordName + "的GetNewAim(Random)中，其目标小于1");
                    return new AbstractCharacter[1] { b[_r] };
                }

                //排除自己
                while (b[_r] == character)
                {
                    _r = Random.Range(0, b.Length);
                }

                return new AbstractCharacter[1] { b[_r] };
            }

            //筛选目标，返回距离最近的

            aimTemp.Clear();
            aimTemp.AddRange(character.attackA.CalculateAgain(character.attackDistance, character));

            //按照攻击对象的数目来返回指定数目的目标、
            for(int x=0;x<aimTemp.Count;x++)
            {
                
                //移除已经死亡的对象
                if ((aimTemp[x].myState.nowState == aimTemp[x].myState.allState.Find(p => p.id == AI.StateID.dead)))
                {
         
                    aimTemp.Remove(aimTemp[x]);
                }
         
            }

            if (aimCount > aimTemp.Count) return aimTemp.ToArray();
            int _vount = aimTemp.Count - aimCount;
            aimTemp.RemoveRange(aimCount, _vount);

            return aimTemp.ToArray();

           
            
        }

   

        /// <summary>
        ///刷新目标
        /// </summary>
        public AbstractCharacter GetANewAim(bool _isRandom)
        {
            if (_isRandom)
            {
                //所有目标，返回随机一个(除了BOSS以外)
                AbstractCharacter[] b = character.attackA.CalculateRandom(character.attackDistance, character, true);
                int _r = Random.Range(0, b.Length);

                //异常报备
                if (b.Length <= 1)
                {
                    print("在" + character.wordName + "的GetNewAim(Random)中，其目标小于1");
                    return b[_r];
                }

                //排除自己
                while (b[_r] == character)
                {
                     _r = Random.Range(0, b.Length);
                }
                return b[_r];
            }

            //筛选目标，返回距离最近的
            AbstractCharacter[] a = character.attackA.CalculateAgain(character.attackDistance, character);
            return a[0];
        }


        private AbstractCharacter unchangeAim;
        /// <summary>
        ///设置一个不变的目标
        /// </summary>
        public void SetUnchangeAim(AbstractCharacter _aim)
        {
            if (_aim != null)
                unchangeAim = _aim;
            else
                unchangeAim = null;
        }

        /// <summary>
        /// 状态切换
        /// </summary> 
        public void ChangeActiveState(StateID stateID)
        {
            nowState.Exit(this);
            nowState = allState.Find(p => p.id == stateID);
            nowState.EnterState(this);
        }
    }

}
