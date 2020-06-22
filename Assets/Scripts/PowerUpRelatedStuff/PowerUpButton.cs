using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    PowerUp powerUp;
    Image image;

    string buildingName;

    private void Awake()
    {
        gameObject.SetActive(false);
        GameEvents.current.onCoinChangeTrigger += UpdateButton;
    }

    private void OnDestroy()
    {
        GameEvents.current.onCoinChangeTrigger -= UpdateButton;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    #region Getter

    public PowerUp GetPowerUp()
    {
        return powerUp;
    }

    #endregion

    #region Setter

    public void SetPowerUp(PowerUp powerUp)
    {
        this.powerUp = powerUp;
    }

    public void SetImage(Sprite sprite)
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
    }

    #endregion

    void SetBuildingName()
    {
        switch (powerUp.building)
        {
            case PowerUpBuilding.None:
                buildingName = "None";
                break;
            case PowerUpBuilding.Farm:
                buildingName = "Farm";
                break;
            case PowerUpBuilding.Inn:
                buildingName = "Inn";
                break;
            case PowerUpBuilding.Blacksmith:
                buildingName = "Blacksmith";
                break;
            case PowerUpBuilding.WarriorBarracks:
                buildingName = "Warrior Barracks";
                break;
            case PowerUpBuilding.KnightJousts:
                buildingName = "Knights Jousts";
                break;
            case PowerUpBuilding.WizardTower:
                buildingName = "Wizard Tower";
                break;
            case PowerUpBuilding.Cathedral:
                buildingName = "Cathedral";
                break;
            case PowerUpBuilding.Citadel:
                buildingName = "Citadel";
                break;
            case PowerUpBuilding.RoyalCastle:
                buildingName = "Royal Castle";
                break;
            case PowerUpBuilding.HeavensGate:
                buildingName = "Heaven's Gate";
                break;
            case PowerUpBuilding.HallOfLegends:
                buildingName = "Hall of Legends";
                break;
            case PowerUpBuilding.All:
                buildingName = "All";
                break;
            default:
                break;
        }
    }

    public void ApplyPowerUp()
    {
        SetBuildingName();
        if (!PowerUpManager.Instance.GetBoughtPowerUpNames().Contains(powerUp.powerUpName))
        {
            switch (powerUp.type)
            {
                case PowerUpType.Clicks:
                    GameManager.Instance.IncreaseCoinsPerClick(powerUp.increaseClickMultiplier);
                    break;
                case PowerUpType.Building:
                    if (buildingName == "All")
                    {
                        foreach (Button buildings in UICollector.Instance.upgradeButtonList)
                        {
                            Upgrade building = buildings.GetComponent<UpgradeButton>().GetUpgrade();

                            building.IncreaseIncome(powerUp.increaseProductionMultiplier);
                        }
                    }
                    else if (buildingName == "None")
                    {

                    }
                    else
                    {
                        for (int i = 0; i < UICollector.Instance.upgradeButtonList.Count; i++)
                        {
                            Debug.Log("CHECKING NAME FOR POWERUP FOR " + buildingName);
                            if (UICollector.Instance.upgradeButtonList[i].GetComponent<UpgradeButton>().GetUpgrade().GetName() == buildingName)
                            {
                                Debug.Log("POWER UP: INCREASE PRODUCTION");
                                UICollector.Instance.upgradeButtonList[i].GetComponent<UpgradeButton>().GetUpgrade().IncreaseIncome(powerUp.increaseProductionMultiplier);
                            }
                        }
                    }
                    break;
                case PowerUpType.XP:
                    GameManager.Instance.IncreaseXPPerClick(powerUp.increaseXPMultiplier);
                    break;
                case PowerUpType.Crit:
                    GameManager.Instance.IncreaseCrit((float)powerUp.increaseCritChanceAmount);
                    GameManager.Instance.IncreaseCritMultiplierX(powerUp.increaseCritMultiplier);
                    break;
                default:
                    break;
            }

            powerUp.hasBeenPurchased = true;

            PowerUpManager.Instance.AddToBoughtPowerUps(powerUp);
            gameObject.SetActive(false);
            UICollector.Instance.powerUpButtons.Remove(GetComponent<Button>());
            Tooltip.instance.HideTooltip();
            Destroy(gameObject);
        }
        

        
    }

    public void UpdateButton()
    {
        if(GameManager.Instance.GetCoins() >= powerUp.coinCost && !PowerUpManager.Instance.GetBoughtPowerUpNames().Contains(powerUp.powerUpName))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

   

   

}
