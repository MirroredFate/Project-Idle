using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{



    public void OnPointerEnter(PointerEventData eventData)
    {

        Upgrade uInfo = GetComponent<UpgradeButton>().GetUpgrade();

        if(uInfo.GetCurrentIncome() < 999999)
        {
            Tooltip.instance.ShowTooltip(string.Format(uInfo.GetName() + "\n"
                        + uInfo.GetCoinsPerSecond().ToString("N0") + " CPS" + "\n"
                        + uInfo.GetNextUpgradeTText()) + "\n"
                        + "Currently " + uInfo.GetCurrentIncome().ToString("N0") + " CPS");
        }
        else
        {
            Tooltip.instance.ShowTooltip(string.Format(uInfo.GetName() + "\n"
            + uInfo.GetCoinsPerSecond().ToString("e3") + " CPS" + "\n"
            + uInfo.GetNextUpgradeTText()) + "\n"
            + "Currently " + uInfo.GetCurrentIncome().ToString("e3") + " CPS");
        }

        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
