using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 滑动条显示加载进度（挂滑动条上）
/// </summary>
 [HideInInspector]
public class LoadingSlider : MonoBehaviour
{
    private Slider loadingSlider;
    public float realValue;
    public float showValue;
    void Start()
    {
        loadingSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showValue < 0.9f)
        {
            if(showValue < realValue )//产生进度条匀速假象(你被骗辣）
            {
                showValue += 0.0065f;
                loadingSlider.value  = showValue;
            }
            
        }
    }
}
