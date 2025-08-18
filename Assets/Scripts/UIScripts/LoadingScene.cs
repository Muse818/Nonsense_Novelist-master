using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    [Header("点击此按钮后，跳转的场景的名称")]
    public string sceneName;


    [Header("加载场景最少所用时间")]
    public float leastLoadTime=2f;
    private float process = 0;

    [Header("加载场景所用的画布(不手动设置就默认)")]
    public GameObject canvas;


    private GameObject obj;
    private Slider slider;
    
    private void Start()
    {
        if(canvas==null)
            canvas = Resources.Load<GameObject>("UI/SceneChangeCanvas");
        //if(sceneName!=null)
        //    StartCoroutine(ReallyLoadSceneAsyn(sceneName, null));
    }

    bool hasLoad = false;
    public void EnterNextScene()
    {

       
        obj = Instantiate<GameObject>(canvas);

        slider = obj.GetComponentInChildren<Slider>();
        //生成加载面板，并开始异步加载场景
        obj.GetComponentInChildren<LoadingSlider_t>().LoadSceneAsyn(sceneName, leastLoadTime);
        //加载结束后，关闭面板
    }
    
    public void SwitchStudy()
    {
        SceneManager.LoadScene("Study");
        PoolMgr.GetInstance().Clear();
    }

    public void SwitchShelf()
    {
        SceneManager.LoadScene("BookShelf");

        PoolMgr.GetInstance().Clear();
    }

}
