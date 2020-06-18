using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Admin")]
    public bool admin = false;

    [Header("XP / Level stuff")]
    [SerializeField] double level = 1;
    [SerializeField] double maxXP = 10;

    [SerializeField] double baseMaxXP;
    [SerializeField] double xp = 0;
    [SerializeField] double xpPerClick = 1;

    [Header("Coins")]
    [SerializeField] double coins;
    [SerializeField] double coinsPerClick = 1;
    [SerializeField] double coinsPerSecond = 0;
    [SerializeField] float critChance = 2;
    [SerializeField] double critMultiplier = 10;
    [SerializeField] bool didCrit = false;

    [SerializeField] double coinsTotalperSecond;
    [SerializeField] double coinsTotalperClick;

    [Header("Total Income of Buildings")]
    [SerializeField] double totalFarmIncome;
    [SerializeField] double totalInnIncome;
    [SerializeField] double totalBlacksmithIncome;
    [SerializeField] double totalBarracksIncome;
    [SerializeField] double totalJoustsIncome;
    [SerializeField] double totalTowerIncome;
    [SerializeField] double totalCathedralIncome;
    [SerializeField] double totalCitadelIncome;
    [SerializeField] double totalCastleIncome;
    [SerializeField] double totalGateIncome;
    [SerializeField] double totalHallIncome;

    [Header("Random Info Stuff")]
    [SerializeField] double clicksDone;
    [SerializeField] double criticalClicksDone;
    [SerializeField] double goldCoins;
    [SerializeField] int unluckCounter;

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
        Tooltip.instance.HideTooltip();
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
        if (didCrit)
        {
            return coinsPerClick * critMultiplier;
        }
        else
        {
            return coinsPerClick;

        }
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

    public double GetClicksDone()
    {
        return clicksDone;
    }

    public double GetCriticalClicksDone()
    {
        return criticalClicksDone;
    }

    public float GetCritChance()
    {
        return critChance;
    }

    public bool GetDidCrit()
    {
        return didCrit;
    }

    public double GetBaseMaxXP()
    {
        return baseMaxXP;
    }

    public double GetCritMultiplier()
    {
        return critMultiplier;
    }

    public double GetCoinsTotalPerSecond()
    {
        return coinsTotalperSecond;
    }

    public double GetCoinsTotalPerClick()
    {
        return coinsTotalperClick;
    }

    public double GetFarmIncome()
    {
        return totalFarmIncome;
    }
    public double GetInnIncome()
    {
        return totalInnIncome;
    }
    public double GetBlacksmithIncome()
    {
        return totalBlacksmithIncome;
    }
    public double GetBarracksIncome()
    {
        return totalBarracksIncome;
    }
    public double GetJoustsIncome()
    {
        return totalJoustsIncome;
    }
    public double GetTowerIncome()
    {
        return totalTowerIncome;
    }
    public double GetCathedralIncome()
    {
        return totalCathedralIncome;
    }
    public double GetCitadelIncome()
    {
        return totalCitadelIncome;
    }
    public double GetCastleIncome()
    {
        return totalCastleIncome;
    }
    public double GetGateIncome()
    {
        return totalGateIncome;
    }
    public double GetHallIncome()
    {
        return totalHallIncome;
    }

    public int GetUnluckCounter()
    {
        return unluckCounter;
    }

    public double GetGoldCoins()
    {
        return goldCoins;
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

    public void IncreaseClicksDone()
    {
        clicksDone++;
    }

    public void IncreaseCriticalClicksDone()
    {
        criticalClicksDone++;
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

    public void RemoveGoldCoins(double amount)
    {
        goldCoins -= amount;
    }

    public void IncreaseCoins()
    {
        CalculateCrit();
        if (didCrit)
        {
            coins += coinsPerClick * critMultiplier;
            coinsTotalperClick += coinsPerClick * critMultiplier;
            goldCoins++;
        }
        else
        {
            coins += coinsPerClick;
            coinsTotalperClick += coinsPerClick;
        }
        
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
        //coinsPerClick += System.Math.Pow(1.15f, level*0.5f);
        IncreaseCoinsPerClick(level * 0.5, 15);
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

    public void IncreaseCoinsPerClick(double basedOn, float increase_Percent)
    {
        float increaseAmount = 1f + (increase_Percent / 100f);

        coinsPerClick += System.Math.Pow(increaseAmount, basedOn);
    }

    public void IncreaseCrit(float amount)
    {
        if(critChance + amount > 100f)
        {
            critChance = 100;
        }
        else
        {
            critChance += amount;
        }
    }

    public void IncreaseCritMultiplier(double amount)
    {
        critMultiplier += amount;
    }

    public void SetCritBool(bool crit)
    {
        didCrit = crit;
    }

    public float GetXPPercentage()
    {
        return ((float)xp / (float)maxXP) * 100f;
    }

    void CalculateCrit()
    {
        float rnd = Random.Range(1f, 100f);
        if (rnd <= critChance || unluckCounter >= (100/critChance))
        {
            didCrit = true;
            unluckCounter = 0;
        }
        else
        {
            unluckCounter++;
            didCrit = false;
        }

    }






    #region Save/Load

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();

        level = data.level;
        maxXP = data.maxXp;
        baseMaxXP = data.baseMaxXP;
        xp = data.xp;
        xpPerClick = data.xpPerClick;
        coins = data.coins;
        coinsPerClick = data.coinsPerClick;
        coinsPerSecond = data.coinsPerSecond;
        critMultiplier = data.critMultiplier;
        critChance = data.critChance;
        coinsTotalperSecond = data.coinsTotalPerSecond;
        coinsTotalperClick = data.coinsTotalPerClick;
        totalFarmIncome = data.farmIncome;
        totalInnIncome = data.innIncome;
        totalBlacksmithIncome = data.blacksmithIncome;
        totalBarracksIncome = data.barracksIncome;
        totalJoustsIncome = data.joustsIncome;
        totalTowerIncome = data.towerIncome;
        totalCathedralIncome = data.cathedralIncome;
        totalCitadelIncome = data.citadelIncome;
        totalCastleIncome = data.castleIncome;
        totalGateIncome = data.gateIncome;
        totalHallIncome = data.hallIncome;
        clicksDone = data.clicksDone;
        unluckCounter = data.unluckCounter;

        for (int i = 0; i < UpgradeManager.instance.GetUpgradeList().Count - 1; i++)
        {
            UpgradeManager.instance.GetUpgradeList()[i].SetName(data.upgradeNames[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetAmount(data.amount[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetCost(data.currentCost[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetCoinsPerSecond(data.upgradeCoinsPerSecond[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetCoinsPerClick(data.upgradeCoinsPerClick[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetCurrentIncome(data.currentIncome[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetTier(data.tier[i]);
            UpgradeManager.instance.GetUpgradeList()[i].SetActive(data.active[i]);
        }


    }

    #endregion



}
