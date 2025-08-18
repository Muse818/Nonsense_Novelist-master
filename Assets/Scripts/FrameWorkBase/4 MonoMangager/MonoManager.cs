using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 公共Mono模块管理模块
/// 通过单例封装MonoController类:实现帧更新添加功能
/// 向外部提供添加帧更新函数的方法
/// 向外部提供添加协程的方法
/// </summary>
public class MonoManager : SingletonBase<MonoManager>
{
    public MonoController controller;

    //单例保证了MonoController的唯一性
    //第一次构造时在场景上制造一个具有MonoController脚本的对象
    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    /// <summary>
    /// 向外部提供用于添加帧更新事件
    /// </summary>
    /// <param name="fun">需要帧更新的逻辑</param>
    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    /// <summary>
    /// 提供向外部用于移除帧更新事件
    /// </summary>
    /// <param name="fun">需要帧更新的逻辑</param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    /// <summary>
    /// 开启协程的方法
    /// </summary>
    /// <returns>协程函数开启对象</returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    /// <summary>
    /// 停止协程的方法
    /// </summary>
    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }
    public void StopCoroutine(string methodName)
    {
        controller.StopCoroutine(methodName);
    }

}
