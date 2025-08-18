
public interface INNEventHandler
{
    bool Valid { get; }

    void SetParam1<T>(T param) { }

    void SetParam2<T>(T param) { }
    void SetParam3<T>(T param) { }

    void Invoke();
}

public class EventHandler0: INNEventHandler
{
    public delegate void CallBack();

    public CallBack mFunc;

    public bool Valid => mFunc != null && !mFunc.Target.Equals(null);
    
    public void Invoke()
    {
        mFunc?.Invoke();
    }
}

public class EventHandlerOneParam<TY1>: INNEventHandler
{
    public delegate void CallBack(TY1 param);
    public CallBack mFunc;
    private object _mParam;

    public bool Valid => mFunc != null && !mFunc.Target.Equals(null);

    public void SetParam1<T>(T param)
    { 
        _mParam = typeof(TY1) == typeof(T) ? param : null;
    }
    
    public void Invoke()
    {
        if (mFunc != null && _mParam != null)
        {
            mFunc((TY1)_mParam);
        }
    }
}

public class EventHandlerTwoParams<TY1, TY2>: INNEventHandler
{
    public delegate void CallBack(TY1 param1, TY2 param2);

    public CallBack mFunc;
    private object _mParam1;
    private object _mParam2;

    public bool Valid => mFunc != null && !mFunc.Target.Equals(null);

    public void SetParam1<T>(T param)
    { 
        _mParam1 = typeof(TY1) == typeof(T) ? param : null;
    }

    public void SetParam2<T>(T param)
    {
        _mParam2 = typeof(TY2) == typeof(T) ? param : null;
    }
    
    public void Invoke()
    {
        if (mFunc != null && _mParam1 != null && _mParam2 != null)
        {
            mFunc((TY1)_mParam1, (TY2)_mParam2);
        }
    }
}

public class EventHandlerThreeParams<TY1, TY2, TY3>: INNEventHandler
{
    public delegate void CallBack(TY1 param1, TY2 param2, TY3 param3);

    public CallBack mFunc;
    private object _mParam1;
    private object _mParam2;
    private object _mParam3;

    public bool Valid => mFunc != null && !mFunc.Target.Equals(null);

    public void SetParam1<T>(T param)
    { 
        _mParam1 = typeof(TY1) == typeof(T) ? param : null;
    }

    public void SetParam2<T>(T param)
    {
        _mParam2 = typeof(TY2) == typeof(T) ? param : null;
    }
    
    public void SetParam3<T>(T param) 
    {
        _mParam3 = typeof(TY2) == typeof(T) ? param : null;
    }
    
    public void Invoke()
    {
        if (mFunc != null && _mParam1 != null && _mParam2 != null && _mParam3 != null)
        {
            mFunc((TY1)_mParam1, (TY2)_mParam2, (TY3)_mParam3);
        }
    }
}