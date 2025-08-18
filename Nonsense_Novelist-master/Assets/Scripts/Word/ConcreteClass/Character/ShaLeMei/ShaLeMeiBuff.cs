using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaLeMeiBuff : MonoBehaviour
{
    float value;
    AbstractCharacter aim;
    /// <summary>µ¹¼ÆÊ± </summary>
    public float nowTime;
    private void Awake()
    {
        aim = GetComponent<AbstractCharacter>();
        value = aim.san * 0.2f;
        aim.san -= value;
        nowTime = 10;
    }

    private void Update()
    {
        nowTime-=Time.deltaTime;
        if (nowTime < 0)
        {
            aim.san += value;
            Destroy(this);
        }
    }
}
