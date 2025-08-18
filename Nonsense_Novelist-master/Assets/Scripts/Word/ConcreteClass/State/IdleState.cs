using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ´ý»ú×´Ì¬
    /// </summary>
    class IdleState :AbstractState
    {

        override public void Awake()
        {
            base.Awake();
            id = StateID.idle;
            triggers.Add(gameObject.AddComponent<IdleToAttackTrigger>());
            map.Add(TriggerID.IdleToAttack,StateID.attack);
        }
        public override void Action(MyState0 myState)
        {
            
        }


        public override void EnterState(MyState0 myState)
        {
           myState.character.charaAnim.Play(AnimEnum.idle);
        }

        public override void Exit(MyState0 myState)
        {
            
        }
    }

}