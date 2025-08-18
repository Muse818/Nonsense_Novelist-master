using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//串行化,串行化是指存储和获取磁盘文件、内存或其他地方中的对象。
[System.Serializable]
public class SaveArticle
{
    public int id;//文章的序号，从0起，每次+1
    public int rand;//文章的得分
    public string title;//文章的标题
    public List<string> content;//文章的内容
    public string reply;//系统反馈
    public bool hasRead;

 //   public List<int> livingTargetPosition = new List<int>();
	//public List<int> livingMonsterTypes = new List<int>();

	public int shootNum = 0;
	public int score = 0;
}

