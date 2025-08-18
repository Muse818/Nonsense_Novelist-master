using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
///<summary>
///选择书本界面选中两格按钮
///</summary>
class ButtonDown : MonoBehaviour, IPointerClickHandler
{
    public bool isSelected = false;
    public static int num = 0;
    public void OnPointerClick(PointerEventData eventData)
    {

        if (!isSelected)
        {
            if (num < 2)
            {
                this.gameObject.GetComponent<Image>().color = new Color((float)255 / 255, (float)200 / 255, (float)132 / 255, (float)255 / 255);
                isSelected = true;
                num++;
            }
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            isSelected = false;
            num--;
        }
    }
}
