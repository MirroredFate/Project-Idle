﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] UICollector uICollector;

    float timePassed;
    static bool quitComfirmation = false;


    // Start is called before the first frame update
    void Start()
    {
        uICollector.clickButton.onClick.AddListener(() => StartCoroutine(OnClickText(Input.mousePosition, uICollector.clickButton.transform, false)));
        uICollector.infoButton.onClick.AddListener(() => ShowHideInfoPanel());
        uICollector.menuButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.continueButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.saveButton.onClick.AddListener(() => SaveGame());
        uICollector.loadButton.onClick.AddListener(() => LoadGame());
        uICollector.exitButton.onClick.AddListener(() => ExitGame());
        uICollector.saveQuitButton.onClick.AddListener(() => SaveAndQuit());
        uICollector.noSaveQuitButton.onClick.AddListener(() => NoSaveButQuit());
        uICollector.cancelButton.onClick.AddListener(() => QuitCancel());
        uICollector.amountButton.onClick.AddListener(() => BuyAmountButtonClick());
        GameEvents.current.onCoinChangeTrigger += UpdateCoinsInfo;
        GameEvents.current.onXPChange += UpdateXPInfo;


        uICollector.upgradeButtonList = new List<Button>();
        UpgradeManager.instance.InitializeUpgrades();
        InstantiateUpgradeButtons();
        InstantiatePowerUpButtons();
        GameEvents.current.CoinChangeTrigger();
        GameEvents.current.XPChange();
    }

    // Update is called once per frame
    void Update()
    {

        #region Coins Per Second Text Fade Update

        timePassed += Time.deltaTime;

        if(timePassed >= 1f)
        {
            if(GameManager.Instance.GetCoinsPerSecond() > 0)
            {
                StartCoroutine(OnClickText(uICollector.autoClicker.transform.position, uICollector.autoClicker.transform, true));
            }
            timePassed = 0;
        }
        #endregion

        #region BuyAmount Button Update

        TextMeshProUGUI text = uICollector.amountButton.GetComponentInChildren<TextMeshProUGUI>();
        text.text = uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmountText();

        #endregion
    }

    #region Upgrade Related Stuff

    void InstantiatePowerUpButtons()
    {
        if (PowerUpManager.Instance.GetPowerUps() != null && PowerUpManager.Instance.GetPowerUps().Count >= 1)
        {
            if(UICollector.Instance.powerUpParent.transform.childCount > 0)
            {
                foreach (Transform child in UICollector.Instance.powerUpParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            UICollector.Instance.powerUpButtons.Clear();
            for (int i = 0; i < PowerUpManager.Instance.GetPowerUps().Count; i++)
            {
                foreach (PowerUp item in PowerUpManager.Instance.GetPowerUps())
                {
                    item.requirementMet = false;
                    item.hasBeenPurchased = false;
                }
                Button powerUpButton = Instantiate(UICollector.Instance.powerUpButtonPrefab, UICollector.Instance.powerUpParent.transform);


                powerUpButton.onClick.RemoveAllListeners();
                powerUpButton.GetComponent<PowerUpButton>().SetPowerUp(PowerUpManager.Instance.GetPowerUps()[i]);
                powerUpButton.onClick.AddListener(() => BuyPowerUp(powerUpButton.GetComponent<PowerUpButton>()));

                PowerUp pUp = powerUpButton.GetComponent<PowerUpButton>().GetPowerUp();

                switch (pUp.type)
                {
                    case PowerUpType.Clicks:
                        powerUpButton.GetComponent<PowerUpButton>().SetImage(UICollector.Instance.clickPowerUpImage);
                        break;
                    case PowerUpType.Building:
                        powerUpButton.GetComponent<PowerUpButton>().SetImage(UICollector.Instance.buildingPowerUpImage);
                        break;
                    case PowerUpType.XP:
                        powerUpButton.GetComponent<PowerUpButton>().SetImage(UICollector.Instance.xpPowerUpImage);
                        break;
                    case PowerUpType.Crit:
                        powerUpButton.GetComponent<PowerUpButton>().SetImage(UICollector.Instance.critPowerUpImage);
                        break;
                    default:
                        break;
                }

                if (PowerUpManager.Instance.CheckForBought(powerUpButton.GetComponent<PowerUpButton>().GetPowerUp().powerUpName))
                {
                    Destroy(powerUpButton.gameObject);
                }
                else
                {
                    UICollector.Instance.powerUpButtons.Add(powerUpButton);
                }

            }

        }
    }

    void InstantiateUpgradeButtons()
    {
        if(UpgradeManager.instance.GetUpgradeList() != null && UpgradeManager.instance.GetUpgradeList().Count > 0)
        {
            //Debug.Log("UpgradeList isn't null");
            if(uICollector.upgradePanel.transform.childCount > 0)
            {
                foreach (Transform child in uICollector.upgradePanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

            }

            uICollector.upgradeButtonList.Clear();
            Debug.Log("Buildings: " + UpgradeManager.instance.GetUpgradeList().Count);
            for (int i = 0; i < UpgradeManager.instance.GetUpgradeList().Count; i++)
            {
                Button upgradeButton = Instantiate(uICollector.upgradeButtonPrefab, uICollector.upgradePanel.transform);
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.GetComponent<UpgradeButton>().SetUpgrade(UpgradeManager.instance.GetUpgradeList()[i]);
                upgradeButton.onClick.AddListener(() => BuyUpgrade(upgradeButton.GetComponent<UpgradeButton>()));

                TextMeshProUGUI uptext = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();

                UpgradeButton upBtn = upgradeButton.GetComponent<UpgradeButton>();
                Upgrade upgrade = upBtn.GetUpgrade();

                upBtn.SetNameText(upgrade.GetName());
                upBtn.SetCostText(string.Format(GameManager.Instance.FormatNumber(upgrade.GetCost()) + "<sprite=0>"));
                upBtn.SetValueText(string.Format(upgrade.GetCurrentIncomePercentage().ToString("N2") + "%"));
                upBtn.SetAmountText(upgrade.GetAmount().ToString());


                uICollector.upgradeButtonList.Add(upgradeButton);

            }

        }
        
    }

    void BuyUpgrade(UpgradeButton upgradeButton)
    {
        Upgrade upgrade = upgradeButton.GetUpgrade();
        BuyAmountButtonBehaviour amountBtn = uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>();

        if(amountBtn.GetAmount() <= 1)
        {
            if(upgrade.GetCost() <= GameManager.Instance.GetCoins())
            {
                GameManager.Instance.RemoveCoins(upgrade.GetCost());
                GameEvents.current.CoinChangeTrigger();
                double amountCalc = (upgrade.GetAmount() + 1) % 5;

                if (amountCalc == 0)
                {
                    upgrade.IncreaseIncome(15f);
                    Debug.Log("Bonus, Income now at: " + upgrade.GetCurrentIncome());
                    GameManager.Instance.IncreaseXPPerClick(upgrade.GetTier(), 25);
                }
            }
        }
        else
        {
            if (CalcCost(upgrade, amountBtn.GetAmount()) <= GameManager.Instance.GetCoins())
            {
                GameManager.Instance.RemoveCoins(CalcCost(upgrade, amountBtn.GetAmount()));
                GameEvents.current.CoinChangeTrigger();

                Upgrade tempUpgrade = new Upgrade("temp", 0, upgrade.GetCost(), IncomeType.CPS, 0, upgrade.GetCoinsPerSecond(), 99);
                for (int i = 0; i < amountBtn.GetAmount() - 1; i++)
                {
                    double amountCalc = (tempUpgrade.GetAmount()) % 5;
                    tempUpgrade.SetAmount(tempUpgrade.GetAmount() + 1);

                    if (amountCalc == 0)
                    {
                        Debug.Log("increase XP per clicK!");
                        upgrade.IncreaseIncome(15f);
                        GameManager.Instance.IncreaseXPPerClick(upgrade.GetTier(), 25);
                    }
                    //tempUpgrade.SetCost(tempUpgrade.GetBaseCost() * System.Math.Pow(1.15f, tempUpgrade.GetAmount()));
                }

                //upgrade.SetCost(tempUpgrade.GetCost());
                //upgrade.SetCoinsPerSecond(tempUpgrade.GetCoinsPerSecond());
            }
        }

        Debug.Log("Current Amount: " + (upgrade.GetAmount() + uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()));

        upgrade.SetAmount(upgrade.GetAmount() + uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount());
        Debug.Log("Current Income for " + upgrade.GetName() + " is " + upgrade.GetCurrentIncome());



        if (upgrade.GetIncomeType() == IncomeType.CPC)
        {
            GameManager.Instance.IncreaseClickPower(upgrade.GetCoinsPerClick());
        }
        else
        {
            GameManager.Instance.AddToTotalIncome(upgrade.GetID(), upgrade.GetCurrentIncome());
            Debug.Log("ID: " + upgrade.GetID());
        }

        upgrade.SetActive(true);

        Debug.Log("BaseCost: " + upgrade.GetBaseCost());
        upgrade.SetCost(upgrade.GetBaseCost() * System.Math.Pow(1.15f, upgrade.GetAmount()));
        GameEvents.current.CoinChangeTrigger();
        UpdateUpgradeButtons();
        PowerUpManager.Instance.CheckRequirements(upgrade);
    }

    void BuyPowerUp(PowerUpButton powerUpButton)
    {
        PowerUp pUp = powerUpButton.GetPowerUp();

        if(pUp.coinCost <= GameManager.Instance.GetCoins())
            {
                GameManager.Instance.RemoveCoins(pUp.coinCost);
                GameEvents.current.CoinChangeTrigger();
                powerUpButton.ApplyPowerUp();
            }
    }

    void UpdateUpgradeButtons()
    {
        if (uICollector.upgradeButtonList.Count > 0)
        {
            foreach (Button button in uICollector.upgradeButtonList)
            {

                Text uptext = button.GetComponentInChildren<Text>();
                UpgradeButton upBtn = button.GetComponent<UpgradeButton>();
                Upgrade upgrade = button.GetComponent<UpgradeButton>().GetUpgrade();

                if (uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount() > 1)
                {
                    
                    upBtn.SetNameText(upgrade.GetName());
                    upBtn.SetCostText(uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmountText() + GameManager.Instance.FormatNumber(CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount())) + "<sprite=0>");
                    upBtn.SetValueText(string.Format(upgrade.GetCurrentIncomePercentage().ToString("N2") + "%"));
                    upBtn.SetAmountText(upgrade.GetAmount().ToString());

                    if (CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()) <= GameManager.Instance.GetCoins())
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
                else
                {
                    upBtn.SetNameText(upgrade.GetName());
                    upBtn.SetCostText(string.Format(GameManager.Instance.FormatNumber(upgrade.GetCost()) + "<sprite=0>"));
                    upBtn.SetValueText(string.Format(upgrade.GetCurrentIncomePercentage().ToString("N2") + "%"));
                    upBtn.SetAmountText(upgrade.GetAmount().ToString());

                    if (upgrade.GetCost() <= GameManager.Instance.GetCoins())
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }

                }
            }
        }
    }


    #endregion

    #region Coroutines
    IEnumerator OnClickText(Vector3 position, Transform parent, bool autoClick)
    {
        GameObject text = Instantiate(uICollector.clickText, new Vector2(position.x, position.y), Quaternion.identity);
        text.transform.SetParent(parent);
        TextMeshProUGUI clickText = text.GetComponent<TextMeshProUGUI>();
        


        GameObject xpText = Instantiate(uICollector.clickText, new Vector2(position.x, position.y), Quaternion.identity);
        xpText.transform.SetParent(parent);
        TextMeshProUGUI clickXPText = xpText.GetComponent<TextMeshProUGUI>();

        


        if (autoClick)
        {

            GameManager.Instance.IncreaseCoinsPerSecond();

            clickText.text = "+" + string.Format(GameManager.Instance.FormatNumber(GameManager.Instance.GetCoinsPerSecond()) + "<sprite=0>");

            GameEvents.current.CoinChangeTrigger();

        }
        else
        {
            GameManager.Instance.IncreaseCoins();
            GameManager.Instance.XPClick();
            GameEvents.current.XPChange();
            if (GameManager.Instance.GetDidCrit())
            {
                GameManager.Instance.IncreaseCriticalClicksDone();
            }
            GameManager.Instance.IncreaseClicksDone();

            if (GameManager.Instance.GetDidCrit())
            {
                clickText.text = "CRIT!+" + string.Format(GameManager.Instance.FormatNumber(GameManager.Instance.GetCoinsPerClick()) + "<sprite=0>");
                GameEvents.current.CoinChangeTrigger();
            }
            else
            {
                clickText.text = "+" + string.Format(GameManager.Instance.FormatNumber(GameManager.Instance.GetCoinsPerClick()) + "<sprite=0>");
                GameEvents.current.CoinChangeTrigger();
            }


            clickXPText.text = string.Format("+" + GameManager.Instance.FormatNumber(GameManager.Instance.GetXPPerClick()) + "<sprite=1>");
            GameEvents.current.CoinChangeTrigger();
        }

        text.transform.position = new Vector2(position.x + 20, position.y);
        xpText.transform.position = new Vector2(position.x + 20, position.y - clickXPText.preferredHeight/2);

        while (clickText.color.a > 0.0f)
        {
            if (GameManager.Instance.GetDidCrit())
            {
                //text.transform.position = new Vector2(position.x + 150, position.y);
                clickText.fontSize = 25;
                text.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 50);
                text.transform.position = new Vector2(text.transform.position.x + 50f * Time.deltaTime, text.transform.position.y - 50f * Time.deltaTime);
                clickText.color = new Color(255, 255, 0, clickText.color.a - (Time.deltaTime * 0.5f));
                GameManager.Instance.SetCritBool(false);
            }
            else
            {
                text.transform.position = new Vector2(text.transform.position.x + 25f * Time.deltaTime, text.transform.position.y + 50f * Time.deltaTime);
                clickText.color = new Color(clickText.color.r, clickText.color.g, clickText.color.b, clickText.color.a - (Time.deltaTime));

            }

            xpText.transform.position = new Vector2(xpText.transform.position.x + 25f * Time.deltaTime, xpText.transform.position.y + 50f * Time.deltaTime);
            clickXPText.color = new Color(clickXPText.color.r, clickXPText.color.g, clickXPText.color.b, clickXPText.color.a - (Time.deltaTime));

            yield return null;
        }

        
        Destroy(text);
        Destroy(xpText);

    }

    #endregion

    void ShowHideInfoPanel()
    {
        bool active = uICollector.infoPanel.activeSelf;

        uICollector.infoPanel.SetActive(!active);
    }

    void ShowHideMenuPanel()
    {
        bool active = uICollector.menuPanel.activeSelf;

        uICollector.menuPanel.SetActive(!active);
    }

    void ExitGame()
    {
        GameManager.Instance.SaveGame();
        Application.Quit();
    }

    void BuyAmountButtonClick()
    {
        BuyAmountButtonBehaviour button = uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>();

        button.OnClick();
        UpdateUpgradeButtons();
    }

    double CalcCost(Upgrade upgrade, int amount)
    {
        Upgrade tempUpgrade = new Upgrade("temp", -1, upgrade.GetCost(), IncomeType.CPS,0, 0, 99);
        double totalCost = 0;
        for (int i = 0; i <= amount-1; i++)
        {
            totalCost += tempUpgrade.GetCost();
            tempUpgrade.SetAmount(tempUpgrade.GetAmount() + 1);
            tempUpgrade.SetCost(tempUpgrade.GetBaseCost() * System.Math.Pow(1.15f, tempUpgrade.GetAmount()));
        }

        return totalCost;
    }

    void SaveGame()
    {
        GameManager.Instance.SaveGame();
    }

    void LoadGame() 
    {
        GameManager.Instance.LoadGame();
        HideAllPanels();
        InstantiateUpgradeButtons();
        InstantiatePowerUpButtons();
        GameEvents.current.CoinChangeTrigger();
        UpdateXPInfo();
        PowerUpManager.Instance.CheckRequirementsForAllBuildings();
    }

    void UpdateCoinsInfo()
    {
        UpdateCoinsClickInfo();
        InfoPanelUpdate();
        UpdateUpgradeButtonsInfo();
        PowerUpManager.Instance.CheckRequirements();
    }

    void UpdateCoinsClickInfo()
    {
        double coins = GameManager.Instance.GetCoins();
        uICollector.coinStatText.text = GameManager.Instance.FormatNumber(coins) + " <sprite=2>";

        UICollector.Instance.critStatText.text = GameManager.Instance.GetCritChance().ToString() + "%" + " <sprite=3>";

        
    }

    void InfoPanelUpdate()
    {
        string totalCoins;
        string totalCoinsPerClick;
        string totalCoinsPerSecond;

        totalCoins = GameManager.Instance.FormatNumber(GameManager.Instance.GetTotalCoins());
        totalCoinsPerClick = GameManager.Instance.FormatNumber(GameManager.Instance.GetTotalCoinsPerClick());
        totalCoinsPerSecond = GameManager.Instance.FormatNumber(GameManager.Instance.GetTotalCoinsPerSecond());

        uICollector.infoNumbers.text = string.Format(totalCoins + "\n\n" + totalCoinsPerClick + "\n\n" + totalCoinsPerSecond + "\n\n\n\n" + GameManager.Instance.FormatNumber(GameManager.Instance.GetClicksDone()) + "\n\n" 
            + GameManager.Instance.FormatNumber(GameManager.Instance.GetCriticalClicksDone()));
    }

    void UpdateUpgradeButtonsInfo()
    {
        foreach (Button button in uICollector.upgradeButtonList)
        {
            Upgrade upgrade = button.GetComponent<UpgradeButton>().GetUpgrade();

            if (uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount() > 1)
            {
                if (CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()) <= GameManager.Instance.GetCoins())
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else
            {
                if (upgrade.GetCost() <= GameManager.Instance.GetCoins())
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
    }

    void UpdateXPInfo()
    {
        uICollector.levelText.text = string.Format("Lvl: " + GameManager.Instance.GetLevel());

        uICollector.xpBar.GetComponent<Slider>().maxValue = (float)GameManager.Instance.GetMaxXP();
        uICollector.xpBar.GetComponent<Slider>().value = (float)GameManager.Instance.GetCurrentXP();

        if (GameManager.Instance.GetCurrentXP() >= GameManager.Instance.GetMaxXP())
        {
            GameManager.Instance.LevelUp();
            GameEvents.current.XPChange();
        }
        PowerUpManager.Instance.CheckRequirements();
    }

    void SaveAndQuit()
    {
        GameManager.Instance.SaveGame();
        quitComfirmation = true;
        Application.Quit();
    }

    void NoSaveButQuit()
    {
        quitComfirmation = true;
        Application.Quit();
    }

    void QuitCancel()
    {
        uICollector.quitPanel.SetActive(false);
    }

    public static void ShowQuitPanel()
    {
        UICollector.Instance.quitPanel.SetActive(true);
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    static bool WantsToQuit()
    {
        if(quitComfirmation)
        {
            return true;
        }
        else
        {
            HideAllPanels();
            ShowQuitPanel();
        }
        return false;
    }

    static void HideAllPanels()
    {
        UICollector.Instance.infoPanel.SetActive(false);
        UICollector.Instance.menuPanel.SetActive(false);
        UICollector.Instance.quitPanel.SetActive(false);
    }
   

}
