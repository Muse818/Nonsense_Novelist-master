using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 曲线预测器
/// </summary>
public class Track : MonoBehaviour
{
    [SerializeField]private int dotNum = 40;
    public GameObject dotsParent;
    public GameObject dotPrefab;
    public float dotSpacing = 0.01f;
    [Range(0.01f, 0.3f)] public float dotMinScale = 0.1f;
    [Range(0.3f, 1f)] public float dotMaxScale = 1f;

    private Transform[] dotList;
    private Vector2 pos;
    private float timeStamp;

    private void Start()
    {
        Hide();
        PrepareDots();
    }
    /// <summary>
    /// 准备轨迹点
    /// </summary>
    private void PrepareDots()
    {
        dotList = new Transform[dotNum];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotNum;

        for(int i = 0; i < dotNum; ++i)
        {
            var dot = Instantiate(dotPrefab).transform;
            dot.parent = dotsParent.transform;
            dot.localScale = 0.2f*Vector3.one * scale;
            dot.position = Vector3.zero;
            if (scale > dotMinScale)
                scale -= scaleFactor;
            dotList[i] = dot;
        }
    }
    bool istrue=false;
    /// <summary>
    /// 更新点的位置
    /// </summary>
    /// <param name="startPos">起始点</param>
    /// <param name="pushSpeed">发射方向</param>
    /// <param name="secondPos">接触点</param>
    /// <param name="reflect">反射方向</param>
    /*public void UpDateDots0(Vector3 startPos, Vector2 shoot, Vector3 secondPos, Vector2 reflect)
    {
        timeStamp = dotSpacing;
        
        for (int i = 0; i < dotNum; ++i)
        {
            if ((dotList[i].position.x<0&& dotList[i].position.x>Shoot.pointt.x&& dotList[i].position.y < Shoot.pointt.y+ shoot.y * timeStamp) ||(dotList[i].position.x > 0 && dotList[i].position.x < Shoot.pointt.x && dotList[i].position.y < Shoot.pointt.y+ reflect.y * timeStamp))//发射
            {
                pos.x = startPos.x + shoot.x * timeStamp;
                pos.y = (startPos.y + shoot.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
                dotList[i].position = pos;
                timeStamp += dotSpacing;
            }
            else//反射
            {
                if (istrue==false)
                {
                    timeStamp = dotSpacing;
                    istrue = true;
                }
                pos.x = secondPos.x + reflect.x * timeStamp;
                pos.y = (secondPos.y + reflect.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
                dotList[i].position = pos;
                timeStamp += dotSpacing;
            }

        }
    }*/
    public void UpDateDots0(Vector3 startPos, Vector2 shoot, Vector3 secondPos, Vector2 reflect)
    {
        timeStamp = dotSpacing;
        bool isReflecting = false;
        Vector2 lastPos = startPos;

        for (int i = 0; i < dotNum; ++i)
        {
            if (!isReflecting)
            {
                pos.x = startPos.x + shoot.x * timeStamp;
                pos.y = (startPos.y + shoot.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;

                // 使用点的移动方向而不是固定坐标来判断反射
                if ((shoot.x > 0 && pos.x > secondPos.x) || (shoot.x < 0 && pos.x < secondPos.x))
                {
                    isReflecting = true;
                    startPos = pos; // 更新反射起点
                    timeStamp = 0; // 重置时间戳
                }
            }
            else
            {
                pos.x = startPos.x + reflect.x * timeStamp;
                pos.y = (startPos.y + reflect.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
            }

            dotList[i].position = pos;
            timeStamp += dotSpacing;
            lastPos = pos; // 更新最后一个位置，用于下一次循环
        }
    }
    public static float Dott(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }
    /// <summary>
    /// 反弹方向
    /// </summary>
    /// <param name="inDirection">入射方向</param>
    /// <param name="inNormal">法线</param>
    /// <returns></returns>
    public static Vector3 Reflectt(Vector3 inDirection, Vector3 inNormal)
    {
        float num = -2f * Dott(inNormal, inDirection);
        return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
    }
    /// <summary>
    /// 显示预测轨迹
    /// </summary>
    public void Show()
    {
        dotsParent.SetActive(true);
    }
    /// <summary>
    /// 隐藏预测轨迹
    /// </summary>
    public void Hide()
    {
        dotsParent.SetActive(false);
    }

}
