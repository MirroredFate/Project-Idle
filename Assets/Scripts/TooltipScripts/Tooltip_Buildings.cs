using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip_Buildings : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;


    public void OnPointerEnter(PointerEventData eventData)
    {

        Upgrade uInfo = GetComponent<UpgradeButton>().GetUpgrade();

        if(uInfo.GetCurrentIncome() < 999999)
        {
            Tooltip.instance.ShowTooltip(string.Format(uInfo.GetName() + "\n"
                        + uInfo.GetCoinsPerSecond().ToString("N1") + " CPS" + "\n"
                        + uInfo.GetNextUpgradeTText()) + "\n"
                        + "Currently " + uInfo.GetCurrentIncome().ToString("N1") + " CPS", tooltipPostion);
        }
        else
        {
            Tooltip.instance.ShowTooltip(string.Format(uInfo.GetName() + "\n"
            + uInfo.GetCoinsPerSecond().ToString("e3") + " CPS" + "\n"
            + uInfo.GetNextUpgradeTText()) + "\n"
            + "Currently " + uInfo.GetCurrentIncome().ToString("e3") + " CPS", tooltipPostion);
        }

        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
