using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [Tooltip("销毁前的延迟时间（秒）")]
    public float delay=2f;
    void Start()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, delay);
        }
    }
}