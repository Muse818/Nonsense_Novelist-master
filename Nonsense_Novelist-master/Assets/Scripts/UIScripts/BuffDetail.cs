using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuffDetail : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPerfab;
    private GameObject go=null;


    /// <summary> 脚本的名称 </summary>
    public string wordname;


    bool isOpen = false;
    public void ClickBuff()
    {
        
        var a = Instantiate(infoPerfab, this.transform.parent.parent.parent.transform);
     
         var _s = wordname;
        System.Type wordType = System.Type.GetType(_s);
        if (wordType != null)
        {
            if (wordType.GetField("s_wordName") == null) print("在" + wordType.ToString() + "中没有定义静态成员s_wordName/s_description");

            a.GetComponent<DetailInfo>().SetInfo((string)wordType.GetField("s_wordName").GetValue(null), (string)wordType.GetField("s_description").GetValue(null));

        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOpen) return;
        if (go != null) return;
        isOpen = true;
        //this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        PoolMgr.GetInstance().GetObj(infoPerfab, (obj) =>
        {
            go = obj;
   
            obj.transform.parent = this.transform.parent.parent.parent.transform;
            obj.transform.localScale = Vector3.one*0.4f;
            obj.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, 
                Camera.main.ScreenToWorldPoint(eventData.position).y,0);
            obj.transform.position += new Vector3(0.8f, 0.7f, 0);
           var _s = wordname;
            System.Type wordType = System.Type.GetType(_s);
            if (wordType != null)
            {
                if (wordType.GetField("s_wordName") == null) print("在" + wordType.ToString() + "中没有定义静态成员s_wordName/s_description");

                obj.GetComponent<DetailInfo>().SetInfo((string)wordType.GetField("s_wordName").GetValue(null), (string)wordType.GetField("s_description").GetValue(null));

            }
        });

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isOpen) return;
        isOpen = false;
        PoolMgr.GetInstance().PushObj(go.name, go);
        go = null;
    }
}
