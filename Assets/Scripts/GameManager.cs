using System.Collections;
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

    public void IncreaseCPS(double amount)
    {
        coinsPerSecond += amount;
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

    public void IncreaseXPPerClick(double upgradeTier)
    {
        xpPerClick += System.Math.Pow(1.15f, upgradeTier);
    }
}
