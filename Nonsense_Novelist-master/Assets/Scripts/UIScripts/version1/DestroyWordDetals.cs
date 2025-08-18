using UnityEngine.UI;
using UnityEngine;

///<summary>
///关闭词条详情面板(挂在词条详情面板上)
///</summary>
class DestroyWordDetals : MonoBehaviour
{
    /// <summary>词条详情</summary>
    public GameObject wordDetail;
    private GameObject otherCanvas;
    private void Start()
    {
        otherCanvas = GameObject.Find("MainCanvas");
    }
    public void CloseDetails()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
        Time.timeScale = GameMgr.instance.timeSpeed;
    }
    /// <summary>
    /// 查看词条详细信息
    /// </summary>
    public void ShowDetails()
    {
        //获取背景板
        Transform a = Instantiate(wordDetail, otherCanvas.transform).transform.GetChild(0).GetChild(0);
        a.transform.GetChild(0).GetComponent<Text>().text = this.GetComponent<AbstractWord0>().wordName;
        a.transform.GetChild(1).GetComponent<Text>().text = this.GetComponent<AbstractWord0>().description;
        Time.timeScale = 0;
    }
}
