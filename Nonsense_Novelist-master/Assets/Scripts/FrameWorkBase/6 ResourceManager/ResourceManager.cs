using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块 [基于Resources]
/// </summary>
public class ResourceManager : SingletonBase<ResourceManager>
{
    /// <summary>
    /// 同步加载资源
    /// 当类型为GameObject类型则实例化并返回
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源名</param>
    /// <returns></returns>
    public T LoadResource<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源名</param>
    /// <param name="collback">回调函数,参数为加载的资源,当类型为GameObject类型则实例化并作为参数</param>
    public void LoadResourceAsync<T>(string name,UnityAction<T> collback) where T : Object
    {
        MonoManager.GetInstance().StartCoroutine(LoadReourceByCoroutine(name, collback));
    }

    /// <summary>
    /// 异步加载资源的协程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="collback"></param>
    /// <returns></returns>
     private IEnumerator LoadReourceByCoroutine<T>(string name, UnityAction<T> collback) where T : Object
    {
        ResourceRequest resReq = Resources.LoadAsync<T>(name);
        yield return resReq;
        if (resReq.asset is GameObject)
            collback(GameObject.Instantiate(resReq.asset) as T);
        else
            collback(resReq.asset as T);
    }
}
