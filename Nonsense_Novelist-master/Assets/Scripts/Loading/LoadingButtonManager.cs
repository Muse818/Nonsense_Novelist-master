using UnityEngine.SceneManagement;
using UnityEngine;

///<summary>
///全局跳转界面按钮Manager
///</summary>
class LoadingButtonManager : MonoBehaviour
{
    public GaiZhangAnim gaiZhangAnim;

    public LoadingScript loadingScript;
    public string startGame = "Login0";
    public string bookDesk = "BookDesk2";
    public string newGame = "NewGame3";
    public string combat = "ShootCombat";

    private int settingNum = 1;
    public AudioSource audioSource;

    private void Start()
    {
        /* 如果使用同一个loading场景
         * if (SceneManager.GetActiveScene().buildIndex == 1)
         {
             sceneNum = 2;
         }*/
    }
    /// <summary>
    /// 开始游戏界面开始按钮 
    /// </summary>
    public void StartGame()
    {
        Invoke("LoadingScript", 1f);
    }
    /// 返回开始游戏界面
    public void BackToStartGame()
    {
        SceneManager.LoadSceneAsync(startGame);
        PoolMgr.GetInstance().Clear();
     
    }
    public void NextToNewGame()
    {
        SceneManager.LoadSceneAsync(newGame);
        PoolMgr.GetInstance().Clear();
    }
    public void BackToBookDesk()
    {
        SceneManager.LoadSceneAsync(bookDesk);
        PoolMgr.GetInstance().Clear();
    }
    /// <summary>
    /// 启用加载场景脚本
    /// </summary>
    public void LoadingScript()
    {
        loadingScript.enabled = true;
    }
    /// <summary>
    /// 新游戏界面开始按钮 →战斗界面
    /// </summary>
    public void StartCombat()
    {
        //播放盖章动画
        gaiZhangAnim.gaizhang.SetBool("gaizhang", true);
        audioSource.Play();
        Invoke("LoadingScript", 4.6f);
    }
    /// <summary>
    /// 返回新游戏界面
    /// </summary>
    public void BackNewGame()
    {
        SceneManager.LoadSceneAsync(newGame);
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GameSetting()
    {
        settingNum++;
        Time.timeScale = settingNum%2;
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void GameQuit()
    {
        Application.Quit();
    }
}
