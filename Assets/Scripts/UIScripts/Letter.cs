using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    //下面的字体关联:此对象则不必关联,下版本删除
    public GameObject firstPanel;
    public Text nulltext;
    
    //信件内容
    public TextMeshProUGUI letterContentText;
    //邮箱号码
    public TextMeshProUGUI coinText;
    //读者评分
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
     // FirstLetter();
    }

    void FirstLetter()
    {
        switch (RecordMgr.instance.recordList.Count)
        {
            case 0:
                {
                    nulltext.gameObject.SetActive(true);
                    firstPanel.SetActive(false);
                    nulltext.text = "目前还没收到信件！";
                }
                break;
            case 1:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "亲爱的安德森:\r\n\t给你准备的蜥蜴粉每天记得冲泡，我在杂志上看到你的文章了，你不会以那种状态还在继续写作吧!\r\n\r\n\t\t\t发件人：医生";
                    scoreText.text = RecordMgr.instance.recordList[0].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "亲爱的安德森:\r\n\t给你准备的蜥蜴粉每天记得冲泡，我在杂志上看到你的文章了，你不会以那种状态还在继续写作吧!\r\n\r\n\t\t\t发件人：医生";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[0].rand.ToString();
                }
                break;
            case 2:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "安德森先生：\r\n\t我拜读了您的新作，简直是如听仙乐而暂明，实在是迫不及待想看到您的存稿了，下一期...下一期！看不到您作品的每一天我都恨不得想杀了您呢，奥实在抱歉，我又控制不住讲出心里话了。\r\n\t\t发件人：狂热粉丝";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[1].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "安德森先生：\r\n\t我拜读了您的新作，简直是如听仙乐而暂明，实在是迫不及待想看到您的存稿了，下一期...下一期！看不到您作品的每一天我都恨不得想杀了您呢，奥实在抱歉，我又控制不住讲出心里话了。\r\n\t\t发件人：狂热粉丝";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[1].rand.ToString();

                }
                break;
            case 3:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "安德森：\r\n\t虽然很抱歉，但我不得不说，你这样是咎由自取。\r\n\r\n\t\t\t发件人：医生";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[2].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "安德森：\r\n\t虽然很抱歉，但我不得不说，你这样是咎由自取。\r\n\r\n\t\t\t发件人：医生";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[2].rand.ToString();
                }
                break;
            case 4:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "安德森先生\r\n\t您上次邮寄的作品已被刊登至第5期，另外，请您下次别再拖延至截稿日期后，您知道的，就算是您这样的作家，也不是次次都能宽限时间的。\r\n\r\n\t\t发件人：报社编辑";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[3].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text =  "安德森先生\r\n\t您上次邮寄的作品已被刊登至第5期，另外，请您下次别再拖延至截稿日期后，您知道的，就算是您这样的作家，也不是次次都能宽限时间的。\r\n\r\n\t\t发件人：报社编辑";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[3].rand.ToString();
                }
                break;
            default:
                {
                    nulltext.gameObject.SetActive(true);
                    firstPanel.SetActive(false);
                    nulltext.text = "没有更多信件了！";
                }
                break;
        }
    }
}
