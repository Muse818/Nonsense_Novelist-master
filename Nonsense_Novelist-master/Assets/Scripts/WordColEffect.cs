using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordColEffect : MonoBehaviour
{
    public void DestroySelf()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
}
