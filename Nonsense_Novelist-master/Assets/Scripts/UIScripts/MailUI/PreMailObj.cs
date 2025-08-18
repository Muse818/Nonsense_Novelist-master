using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MailTable;

public enum moveDir
{
    ToTop,
    ToDown
}

/// <summary>
/// 预览信件对象
/// </summary>
public class PreMailObj : MonoBehaviour
{
    //背景图片
    public Image bgImg;
    //收件人
    public TextMeshProUGUI dearText;
    //信件内容
    public TextMeshProUGUI contentText;
    //发件人
    public TextMeshProUGUI authorText;
    //判断框
    public GameObject ctrlerImgObj;

    //信件UI原位置
    [HideInInspector]
    public Vector3 originPos;
    //信件UI浮动目标位置
    [HideInInspector]
    public Vector3 floatPos;
    
    //信件数据
    private MailInfo mailInfo;
    //背景图片路径
    private string imgPath = "UI/Mail/";
    
    private void Start()
    {
        //初始化记录原此信件位置和浮动的目标位置
        originPos = this.transform.localPosition;
        floatPos = originPos + new Vector3(0, 300, 0);

        //初始化信件信息为NULL
        if (mailInfo == null)
            Show(new MailInfo(E_MailAutherType.Default));
    }

    /// <summary>
    /// 信件的鼠标悬浮效果
    /// </summary>
    /// <param name="targetPos">目标位置</param>
    /// <param name="dir">跳动方向</param>
    public void MoveTarget(Vector3 targetPos,moveDir dir)
    {
        //当前位置
        Vector3 curPos = this.transform.localPosition;
        //每回合偏移量
        Vector3 delta = dir == moveDir.ToTop ? 
            new Vector3(0, Time.deltaTime, 0) : -new Vector3(0, Time.deltaTime, 0);
        //更新偏移量
        while (true)
        {
            curPos += delta;
            if (dir == moveDir.ToTop && curPos.y > targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            else if(dir == moveDir.ToDown && curPos.y < targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            this.transform.localPosition = curPos;
        }
    }

    /// <summary>
    /// 根据MailInfo对象刷新页面信息,并显示信件
    /// </summary>
    /// <param name="mailInfo"></param>
    public void Show(MailInfo mailInfo)
    {
        //根据新数据设置页面显示
        this.mailInfo = mailInfo;
        this.dearText.text = mailInfo.dear;
        this.contentText.text = mailInfo.mailBody;
        this.authorText.text = mailInfo.autherName;

        //根据信件类型显示背景图片
        switch (mailInfo.autherType)
        {
            //报社
            case E_MailAutherType.BaoShe:
                bgImg.sprite = LoadImg(imgPath + "officepaper");
                break;
                //安德鲁
            case E_MailAutherType.KeLao:
                bgImg.sprite = LoadImg(imgPath + "doctorpaper");
                break;
                //粉丝彼得
            case E_MailAutherType.BiDe:
                bgImg.sprite = LoadImg(imgPath + "fanspaper");
                break;
                //佐佐木
            case E_MailAutherType.WenTeCen:
                bgImg.sprite = LoadImg(imgPath + "friendpaper");
                break;
        }

        //MailInfo其他信息补充显示

        //显示对象
        this.gameObject.SetActive(true);
        ctrlerImgObj.SetActive(true);
    }

    public Sprite LoadImg(string imgPath)
    {
        Sprite sprite = Resources.Load<Sprite>(imgPath);
        //使用加载的图片或者默认图片
        return sprite ?? bgImg.sprite;
    }

    /// <summary>
    /// 隐藏此预览信件及其判断框
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
        ctrlerImgObj.SetActive(false);
    }

    /// <summary>
    /// 点击此信件后的动作
    /// </summary>
    public void ClickAction()
    {
        //传入信件对象mailInfo,调用面板方法,打开信件详情
        MailDetailPanel.Instance.ShowMailDetailInfo(this.mailInfo);
        //隐藏预览面板
        MailPreviewPanel.Instance.Hide();
    }

}
