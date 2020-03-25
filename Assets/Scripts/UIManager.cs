using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] UICollector uICollector;

    float timePassed;
    double formatThreshold = 999999999999;

    // Start is called before the first frame update
    void Start()
    {
        uICollector.clickButton.onClick.AddListener(() => StartCoroutine(OnClickText(Input.mousePosition, uICollector.clickButton.transform, false)));
        uICollector.infoButton.onClick.AddListener(() => ShowHideInfoPanel());
        uICollector.menuButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.continueButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.exitButton.onClick.AddListener(() => ExitGame());
        uICollector.amountButton.onClick.AddListener(() => BuyAmountButtonClick());

        uICollector.upgradeButtonList = new List<Button>();
        UpgradeManager.instance.InitializeUpgrades();
        InstantiateUpgradeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        #region Coin Display Update

        double coins = System.Math.Round(GameManager.Instance.GetCoins());
        if (GameManager.Instance.GetCoins() < formatThreshold)
        {
            uICollector.coinStatText.text = coins.ToString("N0") + " Coins";
        }
        else
        {
            uICollector.coinStatText.text = coins.ToString("e3") + " Coins";
        }

        if(GameManager.Instance.GetCoinsPerSecond() < formatThreshold)
        {
            uICollector.cpsStatText.text = GameManager.Instance.GetCoinsPerSecond().ToString("N0") + " Coins/s";
        }
        else
        {
            uICollector.cpsStatText.text = GameManager.Instance.GetCoinsPerSecond().ToString("e3") + " Coins/s";
        }

        
        #endregion

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

        #region Information Panel Display Update

        string totalCoins;
        string totalCoinsPerClick;
        string totalCoinsPerSecond;

        if(GameManager.Instance.GetTotalCoins() < formatThreshold)
        {
            totalCoins = GameManager.Instance.GetTotalCoins().ToString("N0");
        }
        else
        {
            totalCoins = GameManager.Instance.GetTotalCoins().ToString("e3");
        }

        if (GameManager.Instance.GetTotalCoinsPerClick() < formatThreshold)
        {
            totalCoinsPerClick = GameManager.Instance.GetTotalCoinsPerClick().ToString("N0");
        }
        else
        {
            totalCoinsPerClick = GameManager.Instance.GetTotalCoinsPerClick().ToString("e3");
        }

        if (GameManager.Instance.GetTotalCoinsPerSecond() < formatThreshold)
        {
            totalCoinsPerSecond = GameManager.Instance.GetTotalCoinsPerSecond().ToString("N0");
        }
        else
        {
            totalCoinsPerSecond = GameManager.Instance.GetTotalCoinsPerSecond().ToString("e3");
        }


        uICollector.infoNumbers.text = string.Format(totalCoins + "\n\n" + totalCoinsPerClick + "\n\n" + totalCoinsPerSecond);


        #endregion

        #region Upgrade Button Behaviour Update


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


        #endregion

        #region XP Bar Behaviour Update

        if (GameManager.Instance.GetLevel() < formatThreshold)
        {
            uICollector.levelText.text = string.Format("Lvl: " + GameManager.Instance.GetLevel().ToString("N0") + "\n" + GameManager.Instance.GetCurrentXP().ToString("N0")
                + "/" + GameManager.Instance.GetMaxXP().ToString("N0"));
        }
        else
        {
            uICollector.levelText.text = string.Format("Lvl: " + GameManager.Instance.GetLevel().ToString("e3") + "\n" + GameManager.Instance.GetCurrentXP().ToString("e3")
                + "/" + GameManager.Instance.GetMaxXP().ToString("e3"));
        }

        uICollector.xpBar.GetComponent<Slider>().maxValue = (float)GameManager.Instance.GetMaxXP();
        uICollector.xpBar.GetComponent<Slider>().value = (float)GameManager.Instance.GetCurrentXP();

        if(GameManager.Instance.GetCurrentXP() >= GameManager.Instance.GetMaxXP())
        {
            GameManager.Instance.LevelUp();
        }

        #endregion

        #region BuyAmount Button Update

        Text text = uICollector.amountButton.GetComponentInChildren<Text>();
        text.text = uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmountText();

        #endregion
    }

    #region Upgrade Related Stuff
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

                Text uptext = upgradeButton.GetComponentInChildren<Text>();

                UpgradeButton upBtn = upgradeButton.GetComponent<UpgradeButton>();
                Upgrade upgrade = upBtn.GetUpgrade();

                if (upBtn.GetUpgrade().GetCost() < formatThreshold)
                {
                    uptext.text = string.Format(upgrade.GetName() + "\n" +
                        upgrade.GetCost().ToString("N0") + " Coins") + "\n" +
                        upgrade.GetAmount().ToString();
                }
                else
                {
                    uptext.text = string.Format(upgrade.GetName() + "\n" +
                        upgrade.GetCost().ToString("e3") + " Coins") + "\n" +
                        upgrade.GetAmount().ToString();
                }

                uICollector.upgradeButtonList.Add(upgradeButton);

            }

        }
        else
        {

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
                GameManager.Instance.RemoveCoins(System.Math.Round(upgrade.GetCost()));
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
                GameManager.Instance.RemoveCoins(System.Math.Round(CalcCost(upgrade, amountBtn.GetAmount())));

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

        upgrade.SetCost(upgrade.GetBaseCost() * System.Math.Pow(1.15f, upgrade.GetAmount()));
        UpdateUpgradeButtons();
    }

    void UpdateUpgradeButtons()
    {
        if (uICollector.upgradeButtonList.Count > 0)
        {
            foreach (Button button in uICollector.upgradeButtonList)
            {

                Text uptext = button.GetComponentInChildren<Text>();
                Upgrade upgrade = button.GetComponent<UpgradeButton>().GetUpgrade();

                if (uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount() > 1)
                {
                    if (CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()) < formatThreshold)
                    {
                        uptext.text = string.Format(upgrade.GetName() + "\n" +
                            CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()).ToString("N0") + " Coins") + "\n" +
                            upgrade.GetAmount().ToString();
                    }
                    else
                    {
                        uptext.text = string.Format(upgrade.GetName() + "\n" +
                            CalcCost(upgrade, uICollector.amountButton.GetComponent<BuyAmountButtonBehaviour>().GetAmount()).ToString("e3") + " Coins") + "\n" +
                            upgrade.GetAmount().ToString();
                    }

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
                    if (upgrade.GetCost() < formatThreshold)
                    {
                        uptext.text = string.Format(upgrade.GetName() + "\n" +
                            upgrade.GetCost().ToString("N0") + " Coins") + "\n" +
                            upgrade.GetAmount().ToString();
                    }
                    else
                    {
                        uptext.text = string.Format(upgrade.GetName() + "\n" +
                            upgrade.GetCost().ToString("e3") + " Coins") + "\n" +
                            upgrade.GetAmount().ToString();
                    }

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
        // Vector3 mousePos = Camera.main.WorldToScreenPoint(Input.)
        GameObject text = Instantiate(uICollector.clickText, new Vector2(position.x + 100, position.y), Quaternion.identity);
        text.transform.SetParent(parent);
        Text clickText = text.GetComponent<Text>();

        GameObject xpText = Instantiate(uICollector.clickText, new Vector2(position.x, position.y), Quaternion.identity);
        xpText.transform.SetParent(parent);
        Text clickXPText = xpText.GetComponent<Text>();

        if (autoClick)
        {

            GameManager.Instance.IncreaseCoinsPerSecond();

            if(GameManager.Instance.GetCoinsPerSecond() < formatThreshold)
            {
                clickText.text = "+" + string.Format(GameManager.Instance.GetCoinsPerSecond().ToString("N0") + " Coins");
            }
            else
            {
                clickText.text = "+" + string.Format(GameManager.Instance.GetCoinsPerSecond().ToString("e3") + " Coins");
            }
            
        }
        else
        {
            GameManager.Instance.IncreaseCoins();
            GameManager.Instance.XPClick();

            if(GameManager.Instance.GetCoinsPerClick() < formatThreshold)
            {
                clickText.text = "+" + string.Format(GameManager.Instance.GetCoinsPerClick().ToString("N0") + " Coins");
            }
            else
            {
                clickText.text = "+" + string.Format(GameManager.Instance.GetCoinsPerClick().ToString("e3") + " Coins");
            }

            
            if (GameManager.Instance.GetXPPerClick() < formatThreshold)
            {
                clickXPText.text = string.Format("+" + GameManager.Instance.GetXPPerClick().ToString("N0") + " XP");
            }
            else
            {
                clickXPText.text = string.Format("+" + GameManager.Instance.GetXPPerClick().ToString("e3") + " XP");
            }
        }

        while(clickText.color.a > 0.0f)
        {
            text.transform.position = new Vector2(text.transform.position.x + 25f * Time.deltaTime, text.transform.position.y + 50f * Time.deltaTime);
            clickText.color = new Color(clickText.color.r, clickText.color.g, clickText.color.b, clickText.color.a - (Time.deltaTime));

            xpText.transform.position = new Vector2(xpText.transform.position.x - 25f * Time.deltaTime, xpText.transform.position.y + 50f * Time.deltaTime);
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
        for (int i = 0; i < amount-1; i++)
        {
            totalCost += tempUpgrade.GetCost();
            tempUpgrade.SetAmount(tempUpgrade.GetAmount() + 1);
            tempUpgrade.SetCost(tempUpgrade.GetBaseCost() * System.Math.Pow(1.15f, tempUpgrade.GetAmount()));
        }

        return totalCost;
    }

}
