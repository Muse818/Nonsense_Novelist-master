
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailWordInfo : MonoBehaviour
{

    public Text title;
    public TextMeshProUGUI info;

    public void ChangeInformation(string _title,string _info)
    {
        title.text = _title;
        info.text = _info;
        
    }

}
