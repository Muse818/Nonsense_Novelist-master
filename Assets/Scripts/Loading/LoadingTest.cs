using UnityEngine;
using UnityEngine.SceneManagement;
///<summary>
///加载场景测试（仅供开发测试使用）
///</summary>
class LoadingTest : MonoBehaviour
{
    public string sceneName;
    public GaiZhangAnim gaiZhangAnim;

    public void NextLayer()
    {        
        SceneManager.LoadSceneAsync(sceneName);
        PoolMgr.GetInstance().Clear();
    }
    /// <summary>
    /// 新游戏界面的start按钮
    /// </summary>
    public void StartCombat()
    {
        gaiZhangAnim.gaizhang.SetBool("gaizhang",true);
        Invoke("NextLayer", 2.8f);
    }
}
