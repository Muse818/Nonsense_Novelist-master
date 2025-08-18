using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TextRecord", menuName = "GameTextRecord/newRecord")]
public class TextRecord : ScriptableObject
{
    public int id;//文章的序号，从0起，每次+1
    public int rand;//文章的得分
    public string title;//文章的标题
    public List<string> content;//文章的内容
    public string reply;//系统反馈
    public bool hasRead;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
