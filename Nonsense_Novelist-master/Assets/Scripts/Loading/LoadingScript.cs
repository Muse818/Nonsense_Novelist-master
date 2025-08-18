using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 加载场景脚本（所有场景通用，挂摄像机上）
/// 用法：所有场景都挂摄像机，在加载界面拖拽滑动条+enabled，其他场景unenabled
/// 缺点：loading场景需复制多个使用，可以修改为使用一个脚本设定全局变量，将loading场景改为全局通用
/// 跳转场景方法：在button响应函数里面将脚本设置为enabled
///</summary>
public class LoadingScript : MonoBehaviour
{
    public string LoadingSceneName;//在unity里赋值，要加载的场景名
    public LoadingSlider loadingSlider;//有加载进度条则在Unity加
    /// <summary>进度</summary>
    public AsyncOperation a;
    public void OnEnable()//非加载画面场景设置为不启用, 需跳转场景时再启用此脚本
    {
       a = SceneManager.LoadSceneAsync(LoadingSceneName);
        PoolMgr.GetInstance().Clear();
        if (loadingSlider != null)
       a.allowSceneActivation = false;
    }
    public void Update()
    {
        if (loadingSlider != null)
        {
            loadingSlider.realValue = a.progress;//同步加载进度
            if (loadingSlider.showValue >= 0.9f)//等展示的进度条到0.9了，再跳转场景
            {
                a.allowSceneActivation = true;
            }//下个场景一开始，先糊上与【加载场景】相同的封面，和进度在0.9的进度条，之后让进度条在1~3秒内匀速到1，最后撤掉相同封面
        }
    }
    
}
