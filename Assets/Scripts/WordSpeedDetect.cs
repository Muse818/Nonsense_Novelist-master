using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpeedDetect : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((rb.velocity.x + rb.velocity.y )<=0.01f) rb.velocity = Vector2.zero;
    }
}
