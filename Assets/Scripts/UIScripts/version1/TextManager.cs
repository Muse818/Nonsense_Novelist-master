
using UnityEngine;
using UnityEngine.UI;

///<summary>
///游戏中的文本内容(挂在TextManager身上
///</summary>
class TextManager : MonoBehaviour
{
    /// <summary>剧情标题</summary>
    public Text headline;
    /// <summary>关卡剧情</summary>
    public Text levelText;
    /// <summary>剧情脚本</summary>
    public BookNvWuXueTu firstText;
    /// <summary>获取关卡num的脚本</summary>
    public CharacterTranslateAndCamera characterTranslateAndCamera;
    private int num = 0;
    public Text bookContent;
    public Button back;
    public GameObject bookPanel;

    private void Start()
    {
        //每关的文本
        //剧本标题+剧本内容
        if (characterTranslateAndCamera.chapterNum == 1 && characterTranslateAndCamera.guanQiaNum == 0)
        {//第一章第一关
            headline.text = firstText.GetText(1, 0, 1);
            levelText.text = firstText.GetText(1, 1, 1) + firstText.GetText(1, 1, 2) + firstText.GetText(1, 1, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 1 && characterTranslateAndCamera.guanQiaNum == 1)
        {//第一章第二关
            headline.text = firstText.GetText(1, 0, 1);
            levelText.text = firstText.GetText(1, 2, 1) + firstText.GetText(1, 2, 2) + firstText.GetText(1, 2, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 0)
        {//第二章第一关
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 1, 1) + firstText.GetText(2, 1, 2) + firstText.GetText(2, 1, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 1)
        {//第二章第二关
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 2, 1) + firstText.GetText(2, 2, 2) + firstText.GetText(2, 2, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 2)
        {//第二章第三关
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 3, 1) + firstText.GetText(2, 3, 2) + firstText.GetText(2, 3, 3);
        }

        //每章的文本
        headline.text = firstText.GetText(2, 0, 1);
        bookContent.text = "第二章第一幕\n\n" + firstText.GetText(2, 1, 1) +"\n"+ firstText.GetText(2, 1, 2) + "\n" + firstText.GetText(2, 1, 3)+ "\n\n"
            + "第二章第二幕\n\n" + firstText.GetText(2, 2, 1) + "\n" + firstText.GetText(2, 2, 2) + "\n" + firstText.GetText(2, 2, 3) + "\n\n" +
            "第二章第三幕\n\n" + firstText.GetText(2, 3, 1) + "\n" + firstText.GetText(2, 3, 2) + "\n" + firstText.GetText(2, 3, 3);

    }
   /* private void FixedUpdate()
    {
        if (num == 0)//目录
        {
            bookContent.text =
                "第一章：不速之客\n第二章：密特拉\n第三章：沉默的森林\n第四章：梦境探索\n第五章：初临尖塔\n第六章：书塔修炼\n第七章：德洛瑞斯的归来\n第八章：大闹金库\n第九章：缄口地窖";
        }
        else if (num == 1)//第二章第一幕第一部分
        {            
            bookContent.text = "第二章第一幕\n"+firstText.GetText(2, 1, 1) + firstText.GetText(2, 1, 2) + firstText.GetText(2, 1, 3);
        }
        
        else if (num == 2)//第二章第二幕
        {
            bookContent.text = "第二章第二幕\n" + firstText.GetText(2, 2, 1) + firstText.GetText(2, 2, 2) + firstText.GetText(2, 2, 3);
        }
        else if (num == 3)//第二章第三幕
        {
            bookContent.text = "第二章第三幕\n" + firstText.GetText(2, 3, 1) + firstText.GetText(2, 3, 2) + firstText.GetText(2, 3, 3);
            back.interactable = true;
        }

    }*/
    public void RightButton()
    {
        if (num < 3)
        {
            num++;
        }
    }

    public void LeftButton()
    {
        if (num > 0)
        {
            num--;
        }
    }
    public void BackButton()
    {
        bookPanel.SetActive(false);
    }
}
