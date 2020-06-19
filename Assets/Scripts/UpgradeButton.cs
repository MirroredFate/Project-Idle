using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    Upgrade upgrade;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] TextMeshProUGUI amountText;


    public Upgrade GetUpgrade()
    {
        return upgrade;
    }
    
    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void SetCostText(string cost)
    {
        costText.text = cost;
    }

    public void SetValueText(string value)
    {
        valueText.text = value;
    }

    public void SetAmountText(string amount)
    {
        amountText.text = amount;
    }


}
