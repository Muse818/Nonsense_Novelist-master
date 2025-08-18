using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 信件控制者,用于检测信件事件中鼠标的行为,移入移出和点击
/// </summary>
public class MailController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// 控制的信件对象
    /// </summary>
    public PreMailObj mailObj;

    //鼠标进入时的动作
    protected UnityAction enterAction;
    //鼠标退出时的动作
    protected UnityAction exitAction;
    //鼠标在其中点击时的动作
    protected UnityAction clickAction;

    //注册信件相关事件
    private void Start()
    {
        if (mailObj == null)
            return;

        /* 注册鼠标事件 */
        //鼠标进入:UI上浮动
        enterAction += () =>
        {
            mailObj.MoveTarget(mailObj.floatPos, moveDir.ToTop);
        };

        //鼠标退出:UI下浮动
        exitAction += () =>
        {
            mailObj.MoveTarget(mailObj.originPos, moveDir.ToDown);
        };

        //鼠标点击
        clickAction += () =>
        {
            /* 打开信件详情:传入MailInfo,显示信件详情,取消未读的发光显示等
            */
            mailObj.ClickAction();
        };
    }

    //鼠标进入调用
    public void OnPointerEnter(PointerEventData eventData) => enterAction?.Invoke();
    //鼠标退出调用
    public void OnPointerExit(PointerEventData eventData) => exitAction?.Invoke();
    //鼠标点击调用
    public void OnPointerClick(PointerEventData eventData) => clickAction?.Invoke();
}
