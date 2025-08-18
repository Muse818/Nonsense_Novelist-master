
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono公共模块的功能模块
/// 实现向外部提供不继承Mono类的监听函数
/// 并继承Mono方便向外提供部协程接口函数
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;

    void Update()
    {
        if (updateEvent != null) 
        {
            //执行事件
            updateEvent(); 
        }
    }

    /// <summary>
    /// 提供向外部用于添加帧更新事件
    /// </summary>
    /// <param name="fun">需要帧更新的逻辑函数</param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 提供向外部用于移除帧更新事件
    /// </summary>
    /// <param name="fun">需要帧更新的逻辑函数</param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

}
