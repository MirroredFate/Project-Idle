using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [SerializeField] List<PowerUp> powerUps;
    [SerializeField] Transform tooltip;

    List<string> boughtPowerUpNames;

    private void Awake()
    {
        Instance = this;
        boughtPowerUpNames = new List<string>();
        GameEvents.current.onCoinChangeTrigger += CheckRequirements;
        GameEvents.current.onXPChange += CheckRequirements;
    }

    public List<PowerUp> GetPowerUps()
    {
        return powerUps;
    }

    public List<string> GetBoughtPowerUpNames()
    {
        return boughtPowerUpNames;
    }

    public void AddToBoughtPowerUps(PowerUp pUp)
    {
        boughtPowerUpNames.Add(pUp.powerUpName);
    }

    public void AddToBoughtPowerUps(string name)
    {
        boughtPowerUpNames.Add(name);
    }

    public void SetBoughtPowerUpList(List<string> list)
    {
        boughtPowerUpNames = list;
    }


    public void CheckRequirements(Upgrade building)
    {
        foreach (Button pUpButton in UICollector.Instance.powerUpButtons)
        {
            PowerUpButton powerUpButton = pUpButton.GetComponent<PowerUpButton>();
            if (powerUpButton.GetPowerUp().hasBeenPurchased == false)
            {
                if (powerUpButton.GetPowerUp().CheckRequirement(building))
                {
                    powerUpButton.GetPowerUp().requirementMet = true;
                    powerUpButton.transform.gameObject.SetActive(true);
                    pUpButton.GetComponent<ToolTip_PowerUp>().SetToolTipPosition(tooltip);
                }
            }
        }
            
    }

    public void CheckRequirements()
    {
        foreach (Button pUpButton in UICollector.Instance.powerUpButtons)
        {
            PowerUpButton powerUpButton = pUpButton.GetComponent<PowerUpButton>();
            if (powerUpButton.GetPowerUp().hasBeenPurchased == false)
            {
                if (powerUpButton.GetPowerUp().CheckRequirement(null))
                {
                    powerUpButton.GetPowerUp().requirementMet = true;
                    powerUpButton.transform.gameObject.SetActive(true);
                    pUpButton.GetComponent<ToolTip_PowerUp>().SetToolTipPosition(tooltip);
                }
            }

        }
    }

    public bool CheckForBought(string name)
    {
        if (boughtPowerUpNames.Contains(name)) return true;
        else return false;
    }


}
