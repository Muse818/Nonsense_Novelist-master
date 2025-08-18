using System;
using static MailTable;
using UnityEngine;

/// <summary>
/// 信件信息类
/// 配置格式{信件id,信件作者,信件称呼,信件内容}
/// </summary>
public class MailInfo 
{
    /* 静态数据 */
    //信件名称
    public string mailName;
    //发件人类型:区分发件人,同一发件人类型可能以不同称呼出现
    public E_MailAutherType autherType;
    //发件人实际显示姓名
    public string autherName;
    //称呼内容:发件人对收件人的称呼
    public string dear;
    //信件内容
    public string mailBody;
    //附件id
    public int attachId;
    //附件数量
    public int attachNum;

    /* 动态数据 */
    //动态id:用于动态信封的
    public int dId;
    //信件序号:此信件对应的信的id
    public int id;
    //是否已读
    public bool isRead;
    //是否显示:此时信件是否载入信箱
    public bool isDisplay;
    //信件读者评分
    public int score;
    //附件是否已经被领取(拿出)
    public bool attachIsTake;

    public MailInfo(MailTable.Data data)
    {
        this.id = data.id;
        this.mailName = data.mailName;
        this.autherType = data.autherType;
        this.autherName = data.autherName;
        this.dear = data.dear;
        this.mailBody = data.mailBody;
        this.attachId = data.attachId;
        this.attachNum = data.attachNum;
    }

    public MailInfo(int id) 
    {
        this.id = id;
        Data data = MailTable.Find(id);
        if (data != null)
        {
           this.mailName = data.mailName;
           this.autherType = data.autherType;
           this.autherName = data.autherName;
           this.dear = data.dear;
           this.mailBody = data.mailBody;
           this.attachId = data.attachId;
           this.attachNum = data.attachNum;
        }
    }

    /// <summary>
    /// 仅初始化auther字段的信件[测试阶段使用]
    /// </summary>
    /// <param name="auther"></param>
    [Obsolete("MailInfo中仅初始化信件类型的方法,该方法仅限于测试使用")]
    public MailInfo(E_MailAutherType auther)
    {
        this.autherType = auther;
    }
}
