﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool admin = false;

    double level = 1;
    double maxXP = 10;

    double baseMaxXP;
    double xp = 0;
    double xpPerClick = 1;

    double coins;
    double coinsPerClick = 1;
    double coinsPerSecond = 0;

    double coinsTotalperSecond;
    double coinsTotalperClick;

    double totalFarmIncome;
    double totalInnIncome;
    double totalBlacksmithIncome;
    double totalBarracksIncome;
    double totalJoustsIncome;
    double totalTowerIncome;
    double totalCathedralIncome;
    double totalCitadelIncome;
    double totalCastleIncome;
    double totalGateIncome;
    double totalHallIncome;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (admin)
        {
            coinsPerClick = 1e100;
        }

        baseMaxXP = maxXP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Getters
    public double GetCoins()
    {
        return coins;
    }

    public double GetCoinsPerSecond()
    {
        return coinsPerSecond;
    }

    public double GetCoinsPerClick()
    {
        return coinsPerClick;
    }

    public double GetTotalCoins()
    {
        return coinsTotalperClick + coinsTotalperSecond;
    }

    public double GetTotalCoinsPerClick()
    {
        return coinsTotalperClick;
    }

    public double GetTotalCoinsPerSecond()
    {
        return coinsTotalperSecond;
    }

    public double GetLevel()
    {
        return level;
    }

    public double GetMaxXP()
    {
        return maxXP;
    }

    public double GetCurrentXP()
    {
        return xp;
    }

    public double GetXPPerClick()
    {
        return xpPerClick;
    }

    #endregion


    public void IncreaseCoinsPerSecond()
    {
        coins += coinsPerSecond;
        coinsTotalperSecond += coinsPerSecond;
    }

    public void IncreaseClickPower(double amount)
    {
        coinsPerClick += amount;
    }



    public void AddToTotalIncome(int id, double income)
    {
        switch (id)
        {
            case 0: totalFarmIncome = income;
                break;

            case 1: totalInnIncome = income;
                break;

            case 2: totalBlacksmithIncome = income;
                break;

            case 3: totalBarracksIncome = income;
                break;

            case 4: totalJoustsIncome = income;
                break;

            case 5: totalTowerIncome = income;
                break;

            case 6: totalCathedralIncome = income;
                break;

            case 7: totalCitadelIncome = income;
                break;

            case 8: totalCastleIncome = income;
                break;

            case 9: totalGateIncome = income;
                break;

            case 10: totalHallIncome = income;
                break;
        }

        coinsPerSecond = totalFarmIncome + totalInnIncome + totalBlacksmithIncome + totalBarracksIncome + totalJoustsIncome + totalTowerIncome + totalCathedralIncome + totalCitadelIncome + totalCastleIncome +
            totalGateIncome + totalHallIncome;
    }

    public void RemoveCoins(double amount)
    {
        coins -= amount;
    }

    public void IncreaseCoins()
    {
        coins += coinsPerClick;
        coinsTotalperClick += coinsPerClick;
    }

    public void LevelUp()
    {
        level++;
        if(xp > maxXP)
        {
            xp -= maxXP;
        }
        else
        {
             xp = 0;
        }
        maxXP += baseMaxXP * System.Math.Pow(1.15f, level);
        coinsPerClick += System.Math.Pow(1.15f, level);
    }

    public void XPClick()
    {
        xp += xpPerClick;
    }

    public void IncreaseXPPerClick(double upgradeTier, float increase_Percent)
    {
        float increaseAmount = 1f + (increase_Percent / 100f);

        xpPerClick += System.Math.Pow(increaseAmount, upgradeTier);
    }
}
