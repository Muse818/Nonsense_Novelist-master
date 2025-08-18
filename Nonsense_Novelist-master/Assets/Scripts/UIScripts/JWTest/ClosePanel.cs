using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Close()
    {
        Destroy(this.gameObject);
        BookShelfInf.isfirst = true;
    }
}
