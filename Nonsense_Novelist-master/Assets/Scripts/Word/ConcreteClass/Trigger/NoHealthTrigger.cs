using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 生命值为0
    /// </summary>
    class NoHealthTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.NoHealth;
        }
        //public AbstractState aaa;

        [HideInInspector] public delegate void Event_Relife(AbstractCharacter ac);
        [HideInInspector] public Event_Relife event_relife;
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.character.hp <= 0)
            {
                if (myState.character.reLifes > 0)//复活
                {
                   // print("1");//print(.ToString());
                    //执行外部的复活效果
                    if (event_relife!=null)
                    { event_relife(myState.character);  }

                    //血量回满
                    myState.character.hp = myState.character.maxHp;

                    //删除relife效果
                    if (myState.character.GetComponent<ReLife>() != null)
                    {
                        Destroy(myState.character.GetComponent<ReLife>());
                    }

                    myState.character.teXiao.PlayTeXiao("relife");
                    return false;
                }
                else//死亡
                {
                   //BGM 

                      return true;
                }
                  
            }
            else//hp>0
                return false;
        }


    }
}
