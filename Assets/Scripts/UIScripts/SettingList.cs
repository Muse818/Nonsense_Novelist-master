using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingList : MonoBehaviour
{
    [Header("左右组别基物体（手动设置）")]
    public Transform groupL;
    public Transform groupR;
    [Header("设定预制体（手动设置）")]
    public GameObject pingYong;
    public GameObject guiCai;
    public GameObject qiaoSi;

    [Header("左右背景基物体（手动设置）")]
    public Transform groupBGL;
    public Transform groupBGR;
    [Header("设定背景预制体（手动设置）")]
    public GameObject settingBg_U;
    public GameObject settingBg_M;
    public GameObject settingBg_B;

    public void RefreshList()
    {
        //先把所有的还回去
        for(int i= groupL.childCount-1; i>0;i--)
        {
            PoolMgr.GetInstance().PushObj(groupL.GetChild(i).gameObject.name, groupL.GetChild(i).gameObject); 
        }
        for (int i = groupR.childCount - 1; i > 0; i--)
        {
            PoolMgr.GetInstance().PushObj(groupR.GetChild(i).gameObject.name, groupR.GetChild(i).gameObject);
        }


        for (int i = groupBGL.childCount - 1; i > 0; i--)
        {
            PoolMgr.GetInstance().PushObj(groupBGL.GetChild(i).gameObject.name, groupBGL.GetChild(i).gameObject);
        }
        for (int i = groupBGR.childCount - 1; i > 0; i--)
        {
            PoolMgr.GetInstance().PushObj(groupBGR.GetChild(i).gameObject.name, groupBGR.GetChild(i).gameObject);
        }



        //再按照列表刷新
        foreach (var set in GameMgr.instance.settingL)
        {
            AbstractSetting _set = this.gameObject.AddComponent(set) as AbstractSetting;
            
            GameObject obj = pingYong;
            switch (_set.level)
            {
                case SettingLevel.PingYong: { obj = pingYong; } break;
                case SettingLevel.GuiCai: { obj = guiCai; } break;
                case SettingLevel.QiaoSi: { obj = qiaoSi; } break;
              
            }
            PoolMgr.GetInstance().GetObj(obj, (_obj)=>
            {
                _obj.transform.parent = groupL;
                _obj.transform.localPosition = Vector3.zero;
                _obj.transform.localScale = Vector3.one;
  
                _obj.GetComponentInChildren<Text>().text = _set.settingName;
            });
        }

        foreach (var set in GameMgr.instance.settingR)
        {
            AbstractSetting _set = this.gameObject.AddComponent(set) as AbstractSetting;
            GameObject obj = pingYong;
            switch (_set.level)
            {
                case SettingLevel.PingYong: { obj = pingYong; } break;
                case SettingLevel.GuiCai: { obj = guiCai; } break;
                case SettingLevel.QiaoSi: { obj = qiaoSi; } break;

            }
            PoolMgr.GetInstance().GetObj(obj, (_obj) =>
            {
                _obj.transform.parent = groupR;
                _obj.transform.localPosition = Vector3.zero;
                _obj.transform.localScale = Vector3.one;
                print("dsdsd");
                print(_set.settingName);
                _obj.GetComponentInChildren<Text>().text = _set.settingName;
            });
        }



        BGrefresh(true);
        BGrefresh(false);
    }

    private void BGrefresh(bool isL)
    {
        Transform _parent= groupBGL;
        if (!isL) _parent = groupBGR;

        PoolMgr.GetInstance().GetObj(settingBg_U, (_obj) =>
        {
            _obj.transform.parent = _parent;
            _obj.transform.localPosition = Vector3.zero;
            _obj.transform.localScale = Vector3.one;
        });

        foreach (var set in (isL ? GameMgr.instance.settingL : GameMgr.instance.settingR))
        {
            PoolMgr.GetInstance().GetObj(settingBg_M, (_obj) =>
            {
                _obj.transform.parent = _parent;
                _obj.transform.localPosition = Vector3.zero;
                _obj.transform.localScale = Vector3.one;
            });
        }
        PoolMgr.GetInstance().GetObj(settingBg_B, (_obj) =>
        {
            _obj.transform.parent = _parent;
            _obj.transform.localPosition = Vector3.zero;
            _obj.transform.localScale = Vector3.one;
        });

    }
}
