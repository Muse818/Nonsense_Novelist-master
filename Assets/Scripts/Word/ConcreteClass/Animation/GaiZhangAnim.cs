
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///盖章动画(战斗界面的关卡和章节结束panel里面的start)
///</summary>
class GaiZhangAnim : MonoBehaviour
{
    /// <summary>盖章的动画</summary>
    public Animator gaizhang;
    /// <summary>选关的动画</summary>
    public Animator level;
    /// <summary>是否第一次点击按钮</summary>
    private bool isGZFirst = false;
    /// <summary>是否第一次点击按钮</summary>
    private bool isLevelFirst = false;
    public AudioSource gzAudio;

    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Combat")
        {
            gaizhang.SetBool("start", true);
        }
    }
    public void GZ1_2()
    {
        if (isGZFirst == false)
        {
            gaizhang.SetBool("GZ1_2", true);
            isGZFirst = true;
        }
        Invoke("PlayGZClip", 1.5f);
    }
    public void Level1_2()
    {
        if(isLevelFirst == false)
        {
            level.SetBool("level1_2", true);
            isLevelFirst = true;
        }
    }
    public void PlayGZClip()
    {
        gzAudio.Play();
    }
}
