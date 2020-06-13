using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Admin")]
    public bool admin = false;

    [Header("XP / Level stuff")]
    [SerializeField]double level = 1;
    [SerializeField] double maxXP = 10;

    [SerializeField] double baseMaxXP;
    [SerializeField] double xp = 0;
    [SerializeField] double xpPerClick = 1;

    [Header("Coins")]
    [SerializeField] double coins;
    [SerializeField] double coinsPerClick = 1;
    [SerializeField] double coinsPerSecond = 0;

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

    public double GetClicksDone()
    {
        return clicksDone;
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


}
