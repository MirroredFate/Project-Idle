using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_PowerUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform tooltipPosition;

    public void SetToolTipPosition(Transform tooltipPosition)
    {
        this.tooltipPosition = tooltipPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PowerUp pButton = GetComponent<PowerUpButton>().GetPowerUp();

        Tooltip.instance.ShowTooltip(string.Format(pButton.powerUpName + 
            "\n" + "Cost: " + GameManager.Instance.FormatNumber(pButton.coinCost) + " Coins"), tooltipPosition);
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
