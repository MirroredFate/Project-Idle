using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_InfoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.ShowTooltip("Information", tooltipPostion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
