using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_XpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform tooltipPostion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.Instance.GetCurrentXP() > 999999)
        {
            Tooltip.instance.ShowTooltip(string.Format("Level " + GameManager.Instance.GetLevel().ToString()
            + "\n" + GameManager.Instance.GetCurrentXP().ToString("e3") + " / " + GameManager.Instance.GetMaxXP().ToString("e3") + " XP"
            + "\n" + GameManager.Instance.GetXPPercentage().ToString("N2") + "%"), tooltipPostion);
        }
        else
        {
            Tooltip.instance.ShowTooltip(string.Format("Level " + GameManager.Instance.GetLevel().ToString()
            + "\n" + GameManager.Instance.GetCurrentXP().ToString("N1") + " / " + GameManager.Instance.GetMaxXP().ToString("N1") + " XP"
            + "\n" + GameManager.Instance.GetXPPercentage().ToString("N2") + "%"), tooltipPostion);
        }
        




    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
