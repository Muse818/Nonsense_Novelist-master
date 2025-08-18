using UnityEngine;

public class AnimEventReceiver : MonoBehaviour
{
    public delegate void EventCallback();
    
    public event EventCallback OnActBegin;
    public event EventCallback OnActTrigger;
    public event EventCallback OnActEnd;
    public event EventCallback OnDeathTrigger;
    
    public void ActBegin()
    {            
        if (OnActBegin != null)
            OnActBegin();
    }

    public void ActEnd()
    {
        if (OnActEnd != null)
            OnActEnd();
    }

    public void ActTrigger()
    {
        if (OnActTrigger != null)
            OnActTrigger();
    }
    
    public void DeathTrigger()
    {
        if (OnDeathTrigger != null)
            OnDeathTrigger();
    }
}