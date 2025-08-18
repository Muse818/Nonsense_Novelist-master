using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioYinXiao : MonoBehaviour
{

    public AudioClip click;
    public AudioClip hover;
    public AudioClip wallCol;
    public AudioClip[] eventBGM;
    private AudioSource audioSource;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ClickPlay()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    public void TanHui() {
        audioSource.clip = hover;
        audioSource.Play();
    }
    public void WallCol()
    {
        audioSource.clip = wallCol;
        audioSource.Play();
    }
    public void RandomPlay()
    {
        a = UnityEngine.Random.Range(0, eventBGM.Length);
        audioSource.loop = false;
        while (eventBGM[a] == audioSource.clip)
        {
            a = UnityEngine.Random.Range(0, eventBGM.Length);
        }
        audioSource.clip = eventBGM[a];
        audioSource.Play();
    }
}
