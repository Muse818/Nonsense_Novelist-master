using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private static Dictionary<EventEnum, List<INNEventHandler>> _mHandlers = new Dictionary<EventEnum, List<INNEventHandler>>();
    
    private static EventManager _mInstance = null;
    public static EventManager Instance
    {
        get
        {
            if (_mInstance == null)
            {
                _mInstance = new GameObject("EventManager", typeof(EventManager)).GetComponent<EventManager>();
                DontDestroyOnLoad(_mInstance);
            }

            return _mInstance;
        }
    }
    
    public static INNEventHandler Subscribe(EventEnum type, EventHandler0.CallBack callBack)
    {
        if (!_mHandlers.TryGetValue(type, out var handlers))
        {
            handlers = new List<INNEventHandler>();
            _mHandlers.Add(type, handlers);
        }
        
        var handler = new EventHandler0
        {
            mFunc = callBack
        };
        handlers.Add(handler);

        return handler;
    }

    public static void Unsubscribe(EventEnum type, INNEventHandler handler)
    { 
        if (_mHandlers.TryGetValue(type, out var handlers))
        {
            handlers.Remove(handler);
        }
    }

    public static INNEventHandler Subscribe<T>(EventEnum type, EventHandlerOneParam<T>.CallBack callBack)
    {
        if (!_mHandlers.TryGetValue(type, out var handlers))
        {
            handlers = new List<INNEventHandler>();
            _mHandlers.Add(type, handlers);
        }

        var handler = new EventHandlerOneParam<T>
        {
            mFunc = callBack
        };
        handlers.Add(handler);

        return handler;
    }

    public static INNEventHandler Subscribe<T1, T2>(EventEnum type, EventHandlerTwoParams<T1, T2>.CallBack callBack)
    {
        if (!_mHandlers.TryGetValue(type, out var handlers))
        {
            handlers = new List<INNEventHandler>();
            _mHandlers.Add(type, handlers);
        }

        var handler = new EventHandlerTwoParams<T1, T2>
        {
            mFunc = callBack
        };
        handlers.Add(handler);

        return handler;
    }

    public static INNEventHandler Subscribe<T1, T2, T3>(EventEnum type, EventHandlerThreeParams<T1, T2, T3>.CallBack callBack)
    {
        if (!_mHandlers.TryGetValue(type, out var handlers))
        {
            handlers = new List<INNEventHandler>();
            _mHandlers.Add(type, handlers);
        }

        var handler = new EventHandlerThreeParams<T1, T2, T3>
        {
            mFunc = callBack
        };
        handlers.Add(handler);

        return handler;
    }

    // clear invalid handles
    private static void Sweep(List<INNEventHandler> targets)
    {
        var invalids = new List<INNEventHandler>();
        foreach (var handler in targets)
        {
            if (!handler.Valid)
                invalids.Add(handler);
        }
        
        foreach (var invalid in invalids)
            targets.Remove(invalid);
    }

    public static void Invoke(EventEnum type)
    {
        if (_mHandlers.TryGetValue(type, out var handlers))
        {
            Sweep(handlers);
            foreach (var handler in handlers)
            {
                handler.Invoke();
            }
        }
    }

    public static void Invoke<T>(EventEnum type, T param)
    {
        if (_mHandlers.TryGetValue(type, out var handlers))
        {
            Sweep(handlers);
            foreach (var handler in handlers)
            {
                handler.SetParam1(param);
                handler.Invoke();
            }
        }
    }

    public static void Invoke<T1, T2>(EventEnum type, T1 param1, T2 param2)
    {
        if (_mHandlers.TryGetValue(type, out var handlers))
        {
            Sweep(handlers);
            foreach (var handler in handlers)
            {
                handler.SetParam1(param1);
                handler.SetParam2(param2);
                handler.Invoke();
            }
        }
    }

    public static void Invoke<T1, T2, T3>(EventEnum type, T1 param1, T2 param2, T3 param3)
    {
        if (_mHandlers.TryGetValue(type, out var handlers))
        {
            Sweep(handlers);
            foreach (var handler in handlers)
            {
                handler.SetParam1(param1);
                handler.SetParam2(param2);
                handler.SetParam3(param3);
                handler.Invoke();               
            }
        }
    }
}
