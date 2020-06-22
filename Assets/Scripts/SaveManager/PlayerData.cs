using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //GameManager stuff
    public double level;
    public double maxXp;

    public double baseMaxXP;
    public double xp;
    public double xpPerClick;

    public double coins;
    public double coinsPerClick;
    public double coinsPerSecond;
    public double critMultiplier;
    public float critChance;

    public double coinsTotalPerSecond;
    public double coinsTotalPerClick;


    public double farmIncome;
    public double innIncome;
    public double blacksmithIncome;
    public double barracksIncome;
    public double joustsIncome;
    public double towerIncome;
    public double cathedralIncome;
    public double citadelIncome;
    public double castleIncome;
    public double gateIncome;
    public double hallIncome;

    public double clicksDone;
    public double criticalClicksDone;
    public int unluckCounter;

    //Data for each Building
    public string[] upgradeNames;
    public double[] amount;
    public double[] baseCost;
    public double[] currentCost;
    public double[] upgradeCoinsPerClick;
    public double[] upgradeCoinsPerSecond;
    public double[] currentIncome;
    public double[] tier;
    public bool[] active;

    //Data for each PowerUp
    public List<string> powerUpNameList;

    public PlayerData()
    {
        level = GameManager.Instance.GetLevel();
        maxXp = GameManager.Instance.GetMaxXP();
        baseMaxXP = GameManager.Instance.GetBaseMaxXP();
        xp = GameManager.Instance.GetCurrentXP();
        xpPerClick = GameManager.Instance.GetXPPerClick();
        coins = GameManager.Instance.GetCoins();
        coinsPerClick = GameManager.Instance.GetCoinsPerClick();
        coinsPerSecond = GameManager.Instance.GetCoinsPerSecond();
        critMultiplier = GameManager.Instance.GetCritMultiplier();
        critChance = GameManager.Instance.GetCritChance();
        coinsTotalPerSecond = GameManager.Instance.GetTotalCoinsPerSecond();
        coinsTotalPerClick = GameManager.Instance.GetTotalCoinsPerClick();
        farmIncome = GameManager.Instance.GetFarmIncome();
        innIncome = GameManager.Instance.GetInnIncome();
        blacksmithIncome = GameManager.Instance.GetBlacksmithIncome();
        barracksIncome = GameManager.Instance.GetBarracksIncome();
        joustsIncome = GameManager.Instance.GetJoustsIncome();
        towerIncome = GameManager.Instance.GetTowerIncome();
        cathedralIncome = GameManager.Instance.GetCathedralIncome();
        citadelIncome = GameManager.Instance.GetCitadelIncome();
        castleIncome = GameManager.Instance.GetCastleIncome();
        gateIncome = GameManager.Instance.GetGateIncome();
        hallIncome = GameManager.Instance.GetHallIncome();
        clicksDone = GameManager.Instance.GetClicksDone();
        criticalClicksDone = GameManager.Instance.GetCriticalClicksDone();
        unluckCounter = GameManager.Instance.GetUnluckCounter();

        upgradeNames = new string[11];
        amount = new double[11];
        baseCost = new double[11];
        currentCost = new double[11];
        upgradeCoinsPerClick = new double[11];
        upgradeCoinsPerSecond = new double[11];
        currentIncome = new double[11];
        tier = new double[11];
        active = new bool[11];

        if(UpgradeManager.instance.GetUpgradeList().Count > 0)
        {
            for (int i = 0; i <= UpgradeManager.instance.GetUpgradeList().Count - 1; i++)
            {
                upgradeNames[i] = UpgradeManager.instance.GetUpgradeList()[i].GetName();
                amount[i] = UpgradeManager.instance.GetUpgradeList()[i].GetAmount();
                currentCost[i] = UpgradeManager.instance.GetUpgradeList()[i].GetCost();
                upgradeCoinsPerClick[i] = UpgradeManager.instance.GetUpgradeList()[i].GetCoinsPerClick();
                upgradeCoinsPerSecond[i] = UpgradeManager.instance.GetUpgradeList()[i].GetCoinsPerSecond();
                currentIncome[i] = UpgradeManager.instance.GetUpgradeList()[i].GetCurrentIncome();
                tier[i] = UpgradeManager.instance.GetUpgradeList()[i].GetTier();
                active[i] = UpgradeManager.instance.GetUpgradeList()[i].GetActive();
            }
        }


        powerUpNameList = new List<string>();

        Debug.Log("Saving Bought Power Ups.... Count:" + PowerUpManager.Instance.GetBoughtPowerUpNames().Count);
        if(PowerUpManager.Instance.GetBoughtPowerUpNames().Count > 0)
        {
            for (int i = 0; i <= PowerUpManager.Instance.GetBoughtPowerUpNames().Count-1; i++)
            {
                powerUpNameList.Add(PowerUpManager.Instance.GetBoughtPowerUpNames()[i]);
            }
        }

        

    }
}
