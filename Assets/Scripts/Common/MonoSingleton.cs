/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;


/// <summary>
/// 单例模式的抽象类(要保证能继承，不能私有构造函数）【P488 03-09 4:00，详解15:00】
/// </summary>
/// <typeparam name="T">此类继承MonoBehavior，子类约束为MonoSingleton类型</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    //父类已定义静态私有字段，子类不用写
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            //和游戏场景对应
            if (m_Instance == null)
            {//3.设计阶段，如果脚本没挂在物体上（Awake得到null），（为了脚本单例模式）从场景中找唯一实例
                m_Instance = FindObjectOfType(typeof(T)) as T;
                if (m_Instance == null)
                {//4.场景中也找不到，最终new一个叫“Singleton of...”的对象，和挂在其上的 脚本唯一实例
                    m_Instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    m_Instance.Init();
                
                
                }

            }
            return m_Instance;
        }
    }
    //1.unity项目特点：写脚本挂在物体上，项目运行时，系统帮我们把脚本类实例化了
    /// <summary>
    /// 2.程序运行时（Awake），从场景中找到 脚本的唯一实例，记录在m_instance中）
    /// </summary>
    virtual public void Awake()
    {

        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else
        {
            if(m_Instance!=this)
            { Destroy(this); }
        }
       // DontDestroyOnLoad(m_Instance.gameObject);
    }

    public virtual void Init() {  }
    //应用程序退出，清除对象

    virtual public void OnApplicationQuit()
    {
        m_Instance = null;
    }
}
