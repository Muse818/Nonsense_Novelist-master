using UnityEngine.UI;
using UnityEngine;

public class WordCountText : MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text ="x "+ count.ToString();

    }

}
