using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.ShowTooltip("Menu", tooltipPostion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
