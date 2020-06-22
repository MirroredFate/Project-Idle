using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_Coins : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.ShowTooltip(string.Format(GameManager.Instance.FormatNumber(GameManager.Instance.GetCoins()) + " Coins"
                + "\n" + GameManager.Instance.FormatNumber(GameManager.Instance.GetGoldCoins()) + " Gold Coins"
                + "\n" + GameManager.Instance.FormatNumber(GameManager.Instance.GetCoinsPerSecond()) + " Coins per Second"), tooltipPostion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
