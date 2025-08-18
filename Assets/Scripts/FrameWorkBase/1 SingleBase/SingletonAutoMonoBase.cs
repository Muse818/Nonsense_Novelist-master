using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**说明
 * 因为单例类一般为管理类,切换场景时不能失去作用，此类中已经做了处理
 * 因为脚本的挂载总是手动或者代码添加
 * 此类在使用了代码添加脚本的方式,并在场景创建一个与脚本名相同的对象作为管理对象
 * 使用时只需要简单的继承即可
 * 由于逻辑代码实现,则自动保证了唯一性
 */

/// <summary>
/// 自动的Mono单例基类
/// 当单例存在则会自动保证唯一性
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonAutoMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    
    /// <summary>
    /// 当单例为空时,则创建游戏对象,否则不创建，保证单例
    /// </summary>
    /// <returns>返回单例对象</returns>
    public static T GetInstance()
    {
        if (instance == null)
        {
            //在场景创建空物体对象并挂载脚本
            GameObject obj = new GameObject();
            //设置对象名字为反射名
            obj.name = typeof(T).ToString();
            //过场景不会移除
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}
