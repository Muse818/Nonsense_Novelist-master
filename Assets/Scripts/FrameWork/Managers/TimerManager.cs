using System;
using System.Collections.Generic;
using UnityEngine;

// 定义两种委托类型：无参数和有参数
public delegate void TimerHandler();
public delegate void TimerHandlerParam<T>(T param);

class TimerNode
{
    public bool Valid
    {
        get
        {
            if (mRemoved) return false;

            // 检查无参数委托
            if (mCallback != null)
            {
                // 检查委托目标是否有效
                return mCallback.Target != null;
            }

            // 检查带参数委托
            if (mCallbackWithParam != null)
            {
                // 将 object 转换为 Delegate 后检查 Target
                Delegate callback = mCallbackWithParam as Delegate;
                return callback != null && callback.Target != null;
            }

            return false;
        }
    }

    // 支持两种回调类型
    public TimerHandler mCallback;
    public object mCallbackWithParam; // 改为 object 类型

    // 回调参数
    public object mParam;

    public float mRepeatInterval; // 定时器触发的时间间隔;
    public float mDelay; // 第一次触发要隔多少时间;
    public int mRepeatTimes; // 你要触发的次数;
    public float mPassedTime; // 这个Timer过去的时间;
    public bool mRemoved; // 是否已经删除了
    public int mTimerId; // 标识这个timer的唯一Id号;
}

public class TimerManager
{

    private Dictionary<int, TimerNode> mTimers = null;//存放Timer对象

    private List<int> mRemoveTimers = null;//新增Timer缓存队列
    private List<TimerNode> mNewTimers = null;//删除Timer缓存队列
    private int mIncId = 1;//每个Timer的唯一标示

    public void Init()
    {
        mTimers = new Dictionary<int, TimerNode>();
        mNewTimers = new List<TimerNode>();
        mRemoveTimers = new List<int>();
        mIncId = 1;
    }

    // 添加无参数定时器
    public int AddTimer(TimerHandler callBack, float firstDelay,
        float repeatInterval, int repeatTimes = 0)
    {
        return CreateTimer(callBack, null, null, firstDelay, repeatInterval, repeatTimes);
    }

    // 添加带参数定时器
    public int AddTimer<T>(TimerHandlerParam<T> callBack, T param,
        float firstDelay, float repeatInterval, int repeatTimes = 0)
    {
        // 直接存储泛型委托，不进行类型转换
        return CreateTimer(null, callBack, param, firstDelay, repeatInterval, repeatTimes);
    }

    // 创建定时器（内部方法）
    private int CreateTimer(TimerHandler callback,
        object callbackWithParam, object param,
        float firstDelay, float repeatInterval, int repeatTimes)
    {
        if (callback == null && callbackWithParam == null)
        {
            Debug.LogError("CreateTimer failed: both callbacks are null!");
            return -1;
        }
        TimerNode timer = new TimerNode
        {
            mCallback = callback,
            mCallbackWithParam = callbackWithParam, // 直接存储为 object
            mParam = param,
            mRepeatTimes = repeatTimes,
            mRepeatInterval = repeatInterval,
            mDelay = firstDelay,
            mPassedTime = repeatInterval - firstDelay,
            mRemoved = false,
            mTimerId = mIncId
        };

        mIncId++;
        mNewTimers.Add(timer);
        return timer.mTimerId;
    }

    public void RemoveTimer(int timerId)
    {
        if (!mTimers.ContainsKey(timerId))
            return;

        TimerNode timer = mTimers[timerId];
        timer.mRemoved = true;
    }

    public void Update(float deltaTime)
    {
        // 关键检查：所有集合是否初始化
        if (mTimers == null || mNewTimers == null || mRemoveTimers == null)
        {
            Debug.LogError("TimerManager not initialized!");
            return;
        }
        // 添加新定时器
        foreach (TimerNode timer in mNewTimers)
        {
            mTimers.Add(timer.mTimerId, timer);
        }
        mNewTimers.Clear();

        // 更新所有定时器
        foreach (TimerNode timer in mTimers.Values)
        {
            if (!timer.Valid)
            {
                mRemoveTimers.Add(timer.mTimerId);
                continue;
            }

            timer.mPassedTime += deltaTime;
            if (timer.mPassedTime >= timer.mRepeatInterval)
            {
                // 触发回调（支持两种类型）
                if (timer.mCallback != null)
                {
                    timer.mCallback();
                }
                else if (timer.mCallbackWithParam != null)
                {
                    // 动态调用泛型委托
                    var callback = timer.mCallbackWithParam as System.Delegate;
                    if (callback != null)
                    {
                        callback.DynamicInvoke(timer.mParam);
                    }
                }

                timer.mPassedTime -= timer.mRepeatInterval;

                // 处理重复次数
                if (timer.mRepeatTimes > 0)
                {
                    timer.mRepeatTimes--;
                    if (timer.mRepeatTimes == 0)
                    {
                        timer.mRemoved = true;
                        mRemoveTimers.Add(timer.mTimerId);
                    }
                }
            }
        }

        // 清理已删除的定时器
        foreach (int timerID in mRemoveTimers)
        {
            mTimers.Remove(timerID);
        }
        mRemoveTimers.Clear();
    }
}


