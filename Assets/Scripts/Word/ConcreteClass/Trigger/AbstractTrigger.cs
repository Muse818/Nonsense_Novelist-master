using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 抽象触发器
    /// </summary>
    public abstract class AbstractTrigger : MonoBehaviour
    {
      public TriggerID id;

     virtual public void Awake()
        {

        }
        /// <summary>
        /// 是否满足条件
        /// </summary>
      public abstract bool Satisfy(MyState0 myState);
    }

}
