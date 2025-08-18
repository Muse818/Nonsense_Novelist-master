using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AbstractBook : MonoBehaviour
{
    /// <summary>
    /// 主角
    /// </summary>
    protected AbstractCharacter leadingChara;
    /// <summary>
    /// 战场文，随时随地加
    /// </summary>
    static public string beforeFightText, afterFightText;

    /// <summary>
    /// 所有角色的父物体
    /// </summary>
    protected GameObject fatherObject;

    private void Start()
    {
        fatherObject = GameObject.Find("panel");
        FindLeadingChara();
    }

    /// <summary>
    /// 获取主角
    /// </summary>
    protected void FindLeadingChara()
    {
        if (leadingChara == null)
            leadingChara = fatherObject.transform.Find("SelfCharacter").GetComponentInChildren<AbstractCharacter>();
    }

    abstract public string GetText(int character, int part, int phase);
}
