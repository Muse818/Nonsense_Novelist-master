using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
abstract public class AbstractWord0 : MonoBehaviour
{
    /// <summary>词性</summary>
    public WordKindEnum wordKind;
    /// <summary>所属书目</summary>
    public BookNameEnum bookName=BookNameEnum.allBooks;
    /// <summary>名称（词汇本体）</summary>
    [HideInInspector] public string wordName;
    /// <summary>简介（弃用）</summary>
    [HideInInspector] public string brief;
    /// <summary>详细描述</summary>
    [HideInInspector] public string description;

    /// <summary>外部调用用</summary>
    [HideInInspector] static public string s_description;
    /// <summary>名称（词汇本体）</summary>
    [HideInInspector] static public string s_wordName;


    /// <summary>别称（弃用）</summary>
    [HideInInspector] public List<string> nickname=new List<string>();
    /// <summary>稀有度（弃用）</summary>
    [HideInInspector] public int rarity=1;
    /// <summary>弹射机制 </summary>
    public List<WordCollisionShoot> wordCollisionShoots = new List<WordCollisionShoot>();

    /// <summary>
    /// 使用时文本
    /// </summary>
    virtual public string UseText()
    {
        return null;
    }
    /// <summary>
    /// 词组的标签（类型）
    /// </summary>
    /// <returns></returns>

    virtual public string[] DetailLable()
    {
        return null;
    }
}
