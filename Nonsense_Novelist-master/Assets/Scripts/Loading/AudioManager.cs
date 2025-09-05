using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///“Ù–ßπ‹¿Ì∆˜
///</summary>
class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    public string currentScene;

    private void Start()
    {
        DontDestroyOnLoad(audioSource);
    }
    private void Update()
    {

    }
}
