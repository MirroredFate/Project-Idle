using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum PowerUpType 
{ 
    Clicks, 
    Building, 
    XP, 
    Crit 
}
public enum PowerUpBuilding 
{ 
    None, 
    Farm, 
    Inn, 
    Blacksmith, 
    WarriorBarracks, 
    KnightJousts, 
    WizardTower, 
    Cathedral, 
    Citadel, 
    RoyalCastle, 
    HeavensGate,
    HallOfLegends,
    All
}

[CreateAssetMenu(fileName = "Data", menuName = "PowerUps/Create PowerUp", order = 1)]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public int id;

    public PowerUpType type;
    public PowerUpBuilding building;
    public bool hasSecondBuilding;

    [Header("Only Important if 'Has Second Building' is checked")]
    public PowerUpBuilding secondBuilding;

    [Header("Requirement")]
    public double requiredAmountOfBuilding;
    public double requiredAmountOfSecondBuilding;
    public double requiredAmountOfClicks;
    public double requiredLevel;
    public double requiredCriticalClicks;
    public double requiredTotalCoins;

    [Header("Cost")]
    public double coinCost;
    public double goldCoinCost;
    
    [Header("Increase")]
    public double increaseProductionMultiplier;
    public double increaseClickMultiplier;
    public double increaseXPMultiplier;
    public double increaseCritChanceAmount;
    public double increaseCritMultiplier;

    public bool hasBeenPurchased = false;
    public bool requirementMet = false;

    public bool CheckRequirement(Upgrade _building)
    {
        switch (type)
        {
            case PowerUpType.Clicks:
                if (GameManager.Instance.GetClicksDone() >= requiredAmountOfClicks) return true;
                break;
            case PowerUpType.Building:
                switch (building)
                {
                    case PowerUpBuilding.None:
                        break;
                    case PowerUpBuilding.Farm:
                        if(_building != null && _building.GetID() == 0)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.Inn:
                        if (_building != null && _building.GetID() == 1)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.Blacksmith:
                        if (_building != null && _building.GetID() == 2)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.WarriorBarracks:
                        if (_building != null && _building.GetID() == 3)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.KnightJousts:
                        if (_building != null && _building.GetID() == 4)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.WizardTower:
                        if (_building != null && _building.GetID() == 5)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.Cathedral:
                        if (_building != null && _building.GetID() == 6)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.Citadel:
                        if (_building != null && _building.GetID() == 7)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.RoyalCastle:
                        if (_building != null && _building.GetID() == 8)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.HeavensGate:
                        if (_building != null && _building.GetID() == 9)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.HallOfLegends:
                        if (_building != null && _building.GetID() == 10)
                        {
                            if (_building.GetAmount() >= requiredAmountOfBuilding) return true;
                        }
                        break;
                    case PowerUpBuilding.All:
                        break;
                    default:
                        break;
                }
                break;
            case PowerUpType.XP:
                if (GameManager.Instance.GetLevel() >= requiredLevel) return true;
                break;
            case PowerUpType.Crit:
                if (GameManager.Instance.GetCriticalClicksDone() >= requiredCriticalClicks) return true;
                break;
            default:
                break;
        }
        return false;
    }

    public string GetBuildingName()
    {
        switch (building)
        {
            case PowerUpBuilding.None:
                return "None";
            case PowerUpBuilding.Farm:
                return "Farms";
            case PowerUpBuilding.Inn:
                return "Inns";
            case PowerUpBuilding.Blacksmith:
                return "Blacksmiths";
            case PowerUpBuilding.WarriorBarracks:
                return "Warrior Barracks";
            case PowerUpBuilding.KnightJousts:
                return "Knight Jousts";
            case PowerUpBuilding.WizardTower:
                return "Wizard Towers";
            case PowerUpBuilding.Cathedral:
                return "Cathedral";
            case PowerUpBuilding.Citadel:
                return "Citadels";
            case PowerUpBuilding.RoyalCastle:
                return "Royal Castles";
            case PowerUpBuilding.HeavensGate:
                return "Heavens Gates";
            case PowerUpBuilding.HallOfLegends:
                return "Hall of Legends";
            case PowerUpBuilding.All:
                return "All";
            default:
                return "Farms";
        }
    }
}
