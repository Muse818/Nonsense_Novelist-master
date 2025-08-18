using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发射器
/// </summary>
public class RollControler : MonoBehaviour
{

    // Update is called once per frame
    public float leftAngle = 60;
    public float rightAngle = 300;
    void Update()
    {
        if (Time.timeScale == 0f)
            return;

            //计算角度
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 dic1 = Vector2.down;
            Vector2 dic2 = transform.position - clickPos;
            //这一段会让鼠标在发射器下方时，映射到上方
            //var y = dic2.y;
            //dic2.y = -Mathf.Abs(y);
            Vector3 v3 = Vector3.Cross(dic1, dic2);

            float angle = 0;
        if (v3.z > 0)
        {
            angle = Vector3.Angle(dic1, dic2); transform.eulerAngles = new Vector3(0, 0, /*angle*/ Mathf.Clamp (angle,0, leftAngle));
        }
        else
        {
            angle = 360 - Vector3.Angle(dic1, dic2); transform.eulerAngles = new Vector3(0, 0, /*angle*/ Mathf.Clamp(angle, rightAngle, 360));
        }
                

       
    }

}