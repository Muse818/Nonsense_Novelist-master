
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在词条小球身上
/// </summary>
public class WordVec : MonoBehaviour
{
    Rigidbody2D rigid;
    public float velocity=2f;


    SpriteRenderer sp;
    public Color color;
    public Color oriColor;

    private GameObject Info;
    private WordInformation Info_obj;

    Vector3 oriPos = Vector3.one;
    Vector3 oriScale = Vector3.one;
    private float cardSize = 0.6f;//卡牌大小
    private Vector3 cardOffset = new Vector3(100f,0f, 0);//卡牌偏移
    private float speed=3;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>();


        Info = null;
    }

    void Update()
    {
        if (CharacterManager.instance.pause)
        {
            StopAllCoroutines();
            return;
        } 


        if (rigid.velocity.magnitude < velocity)
        {
            rigid.velocity = new Vector2(0, 0);
            
        }
    }

    private void OnMouseOver()
    {
        if (CharacterManager.instance.pause) return;

        if (Info != null) return;

        if (this.GetComponent<AbstractWord0>() == null)
        {
            print("can not find word in ball");
            return;
        }

        oriColor = sp.color;

        PoolMgr.GetInstance().GetObj("UI/WordInformation", (obj) =>
         {       
             Info = obj;
             Info_obj = obj.GetComponentInChildren<WordInformation>();
             Info_obj.ChangeInformation(this.GetComponent<AbstractWord0>());
             Info_obj.SetIsDetail(false);

             obj.transform.parent = this.transform; 
             
             oriPos= Info_obj.transform.position;
             Info_obj.transform.position = Camera.main.WorldToScreenPoint(this.transform.position)+ cardOffset;

             oriScale = Info_obj.transform.localScale;
             Info_obj.transform.localScale = Vector3.zero;
             StartCoroutine(Bigger());
         });
            
    }
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    IEnumerator Bigger()
    {
        while ((Info!=null)&&(Info_obj.transform.localScale.x< (oriScale * cardSize).x))
        {
            Info_obj.transform.localScale += oriScale * cardSize*Time.deltaTime* speed;

            sp.color -= (oriColor-color) * 0.04f;
            yield return waitForFixedUpdate;
        }
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();

        if (Info == null) return;

        var _obj = Info.GetComponentInChildren<WordInformation>();
        _obj.transform.position = oriPos;
        _obj.transform.localScale = oriScale;

        sp.color = oriColor;

        PoolMgr.GetInstance().PushObj(Info.name, Info);
        Info = null;
        Info_obj = null;


    }

    private void OnDestroy()
    {
        StopAllCoroutines(); 
    }
}
