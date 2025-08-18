using UnityEngine.EventSystems;
using UnityEngine;

///<summary>
///
///</summary>
class MouseOverWord : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.GetChild(1).gameObject.SetActive(false);

    }
}
