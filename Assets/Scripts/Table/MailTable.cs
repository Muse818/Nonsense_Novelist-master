using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting;

public class MailTable : MapTable<int, MailTable.Data>
{
    /// <summary>
    /// 信件作者类型
    /// </summary>
    public enum E_MailAutherType
    {
        /// <summary>
        /// 默认信息,空的邮件作者类型
        /// </summary>
        Default,
        /// <summary>
        /// 报社编辑
        /// </summary>
        BaoShe,
        /// <summary>
        /// 安德鲁医生
        /// </summary>
        KeLao,
        /// <summary>
        /// 忠实粉丝彼得
        /// </summary>
        BiDe,
        /// <summary>
        /// 文特森
        /// </summary>
        WenTeCen,
    }

    /// <summary>
    /// 信件数据类:静态数据
    /// </summary>
    public class Data
    {
        public int id;
        //信件名称
        public string mailName;
        //发件人类型:因同一发件人可能以不同称呼出现,故区分
        public E_MailAutherType autherType;
        //尾款:发件人实际显示的称呼
        public string autherName;
        //称呼内容:信件头
        public string dear;
        //信件内容
        public string mailBody;
        //附件id
        public int attachId;
        //附件数量
        public int attachNum;
    }

    public override string AssetName => "MailData";

    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        //序列化数据为Data
        Data data = new Data();
        data.id = reader.Read<int>();
        data.mailName = reader.Read<string>();
        data.autherType = (E_MailAutherType)reader.Read<int>();
        data.autherName = reader.Read<string>();
        data.dear = reader.Read<string>();
        data.mailBody = reader.Read<string>();
        data.attachId = reader.Read<int>();
        data.attachNum = reader.Read<int>();

        //送回数据
        return new KeyValuePair<int, Data>(data.id, data);
    }


}
