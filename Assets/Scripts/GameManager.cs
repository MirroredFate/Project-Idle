using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool admin = false;

    double coins;
    double coinsPerClick = 1;
    double coinsPerSecond = 0;

    double coinsTotalperSecond;
    double coinsTotalperClick;

    // Start is called before the first frame update
    void Start()
    {
        if (admin)
        {
            coinsPerClick = 1e100;
        }
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
}
