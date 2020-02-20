﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IncomeType { CPS, CPC}

public class Upgrade
{
    string name;
    int id;

    double amount;
    double baseCost;
    double currentCost;

    IncomeType incomeType;

    double coinsPerClick;
    double coinsPerSecond;

    double tier;

    bool active;

    public Upgrade(string name, int id, double cost, IncomeType type, double coinsPerClick, double coinsPerSecond, double tier)
    {
        this.name = name;
        this.id = id;
        this.baseCost = cost;
        incomeType = type;
        this.coinsPerClick = coinsPerClick;
        this.coinsPerSecond = coinsPerSecond;
        this.tier = tier;

        currentCost = baseCost;

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
        currentCost = baseCost;
        currentCost = amount;
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
        return currentCost;
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

    public double GetTier()
    {
        return tier;
    }

    public double GetBaseCost()
    {
        return baseCost;
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
