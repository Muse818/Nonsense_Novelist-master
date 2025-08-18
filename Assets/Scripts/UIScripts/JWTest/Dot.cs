using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //this.gameObject.name = "1";
            //Vector3 dir = Vector3.Reflect(direction, collision.GetContact(0).normal);
        }
    }
    

}
