using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SettingLevel
{
    PingYong=0,
    QiaoSi=1,
    GuiCai=2,
    DuTe=3,
}
public class AbstractSetting : MonoBehaviour
{

    [HideInInspector]public SettingLevel level = SettingLevel.PingYong;
    [HideInInspector] public string settingName;
    [HideInInspector] public string res_name;
    [HideInInspector] public string info;
    [HideInInspector] public List<string> lables;
    [HideInInspector] public CampEnum camp;

    public bool hasAdd = false;
    /// <summary>
    /// 在监听的变量发生变化的时候，执行这个
    /// </summary>
    /// <returns></returns>
    /// 

    public virtual void Awake()
    {
        
    }
    public virtual void Start()
    {
        
    }
    public virtual void Init()
    {
        
    }
}
