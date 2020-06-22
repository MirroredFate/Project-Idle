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

        switch (pButton.type)
        {
            case PowerUpType.Clicks:
                Tooltip.instance.ShowTooltip(string.Format(pButton.powerUpName
                    + "\n" + "Requires " + GameManager.Instance.FormatNumber(pButton.requiredAmountOfClicks) + " Clicks"
                    + "\n" + "Cost: " + GameManager.Instance.FormatNumber(pButton.coinCost) + " Coins"
                    + "\n" + "Increases your Coins per Click by " + GameManager.Instance.ConvertMultiplierInPercentString(pButton.increaseClickMultiplier)), tooltipPosition);
                break;
            case PowerUpType.Building:
                Tooltip.instance.ShowTooltip(string.Format(pButton.powerUpName
                    + "\n" + "Requires " + GameManager.Instance.FormatNumber(pButton.requiredAmountOfBuilding) + " " + pButton.GetBuildingName()
                    + "\n" + "Cost: " + GameManager.Instance.FormatNumber(pButton.coinCost) + " Coins"
                    + "\n" + "Increases the Production of your " + pButton.GetBuildingName() + " by " + GameManager.Instance.ConvertMultiplierInPercentString(pButton.increaseProductionMultiplier)), tooltipPosition);
                break;
            case PowerUpType.Crit:
                Tooltip.instance.ShowTooltip(string.Format(pButton.powerUpName
                    + "\n" + "Requires " + GameManager.Instance.FormatNumber(pButton.requiredCriticalClicks) + " Critical Clicks"
                    + "\n" + "Cost: " + GameManager.Instance.FormatNumber(pButton.coinCost) + " Coins, "+  GameManager.Instance.FormatNumber(pButton.goldCoinCost) + " Gold Coins"
                    + "\n" + "Increases your Crit Chance by " + pButton.increaseCritChanceAmount + "% and your Crit Multiplier by " + GameManager.Instance.ConvertMultiplierInPercentString(pButton.increaseCritMultiplier)), tooltipPosition);
                break;
            case PowerUpType.XP:
                Tooltip.instance.ShowTooltip(string.Format(pButton.powerUpName
                    + "\n" + "Requires Level " + GameManager.Instance.FormatNumber(pButton.requiredLevel)
                    + "\n" + "Cost: " + GameManager.Instance.FormatNumber(pButton.coinCost)
                    + "\n" + "Increases your XP per Click by " + GameManager.Instance.ConvertMultiplierInPercentString(pButton.increaseXPMultiplier)), tooltipPosition);
                break;
            default:
                break;
        }

        
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
