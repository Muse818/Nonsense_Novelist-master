/// <summary>
/// 作用范围类型（具体实现在UI脚本判断此字段）
/// </summary>
public enum ChooseWayEnum
{ 
    /// <summary>无目标（如全屏法术）</summary>
   allChoose=0,
    /// <summary>指向型技能，需要选单位</summary>
    canChoose = 1,
    /// <summary>非指向型技能，范围选择</summary>
    autoChoose = 2,

}
