using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ¿ªÊ¼¹¥»÷
    /// </summary>
    class IdleToAttackTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.IdleToAttack;
        }
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.character.hp <= 0) return false;
            if (CharacterManager.instance.pause)return false;

            if (myState.aim != null)
            {
                return true;
            }
        
            return false;
        }
    }
}
