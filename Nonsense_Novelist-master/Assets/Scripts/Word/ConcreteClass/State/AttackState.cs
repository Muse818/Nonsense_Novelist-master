using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    class AttackState :AbstractState
    {
        /// <summary>是否是物理伤害</summary>
        public bool isPhysics=true;

        /// <summary>平A计时器（累加）</summary>
        [HideInInspector] public float attackAtime;
        private List<AbstractVerbs> canUseSkills=new List<AbstractVerbs>();
        private AbstractVerbs nowSkill;

        override public void Awake()
        {
            base.Awake();
            canUseSkills.Clear();
            id = StateID.attack;
            triggers.Add(gameObject.AddComponent<OutAttackTrigger>());  
            map.Add(TriggerID.OutAttack,StateID.idle);
            triggers.Add(gameObject.AddComponent<KilledAimTrigger>());
            map.Add(TriggerID.KilledAim, StateID.idle);
        }
        [HideInInspector] public delegate void Event_UseVerb(AbstractVerbs _verb);
        [HideInInspector] public Event_UseVerb event_UseVerb;
        private float count;//防止bug用，最大限度的不执行时间
        public override void Action(MyState0 myState)
        {
            //平A间隔累计
            attackAtime += Time.deltaTime;

            //如果能量已满，则加入使用排队列表
            foreach (AbstractVerbs skill in myState.character.skills)
            {
                //如果能量已满&&有目标
                if (skill.CalculateCD() && skill.skillMode.CalculateAgain(skill.attackDistance, myState.character) != null)
                {
                    skill.CDZero();
                    canUseSkills.Add(skill);
                }
            }

            //如果现在正在使用技能
            if (nowSkill != null)
            {
                count += Time.deltaTime;
                //则检测技能是否有使用完毕
                if (((!(nowSkill.isUsing)) && count > 0.04f) || (count > 2.2f))
                {
                    canUseSkills.RemoveAt(0);
                    nowSkill = null;
                }
            }
            //如果现在没在使用技能
            else if ((canUseSkills.Count > 0))
            {
                //使用一个排队中的技能
                nowSkill = canUseSkills[0];
                count = 0;
                canUseSkills[0].UseVerb(myState.character);
                if (event_UseVerb != null)
                {
                    event_UseVerb(canUseSkills[0]);
                }
         
            }
            //如果没有技能在使用&&平A冷却完毕
            //else if (attackAtime >= (myState.character.attackInterval/myState.character.attackSpeedPlus))
            //{
             
            //    if (/*myState.character.AttackA()*/myState.hasAttackAAnim)
            //    {
            //        attackAtime = 0;
            //        //myState.hasAttackAAnim = false;
            //      // myState.character.charaAnim.Play(AnimEnum.attack);

            //    }
            //    else
            //    {
                  
                    
            //    }
            //}
            else
            {
               
               // myState.character.charaAnim.Play(AnimEnum.idle);
            }
        }

        /// <summary>
        /// 是否能平A（没有技能在使用）
        /// </summary>
        /// <returns></returns>
        private bool canA(MyState0 myState)
        {
            foreach (AbstractVerbs skill in myState.character.skills)
            {
                if (skill.isUsing)
                {  
                    print("skill.isUsing"+skill.wordName);return false;
                }
                 
                  
            }
            return true;
        }


        public override void EnterState(MyState0 myState)
        {
            myState.character.charaAnim.Play(AnimEnum.attack);
            //myState.character.charaAnim.Play(AnimEnum.attack);
        }

        public override void Exit(MyState0 myState)
        {
        }
    }

}