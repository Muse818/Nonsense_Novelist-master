using System;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// 空接口使得,写法简单
/// </summary>
interface IEventInfo{   }

/// <summary>
/// 用于存储事件监听
/// 包裹事件存储回调所用得委托容器
/// 使得避免装箱拆箱
/// </summary>
/// <typeparam name="T">参数类型</typeparam>
public class EventInfo<T>: IEventInfo
{
    public UnityAction<T> actionos;
    public EventInfo(UnityAction<T> action)
    {
        actionos += action;
    }
}

/// <summary>
/// 没有参数的委托信息
/// </summary>
public class EventInfo: IEventInfo
{
    public UnityAction actionos;
    public EventInfo(UnityAction action)
    {
        actionos += action;
    }
}

/// <summary>
/// 事件中心
/// 单例对象
/// 观察者模式
/// </summary>
public class EventCenter : SingletonBase<EventCenter>
{
    /// <summary>
    /// 事件存储容器 <事件唯一标识(/事件名称),处理事件的委托函数>
    /// 委托可以存储多个函数,处理时间连锁反应
    /// UnityAction<object> 含一个参数的委托,事件发生便于传入其他参数
    /// </summary>
    Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听(有一个参数)
    /// 注意事件标识不能重复
    /// 建议在Start一类的初始化函数中调用
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="action">所要处理的时间</param>
    public void AddEventListener<T>(string name , UnityAction<T> action)
    {

        //当没有此事件时,创建一个
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
        //有则添加一个
        else
        {
            //转换成事件信息类添加回调
            (eventDic[name] as EventInfo<T>).actionos += action;
        }
    }

    /// <summary>
    /// 添加事件监听(没有参数)
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="action">事件回调</param>
    public void AddEventListener(string name, UnityAction action)
    {
        //当没有此事件时
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo(action));
        }
        //有则添加一个
        else
        {
            //转换成事件信息类添加回调
            (eventDic[name] as EventInfo).actionos += action;
        }
    }

    /// <summary>
    /// 事件触发(一个参数)
    /// </summary>
    /// <param name="name">事件名称</param>
    public void EventTrigger<T>(string name,T var)
    {
        if (eventDic.ContainsKey(name))
        {
            //从字典中取出标识对应的回调,不为空则执行
            (eventDic[name] as EventInfo<T>).actionos?.Invoke(var);
        }
    }

    /// <summary>
    /// 事件触发(没有参数)
    /// </summary>
    /// <param name="name">事件名称</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            //从字典中取出标识对应的回调,不为空则执行
            (eventDic[name] as EventInfo).actionos?.Invoke();
        }
    }

    /// <summary>
    /// 移除事件的有一个参数的监听函数
    /// 建议在OnDestory中调用
    /// </summary>
    /// <param name="name">事件标识</param>
    /// <param name="action">已经添加的监听函数</param>
    public void RemoveEventListener<T>(string name,UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actionos -= action;
        }
    }

    /// <summary>
    /// 移除事件的没有参数的监听函数
    /// </summary>
    /// <param name="name">事件标识</param>
    /// <param name="action">已经添加的监听函数</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actionos -= action;
        }
    }

    /// <summary>
    /// 清空所有事件
    /// 场景切换时清空所有事件监听
    /// </summary>
    public void ClearEvent()
    {
        eventDic.Clear();
    }
    
}
