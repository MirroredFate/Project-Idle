using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] UICollector uICollector;

    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        uICollector.clickButton.onClick.AddListener(() => StartCoroutine(OnClickText(Input.mousePosition, uICollector.clickButton.transform, false)));
        uICollector.infoButton.onClick.AddListener(() => ShowHideInfoPanel());
        uICollector.menuButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.continueButton.onClick.AddListener(() => ShowHideMenuPanel());
        uICollector.exitButton.onClick.AddListener(() => ExitGame());

        uICollector.upgradeButtonList = new List<Button>();
        uICollector.upgradeManager.InitializeUpgrades();
        InstantiateUpgradeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        #region Coin Display Update

        double coins = System.Math.Round(uICollector.gameManager.GetCoins());
        if (uICollector.gameManager.GetCoins() < 999999)
        {
            uICollector.coinStatText.text = coins.ToString("N0") + " Coins";
        }
        else
        {
            uICollector.coinStatText.text = coins.ToString("e3") + " Coins";
        }

        if(uICollector.gameManager.GetCoinsPerSecond() < 999999)
        {
            uICollector.cpsStatText.text = uICollector.gameManager.GetCoinsPerSecond().ToString("N0") + " / s";
        }
        else
        {
            uICollector.cpsStatText.text = uICollector.gameManager.GetCoinsPerSecond().ToString("e3") + "/s";
        }

        
        #endregion

        #region Coins Per Second Text Fade Update

        timePassed += Time.deltaTime;

        if(timePassed >= 1f)
        {
            if(uICollector.gameManager.GetCoinsPerSecond() > 0)
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

        if(uICollector.gameManager.GetTotalCoins() < 999999)
        {
            totalCoins = uICollector.gameManager.GetTotalCoins().ToString("N0");
        }
        else
        {
            totalCoins = uICollector.gameManager.GetTotalCoins().ToString("e3");
        }

        if (uICollector.gameManager.GetTotalCoinsPerClick() < 999999)
        {
            totalCoinsPerClick = uICollector.gameManager.GetTotalCoinsPerClick().ToString("N0");
        }
        else
        {
            totalCoinsPerClick = uICollector.gameManager.GetTotalCoinsPerClick().ToString("e3");
        }

        if (uICollector.gameManager.GetTotalCoinsPerSecond() < 999999)
        {
            totalCoinsPerSecond = uICollector.gameManager.GetTotalCoinsPerSecond().ToString("N0");
        }
        else
        {
            totalCoinsPerSecond = uICollector.gameManager.GetTotalCoinsPerSecond().ToString("e3");
        }


        uICollector.infoNumbers.text = string.Format(totalCoins + "\n\n" + totalCoinsPerClick + "\n\n" + totalCoinsPerSecond);


        #endregion

        #region Upgrade Button Behaviour Update

        foreach (Button button in uICollector.upgradeButtonList)
        {
            if(button.GetComponent<UpgradeButton>().GetCost() <= uICollector.gameManager.GetCoins())
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }

            

        }

        #endregion


    }

    #region Upgrade Related Stuff
    void InstantiateUpgradeButtons()
    {
        if(uICollector.upgradeManager.GetUpgradeList() != null && uICollector.upgradeManager.GetUpgradeList().Count > 0)
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

            for (int i = 0; i < uICollector.upgradeManager.GetUpgradeList().Count; i++)
            {
                Button upgrade = Instantiate(uICollector.upgradeButtonPrefab, uICollector.upgradePanel.transform);
                int index = uICollector.upgradeManager.GetUpgradeList().IndexOf(uICollector.upgradeManager.GetUpgradeList()[i]);
                upgrade.onClick.RemoveAllListeners();
                upgrade.onClick.AddListener(() => BuyUpgrade(uICollector.upgradeManager.GetUpgradeList()[index]));
                

                Text uptext = upgrade.GetComponentInChildren<Text>();

                upgrade.GetComponent<UpgradeButton>().SetCost(uICollector.upgradeManager.GetUpgradeList()[i].GetCost());
                upgrade.GetComponent<UpgradeButton>().SetAmount((int)uICollector.upgradeManager.GetUpgradeList()[i].GetAmount());

                if (uICollector.upgradeManager.GetUpgradeList()[i].GetCost() < 999999)
                {
                    uptext.text = string.Format(uICollector.upgradeManager.GetUpgradeList()[i].GetName() + "\n" + 
                        uICollector.upgradeManager.GetUpgradeList()[i].GetCost().ToString("N0") + " Coins") + "\n" + 
                        uICollector.upgradeManager.GetUpgradeList()[i].GetAmount().ToString();
                }
                else
                {
                    uptext.text = string.Format(uICollector.upgradeManager.GetUpgradeList()[i].GetName() + "\n" + 
                        uICollector.upgradeManager.GetUpgradeList()[i].GetCost().ToString("e3") + " Coins") + "\n" +
                        uICollector.upgradeManager.GetUpgradeList()[i].GetAmount().ToString();
                }

                uICollector.upgradeButtonList.Add(upgrade);
                
                //Debug.Log("Name: " + uICollector.upgradeManager.GetUpgradeList()[i].GetName() + "|" + "1x " + uICollector.upgradeManager.GetUpgradeList()[i].GetCost() + " Coins");
            }

        }
        else
        {
            //Debug.Log("UpgradeList is null or empty...");
        }
        
    }

    void BuyUpgrade(Upgrade upgrade)
    {

        //Debug.Log("You are trying to buy... " + upgrade.GetName());
        if(upgrade.GetCost() <= uICollector.gameManager.GetCoins())
        {
            uICollector.gameManager.RemoveCoins(System.Math.Round(upgrade.GetCost()));
            int index = uICollector.upgradeManager.GetUpgradeList().IndexOf(upgrade);

            double amountCalc = (upgrade.GetAmount()+1) % 5;
            Debug.Log(amountCalc);

            if (amountCalc == 0)
            {
                upgrade.IncreaseIncome(50);
            }

            if (upgrade.GetIncomeType() == IncomeType.CPC)
            {
                uICollector.gameManager.IncreaseClickPower(upgrade.GetCoinsPerClick());
            }
            else
            {
                uICollector.gameManager.IncreaseCPS(upgrade.GetCoinsPerSecond());
            }

            upgrade.SetActive(true);

            upgrade.SetAmount(upgrade.GetAmount() + 1);

            upgrade.SetCost(System.Math.Round(upgrade.GetCost() * System.Math.Pow(1.15f, upgrade.GetAmount())));

            uICollector.upgradeManager.GetUpgradeList().RemoveAt(index);
            uICollector.upgradeManager.GetUpgradeList().Insert(index, upgrade);
            InstantiateUpgradeButtons();

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

        if (autoClick)
        {

            uICollector.gameManager.IncreaseCoinsPerSecond();

            if(uICollector.gameManager.GetCoinsPerSecond() < 999999)
            {
                clickText.text = "+" + uICollector.gameManager.GetCoinsPerSecond().ToString();
            }
            else
            {
                clickText.text = "+" + uICollector.gameManager.GetCoinsPerSecond().ToString("e3");
            }
            
        }
        else
        {


            uICollector.gameManager.IncreaseCoins();

            if(uICollector.gameManager.GetCoinsPerClick() < 999999)
            {
                clickText.text = "+" + uICollector.gameManager.GetCoinsPerClick().ToString();
            }
            else
            {
                clickText.text = "+" + uICollector.gameManager.GetCoinsPerClick().ToString("e3");
            }
            

        }

        while(clickText.color.a > 0.0f)
        {
            text.transform.position = new Vector2(text.transform.position.x, text.transform.position.y + 50f * Time.deltaTime);
            clickText.color = new Color(clickText.color.r, clickText.color.g, clickText.color.b, clickText.color.a - (Time.deltaTime));

            yield return null;
        }


        Destroy(text);

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


}
