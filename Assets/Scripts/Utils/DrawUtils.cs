using UnityEngine;
using UnityEngine.UIElements;
public class DrawUtils
{
    /// <summary>
    /// 绘制旋转正方形的调试图形
    /// </summary>
    /// <param name="center">正方形中心点（世界坐标）</param>
    /// <param name="size">正方形边长</param>
    /// <param name="angleDegrees">旋转角度（度数）</param>
    /// <param name="color">线条颜色</param>
    /// <param name="duration">显示持续时间（秒）</param>
    public static void DrawRotatedSquare(
        Vector3 center,
        float size,
        float angleDegrees,
        Color color,
        float duration = 0)
    {
        float halfSize = size / 2f;
        Vector3[] localPoints = new Vector3[4]
        {
            new Vector3( halfSize,  halfSize, 0), 
            new Vector3(-halfSize,  halfSize, 0), 
            new Vector3(-halfSize, -halfSize, 0), 
            new Vector3( halfSize, -halfSize, 0)  
        };
        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        Vector3[] worldPoints = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Vector3 original = localPoints[i];
            float rotatedX = original.x * cos - original.y * sin;
            float rotatedY = original.x * sin + original.y * cos;
            worldPoints[i] = center + new Vector3(rotatedX, rotatedY, 0);
        }
        for (int i = 0; i < worldPoints.Length; i++)
        {
            int nextIndex = (i + 1) % worldPoints.Length;
            Debug.DrawLine(worldPoints[i], worldPoints[nextIndex], color);
        }
    }
    /// <summary>
    /// 绘制多边形的调试图形
    /// </summary>
    /// <param name="vertexs">顶点</param>
    /// <param name="color">线条颜色</param>
    public static void DrawPolygon(
        Vector3[] vertexs,
        Color color,
        float duration = 0)
    {
        for (int i = 0; i < vertexs.Length; i++)
        {
            int nextIndex = (i + 1) % vertexs.Length;
            Debug.DrawLine(vertexs[i], vertexs[nextIndex], color);
        }
    }
}