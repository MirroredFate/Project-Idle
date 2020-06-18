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
        if(GameManager.Instance.GetCoins() > 999999)
        {
            Tooltip.instance.ShowTooltip(string.Format(GameManager.Instance.GetCoins().ToString("e3") + " Coins"
                + "\n" + GameManager.Instance.GetGoldCoins().ToString("e3") + " Gold Coins"
                + "\n" + GameManager.Instance.GetCoinsPerSecond().ToString("e3")), tooltipPostion);
        }
        else
        {
            Tooltip.instance.ShowTooltip(string.Format(GameManager.Instance.GetCoins().ToString("N1") + " Coins"
                + "\n" + GameManager.Instance.GetGoldCoins().ToString("N1") + " Gold Coins"
                + "\n" + GameManager.Instance.GetCoinsPerSecond().ToString("N1") + " Coins per Second"), tooltipPostion);
        }
        




    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
