using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    double cost;
    int amount;

    public double GetCost()
    {
        return cost;
    }

    public void SetCost(double amount)
    {
        cost = amount;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }
}
