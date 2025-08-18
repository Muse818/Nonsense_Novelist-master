using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ±ª‘Œ
    /// </summary>
    class BeDizzyTrigger : AbstractTrigger
    {

        override public void Awake()
        {
            base.Awake();
            id = TriggerID.BeDizzy;
        }
        public override bool Satisfy(MyState0 myState)
        {
            return myState.character.dizzyTime>0;
        }
    }
}
