using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入控制管理类
/// 基于:1.事件中心 2.公共Mono模块
/// </summary>
public class InputManager : SingletonBase<InputManager>
{
    //在构造函数中添加全局Mono监听
    public InputManager()
    {
        MonoManager.GetInstance().AddUpdateListener(MyUpdate);
    }
    private bool isOpen = true;

    /// <summary>
    /// 是否开启输入检测
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    /// <summary>
    /// 检测按键按下和抬起事件
    /// 分发 "keyDown" 和 "keyUp" 事件
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        //事件中心分发按下事件
        if (Input.GetKeyDown(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("keyDown", key);

        //事件中心分发抬起事件
        if (Input.GetKeyUp(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("keyUp", key);
    }
    
    //用于输入检测的帧更新
    void MyUpdate()
    {
        //未开启输入检测
        if (!isOpen)
            return;

        /*WASD按键的的分发,其余按键分发也类似*/
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }

}
