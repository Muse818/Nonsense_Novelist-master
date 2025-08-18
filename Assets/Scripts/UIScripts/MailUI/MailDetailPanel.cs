using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MailTable;

public class MailDetailPanel : BasePanel<MailDetailPanel>
{
    //返回按钮
    public Button backBtn;
    //信件内容背景图片
    public Image contentImage;
    //读者评分文字
    public TextMeshProUGUI scoreText;
    //读者评分图片文字
    public Image scoreTitleImg;
    //读者评分底图
    public Image scorebgImg;
    //收件人
    public TextMeshProUGUI dearText;
    //信件内容
    public TextMeshProUGUI contentText;
    //新建尾部
    public TextMeshProUGUI autherText;
    //点击此按钮领取附件
    public Button attchBtn;
    
    //信件信息
    private MailInfo mailInfo;
    //背景图片路径
    private string imgPath = "UI/Mail/";

    //储存附件列表:从UpdateDetailInfo函数读入
    protected override void Init()
    {
        //返回按钮
        backBtn.onClick.AddListener(() => {
            //当前隐藏面板
            Hide();
            //显示预览面板
            MailPreviewPanel.Instance.Show();
        });

        //领取附件:根据MailInfo中的`附件id和数量`领取
        attchBtn.onClick.AddListener(() => {
            //领取附件
            tackAttach();
        });

        //初始化完成后,详情面板暂时隐藏
        Hide();
    }

    /// <summary>
    /// 更新面板显示信息:根据传入的MailInfo
    /// </summary>
    public void ShowMailDetailInfo(MailInfo info)
    {
        this.mailInfo = info;
        Show();
    }

    /// <summary>
    /// 重写基类的Show,在每次显示时根据信件信息显示
    /// </summary>
    public override void Show()
    {
        dearText.text = mailInfo.dear;
        contentText.text = mailInfo.mailBody;
        autherText.text = mailInfo.autherName;
        scoreText.text = mailInfo.score.ToString();
        
        //附件已经领取,则取消领取按钮,不能重复领取
        if(mailInfo.attachIsTake)
            attchBtn.gameObject.SetActive(false);

        //只有报社编辑显示读者评分模块
        if(mailInfo.autherType == E_MailAutherType.BaoShe)
        {
            scoreText.gameObject.SetActive(true);
            scoreTitleImg.gameObject.SetActive(true);
            scorebgImg.gameObject.SetActive(true);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            scoreTitleImg.gameObject.SetActive(false);
            scorebgImg.gameObject.SetActive(false);
        }
        //根据信件类型显示背景图片
        switch (mailInfo.autherType)
        {
            //报社
            case E_MailAutherType.BaoShe:
                contentImage.sprite = LoadImg(imgPath + "officepaper");
                break;
            //安德鲁
            case E_MailAutherType.KeLao:
                contentImage.sprite = LoadImg(imgPath + "doctorpaper");
                break;
            //粉丝彼得
            case E_MailAutherType.BiDe:
                contentImage.sprite = LoadImg(imgPath + "fanspaper");
                break;
            //佐佐木
            case E_MailAutherType.WenTeCen:
                contentImage.sprite = LoadImg(imgPath + "friendpaper");
                break;
        }
        base.Show();
    }

    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="imgPath"></param>
    /// <returns></returns>
    public Sprite LoadImg(string imgPath)
    {
        Sprite sprite = Resources.Load<Sprite>(imgPath);
        //使用加载的图片或者默认图片
        return sprite ?? contentImage.sprite;
    }

    public void tackAttach()
    {
        //领取根据附件id领取信件的逻辑
        this.mailInfo.attachIsTake = true;
        //持久化存储已领取状态
        MailDataManager.instance.Cover(mailInfo);
    }
}
