using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_Crit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.ShowTooltip(string.Format(GameManager.Instance.GetCritChance().ToString() + "% Crit Chance"
            + "\n" + GameManager.Instance.GetCritMultiplier().ToString() + "x Coins per Crit"), tooltipPostion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
