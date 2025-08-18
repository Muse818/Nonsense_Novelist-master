using LitJson;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MailTable;

public class MailPreviewPanel : BasePanel<MailPreviewPanel>
{
    //信件对象在场景的父节点位置
    public Transform MailPosFather;
    /// <summary>
    /// 分类标签对象
    /// 报社编辑 | 安德鲁医生 | 粉丝彼得 | 佐佐木编辑 
    /// </summary>
    public Toggle[] AuthorToggles;
    public Button backBtn;
    //界面上的信件对象
    public PreMailObj[] mailObjs;
    //翻页按钮上一页下一页
    public TextMeshProUGUI pageNumText;
    public Button AddPageBtn;
    public Button SubPageBtn;
    //无邮件文字
    public TextMeshProUGUI noneMailText;

    //存储所有Mail
    private List<MailInfo> mailDataList = new List<MailInfo>();
    //待显示的MainData
    private List<MailInfo> prepMailData = new List<MailInfo>();
    //页码
    private int pageNum;
    //每一页能容纳的最大信件数量
    private int maxMailCount;
    //当前应该显示的数据在prepMailData的开始和结束索引
    private int startIndex ,lastIndex;
    
    //初始化面板字段
    protected override void Init()
    {
        /* 初始化字段 */
        //显示页码
        pageNum = 1;
        //信箱可容纳信件数量
        maxMailCount = mailObjs.Length;
        //所有信件数据
        ReadMailsData();
        //待显示数据
        RefreshPrepMail();
        //计算显示索引
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //显示信件
        ShowMail();
        //更新翻页按钮显示
        BtnApprUpdate();
        //绑定筛选事件:重新筛选待显示数据,显示信件
        for (int i = 0; i < AuthorToggles.Length; i++)
        {
            AuthorToggles[i].onValueChanged.AddListener((isOn) => {
                RefreshPrepMail();
                ShowMail();
            });
        }

        //上下翻页事件
        AddPageBtn.onClick.AddListener(() => {
            TurnPage(true);
        });
        SubPageBtn.onClick.AddListener(() => {
            TurnPage(false);
        });

        //返回按钮
        backBtn.onClick.AddListener(() => {
            PoolMgr.GetInstance().Clear();
            SceneManager.LoadScene("Study");
        });
    }

    /// <summary>
    /// 读取信件数据并存储(缓存)
    /// </summary>
    private void ReadMailsData()
    {
        //模拟数据
        MailDataManager.instance.CreateMail(100101);
        MailDataManager.instance.CreateMail(100102);
        MailDataManager.instance.CreateMail(100103);
        MailDataManager.instance.CreateMail(100104);
        MailDataManager.instance.CreateMail(100105);
        MailDataManager.instance.CreateMail(100201);
        MailDataManager.instance.CreateMail(100202);
        MailDataManager.instance.CreateMail(100203);
        MailDataManager.instance.CreateMail(100204);
        MailDataManager.instance.CreateMail(100205);
        MailDataManager.instance.CreateMail(100206);
        MailDataManager.instance.CreateMail(100207);
        MailDataManager.Instance.SaveData();

        //从管理器获取数据
        Dictionary<int, MailInfo> dataList = MailDataManager.instance.DataList;
        mailDataList.AddRange(dataList.Values);

        //排序
        mailDataList.Sort((mail1,mail2) =>
        {
            return mail1.id - mail2.id;
        });
    }

    /// <summary>
    /// 根据筛选标签变化刷新待显示数据列表
    /// </summary>
    private void RefreshPrepMail()
    {
        //先清除旧数据
        prepMailData.Clear();
        //然后根据标签是否开启将每个数据加入待显示列表
        for (int i = 0; i < mailDataList.Count; i++) 
        {
            //如果配置此信件当前不显示则跳过此信件
            //if (mailDataList[i].isDisplay == false)
            //    continue;

            if (mailDataList[i].autherType == E_MailAutherType.BaoShe && AuthorToggles[0].isOn) 
                prepMailData.Add(mailDataList[i]);
            
            if (mailDataList[i].autherType == E_MailAutherType.KeLao && AuthorToggles[1].isOn)
                prepMailData.Add(mailDataList[i]);

            if (mailDataList[i].autherType == E_MailAutherType.BiDe && AuthorToggles[2].isOn)
                prepMailData.Add(mailDataList[i]);
        
            if (mailDataList[i].autherType == E_MailAutherType.WenTeCen && AuthorToggles[3].isOn)
                prepMailData.Add(mailDataList[i]);
        }

        //每次筛选标签变化,页码重置
        pageNum = 1;
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //更新UI页码为pageNum
        pageNumText.text = pageNum.ToString();
        //更新翻页按钮
        BtnApprUpdate();
    }

    /// <summary>
    /// 翻页:更新翻页后数据和UI页码
    /// </summary>
    /// <param name="isAdd">页码是否增加,false表示页码后退</param>
    private void TurnPage(bool isAdd)
    {
        //判断翻页并翻页,更新页码
        if (CanTrunPage(isAdd))
        {
            //更新UI页码为 pageNum
            pageNumText.text = pageNum.ToString();
            //显示数据
            ShowMail();
            //更新翻页按钮的显示
            BtnApprUpdate();
        }
    }

    /// <summary>
    /// 更新翻页按钮的显示情况:根据startIndex和lastIndex显示和隐藏左/右翻页按钮
    /// </summary>
    private void BtnApprUpdate()
    {
        //无信件显示
        if(prepMailData.Count == 0)
        {
            //隐藏按钮
            SubPageBtn.gameObject.SetActive(false);
            AddPageBtn.gameObject.SetActive(false);
            //显示无信件按钮
            noneMailText.gameObject.SetActive(true);
            noneMailText.gameObject.SetActive(true);
            return;
        }

        noneMailText.gameObject.SetActive(false);
        /* 更新加减页码的按钮 */
        //左边按钮
        if (startIndex <= 1)
            SubPageBtn.gameObject.SetActive(false);
        else
            SubPageBtn.gameObject.SetActive(true);

        //右边按钮
        if (lastIndex >= prepMailData.Count - 1)
            AddPageBtn.gameObject.SetActive(false);
        else
            AddPageBtn.gameObject.SetActive(true);
    }

    /// <summary>
    /// 判断是否可翻页,更新显示数据索引[startIndex,lastIndex]
    /// </summary>
    /// <returns>是否可以翻页</returns>
    private bool CanTrunPage(bool isAdd)
    {
        //假设翻页,下页开始和结束索引
        int nextStart = 0, nextLast = 0 ; 
        if (isAdd)
        {
            //下一页页码为pageNum + 1
            //下页开始索引 = 每页显示最大个数len * ( page - 1)
            nextStart = maxMailCount * (pageNum + 1 - 1);
            //根据翻页后的开始索引判断是否可以翻页
            if (nextStart > prepMailData.Count - 1)
                //不可以翻页,不更新任何东西
                return false;

            //可以翻页:更新页码和显示索引
            pageNum++;
            //显示开始索引
            startIndex = nextStart;
            //显示结束索引:开始索引start + (len-1)
            lastIndex = startIndex + maxMailCount - 1;
            //剩余数据不足则只显示剩余数据
            lastIndex = lastIndex > prepMailData.Count - 1 ? prepMailData.Count - 1 : lastIndex;
        }
        else
        {
            //上一页码:pageNum - 1
            //上一页开始索引和结束索引
            nextStart = maxMailCount * (pageNum - 1 - 1);
            nextLast = nextStart + maxMailCount - 1;
            //当上一页结束索引小于0,表示不可向上翻页
            if (nextLast < 0) 
                return false;

            //更新页码和显示索引
            pageNum--;
            startIndex = nextStart;
            lastIndex = nextLast;
            //上一页翻页应该不会出现越界
        }
        return true;
    }

    /// <summary>
    /// 根据显示索引范围显示预览信件
    /// </summary>
    public void ShowMail()
    {
        //根据数据激活对应信件
        //更新页码先隐藏之前数据
        for (int i = 0; i < mailObjs.Length; i++) 
            mailObjs[i].Hide();

        //根据数据显示预览信件
        for (int i = startIndex, j = 0; i <= lastIndex; i++, j++)  
            mailObjs[j].Show(prepMailData[i]);
    }

    /// <summary>
    /// 根据id在信箱显示信件
    /// </summary>
    /// <param name="id">信件的id</param>
    public void displayMailById(int id)
    {

    }

    /// <summary>
    /// 设置信件的附件内容[暂未实现,功能API形式]
    /// </summary>
    /// <param name="id">信件的id</param>
    /// <param name="attachId">附件的道具id</param>
    /// <param name="attachNum">附件的数量</param>
    public void setAttachById(int id,int attachId,int attachNum)
    {
        
    }

    /// <summary>
    /// 根据ID设置读者评分
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public void setScoreById(int id, int score)
    {
        //只有报社编辑类型才有读者评分
    
    }

}

