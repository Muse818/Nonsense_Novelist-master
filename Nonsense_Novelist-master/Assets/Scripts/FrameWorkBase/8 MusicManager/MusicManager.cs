using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/**相关组件
* AudioLister组件 一般在摄像机上,
* AudioSource组件 一般在播放源头上,用于播放音乐
*/

/// <summary>
/// 音乐管理器
/// 规定背景音乐和音效放在Resources文件夹中
/// 分别在Music/Back/ 和 Music/Sound路径下
/// </summary>
public class MusicManager : SingletonBase<MusicManager>
{
    //背景音乐播放使用的组件
    private AudioSource backMusic = null;

    //音效播放对象
    private GameObject soundObj = null;
    //音效播放容器
    private List<AudioSource> soundList = new List<AudioSource>();

    //背景音乐大小
    private float backValue = 0.5f;
    private float soundValue = 0.5f;

    public MusicManager()
    {
        //添加一个帧更新,用于音效播放完毕的检测
        MonoManager.GetInstance().AddUpdateListener(MusicUpdate);
    }

    //帧更新,用于检测音效播放完毕的移除
    public void MusicUpdate()
    {
        for (int i = soundList.Count - 1; i >= 0; i--) 
        {
            //当音乐没有在播放时,即已经播放完毕
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">背景音乐的名字</param>
    public void PlayBackMusic(string name)
    {
        //添加组件
        if (backMusic == null)
        {
            GameObject backMusicObj = new GameObject();
            backMusicObj.name = "backMusic";
            backMusic = backMusicObj.AddComponent<AudioSource>();
        }
        //异步加载资源并将资源使用到AudioSource
        ResourceManager.GetInstance().LoadResourceAsync<AudioClip>("Music/Back/" + name, (clip) =>
        {
            backMusic.clip = clip;
            backMusic.volume = backValue;
            backMusic.Play();
        });
    }


    /// <summary>
    /// 暂停当前播放的背景音乐
    /// </summary>
    public void PauseBackMusic()
    {
        if (backMusic == null)
            return;
        backMusic.Pause();
    }


    /// <summary>
    /// 停止当前播放的背景音乐
    /// </summary>
    public void StopBackMusic()
    {
        if (backMusic == null)
            return;
        backMusic.Stop();
    }


    /// <summary>
    /// 设置背景音乐大小
    /// </summary>
    /// <param name="value">新的音量大小</param>
    public void ChangeBackvalue(float value)
    {
        backValue = value;
        if (backMusic == null)
            return;
        backMusic.loop = true;
        backMusic.volume = backValue;
    }

    /// <summary>
    /// 播放音效
    /// 通过组件的添加和移除播放音效,每个音效单独一个播放组件
    /// </summary>
    /// <param name="name">音效名称</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="callback">音效播放用的组件</param>
    public void PlaySound(string name,bool isLoop, UnityAction<AudioSource> callback = null)
    {
        //制造音效对象
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "soundObj";
        }

        //加载音效资源赋值
        ResourceManager.GetInstance().LoadResourceAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource sound = soundObj.AddComponent<AudioSource>();
            sound.clip = clip;
            sound.volume = soundValue;
            sound.loop = isLoop;
            sound.Play();
            soundList.Add(sound);
            //使用音效函数
            if (callback!=null)
            {
                callback(sound);
            }
        });

    }

    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeSoundValue(float Value)
    {
        soundValue = Value;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = soundValue;
        }
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="soundSource">音效播放用的组件</param>
    public void StopSound(AudioSource soundSource)
    {
        if (soundList.Contains(soundSource))
        {
            soundList.Remove(soundSource);
            soundSource.Stop();
            GameObject.Destroy(soundSource);
        }
    }
}
