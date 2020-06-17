using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    Upgrade upgrade;

    [SerializeField] Text nameText;
    [SerializeField] Text costText;
    [SerializeField] Text valueText;
    [SerializeField] Text amountText;


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
