using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI面板基类
/// UI面板继承此类即可获取Mono的支持和常用父类功能
/// </summary>
/// <typeparam name="T">子类面板</typeparam>
public abstract class BasePanel<T> : MonoBehaviour where T:class
{
    /// <summary>
    /// 单例对象,方便面板间的调用和信息交换
    /// </summary>
    public static T Instance;

    //管理面板透明度的组件
    CanvasGroup canvasGroup;
    //表示面板是否隐藏的标识
    private bool isShow = true;
    //UI渐变显示/隐藏速度
    private float showSpeed = 5f;
    //隐藏面板后的回调
    UnityAction hideCallback = null;

    private void Awake()
    {
        Instance = this as T;
        //初始化Panel的CanvasGroup组件
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化函数
    /// 代替子类Start逻辑,建议用于字段初始化和UI事件绑定
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 显示面板
    /// 子类可重写,建议可用于更新,每次刷新页面需要更新的数据
    /// </summary>
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
        isShow = true;
        //先设置面板透明度为0,待渐变为1
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// 隐藏面板
    /// 可传入无参委托,建议可用于销毁面板预制体等
    /// </summary>
    /// <param name="callBack">隐藏面板后的回调</param>
    public virtual void Hide(UnityAction callBack = null)
    {
        isShow = false;
        //先设置面板透明度为1,待渐变为0
        canvasGroup.alpha = 1;
        hideCallback += callBack;
    }
    
    /// <summary>
    /// UI页面退场特效
    /// </summary>
    public virtual void PlayFadeOut()
    {
        if (isShow == false && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= showSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                this.gameObject.SetActive(false);
                //隐藏后调用委托移除面板对象
                hideCallback?.Invoke();
            }
        }

    }

    /// <summary>
    /// UI页面进场特效
    /// </summary>
    public virtual void PlayFadeIn()
    {
        if (isShow == true && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += showSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
    }

    /// <summary>
    /// update帧更新方法:
    /// 子类可重写在base()逻辑上提供新功能
    /// </summary>
    protected virtual void Update()
    {
        PlayFadeIn();
        PlayFadeOut();
    }
    
}
