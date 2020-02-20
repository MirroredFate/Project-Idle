using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IncomeType { CPS, CPC}

public class Upgrade
{
    string name;
    int id;

    double amount;
    double cost;
    IncomeType incomeType;

    double coinsPerClick;
    double coinsPerSecond;

    bool active;

    public Upgrade(string name, int id, double cost, IncomeType type, double coinsPerClick, double coinsPerSecond)
    {
        this.name = name;
        this.id = id;
        this.cost = cost;
        incomeType = type;
        this.coinsPerClick = coinsPerClick;
        this.coinsPerSecond = coinsPerSecond;

        amount = 0;
        active = false;
    }


    #region Setters

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetAmount(double amount)
    {
        this.amount = amount;
    }

    public void SetCost(double amount)
    {
        cost = amount;
    }

    public void SetCoinsPerClick(double amount)
    {
        coinsPerClick = amount;
    }

    public void SetCoinsPerSecond(double amount)
    {
        coinsPerSecond = amount;
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    #endregion

    #region Getters


    public string GetName()
    {
        return name;
    }

    public double GetAmount()
    {
        return amount;
    }

    public double GetCost()
    {
        return cost;
    }

    public double GetCoinsPerClick()
    {
        return coinsPerClick;
    }

    public double GetCoinsPerSecond()
    {
        return coinsPerSecond;
    }

    public bool GetActive()
    {
        return active;
    }

    public int GetID()
    {
        return id;
    }

    public IncomeType GetIncomeType()
    {
        return incomeType;
    }

    #endregion

    public void IncreaseIncome(int percentage)
    {
        if (incomeType == IncomeType.CPC)
        {
            coinsPerClick = System.Math.Round(coinsPerClick * (1 + percentage * 0.01f));
        }
        else if (incomeType == IncomeType.CPS)
        {
            coinsPerSecond = System.Math.Round(coinsPerSecond * (1 + (percentage * 0.01f)));
        }
    }

}
