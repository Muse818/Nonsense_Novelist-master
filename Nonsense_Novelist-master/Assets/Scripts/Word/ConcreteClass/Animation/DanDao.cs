using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;
///<summary>
///弹道（挂在子弹上）
///</summary>
class DanDao: MonoBehaviour
{
    /// <summary>外部赋值</summary>
    public GameObject aim;
    /// <summary>外部赋值</summary>
    public float bulletSpeed = 1;
    bool a;

    private void OnEnable()
    {
        if(!a)
            this.enabled= false;
    }

    public void SetOff(Vector3 birthPos)
    {
        this.transform.position = birthPos;
        a = true;
        this.enabled= true;
    }

    private void Update()
    {
        if (CharacterManager.instance.pause) return;
        if(a && aim != null)
        {
            a = false;
            StartCoroutine(Wait());
            this.transform.right=  aim.transform.position- this.transform.position;
            this.transform.Translate((aim.transform.position-this.transform.position).normalized*bulletSpeed,Space.World);
        }
        if(aim!=null && Vector3.Distance(this.transform.position, aim.transform.position)<1)
        {
            this.gameObject.SetActive(false);
        }
        if( aim==null)
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
        a = true;
    }
}
