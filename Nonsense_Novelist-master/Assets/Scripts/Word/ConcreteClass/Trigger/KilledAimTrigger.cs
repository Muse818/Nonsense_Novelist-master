using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 击杀掉目标
    /// </summary>
    class KilledAimTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.KilledAim;
        }
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.aim!=null && myState.aim[0].hp <= 0)
            {
                myState.FindAim();
                return true;
            }
            else
                return false;
        }
    }
}
