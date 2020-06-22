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


        Tooltip.instance.ShowTooltip(string.Format(uInfo.GetName() + "\n"
                        + GameManager.Instance.FormatNumber(uInfo.GetCoinsPerSecond()) + " CPS" + "\n"
                        + uInfo.GetNextUpgradeTText()) + "\n"
                        + "Currently " + GameManager.Instance.FormatNumber(uInfo.GetCurrentIncome()) +  " CPS", tooltipPostion);



    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
