
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

public class CoreEntity : IDisposable
{
    private bool mDisposed = false;
    public bool IsDisposed => mDisposed;

    public CoreEntity()
    {
        
    }
    
    ~CoreEntity()
    {
        Dispose(false);
    }   

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!mDisposed)
        {
            mDisposed = true;
            
            if (disposing)
            {
                // 释放托管资源
                OnDisposing();
            }
            // 释放非托管资源
            OnDisposed();
        }
    }
    
    protected virtual void OnDisposing() {}
    
    protected virtual void OnDisposed() {}
}

public static class EntityFunc
{
    public static bool IsValid(this CoreEntity entity)
    {
        return entity != null && !entity.IsDisposed;
    }
}
