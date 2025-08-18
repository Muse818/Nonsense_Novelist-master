using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换管理
/// </summary>
public class SceneChangeManager : SingletonBase<SceneChangeManager>
{
    /// <summary>
    /// 同步加载场景
    /// 其中MonoManager为公共Mono模块对象用于开启协程等
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="completed">场景加载完成需要做的事情</param>
    public void LoadScene(string name,UnityAction completed)
    {
        //加载场景
        SceneManager.LoadScene(name);
        //执行场景加载完成做的事情
        completed.Invoke();
    }

    /// <summary>
    /// 异步加载场景方法
    /// 可以通过事件中心类EventCenter 监听名为"进度条更新"的事件
    /// 其中含有加载进度用于更新进度条
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="completed">加载完做的事情</param>
    private void LoadSceneAsync(string name, UnityAction completed)
    {
        MonoManager.GetInstance().controller.StartCoroutine(LoadSceneByCoroutine(name,completed));
    } 

    //协程加载场景的方法
    private IEnumerator LoadSceneByCoroutine(string name, UnityAction completed)
    {
        AsyncOperation  ao = SceneManager.LoadSceneAsync(name);
       
        //使用异步加载操作类判断,加载完成并操作进度
        while (!ao.isDone)
        {
            //分发更新进度条的事件,外部可以参考监听,参数为当前进度
            //分发"进度条更新"事件,传入加载过程
            EventCenter.GetInstance().EventTrigger("进度条更新",ao.progress);
            //ao.progress加载进度 0-1
            yield return ao.progress;
        }
        //加载完成后执行的方法
        completed();
    }
}
