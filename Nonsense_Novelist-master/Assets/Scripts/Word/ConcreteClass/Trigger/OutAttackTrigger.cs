using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// Àë¿ª¹¥»÷·¶Î§
    /// </summary>
    class OutAttackTrigger : AbstractTrigger
    {

        override public void Awake()
        {
            base.Awake();
            id = TriggerID.OutAttack;
        }
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.character.hp <= 0) return false;
            if (CharacterManager.instance.pause)
            {
                return true;
            }


            if (myState.aim[0] == null ||
                 Situation.Distance(myState.character.situation, myState.aim[0].situation) > myState.character.attackDistance)
            {
                myState.aim = null;
                return true;
            }
            else 
                return false;
        }
    }
}
