using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICollector : MonoBehaviour
{
    public static UICollector Instance { get; private set; }

    //General Click Stuff
    [Header("General Click Stuff")]
    public TextMeshProUGUI coinStatText;
    public TextMeshProUGUI critStatText;
    public Button clickButton;

    //XP Bar Stuff
    [Header("XP Bar")]
    public GameObject xpBar;
    public TextMeshProUGUI levelText;

    //Information Panel Stuff
    [Header("Information Panel Stuff")]
    public Button infoButton;
    public GameObject infoPanel;
    public Text infoNumbers;

    //Menu Panel Stuff
    [Header("Menu Panel Stuff")]
    public GameObject menuPanel;
    public Button menuButton;
    public Button continueButton;
    public Button saveButton;
    public Button loadButton;
    public Button exitButton;

    //Quit Panel Stuff
    [Header("Quit Panel Stuff")]
    public GameObject quitPanel;
    public Button saveQuitButton;
    public Button noSaveQuitButton;
    public Button cancelButton;

    //FadeText Stuff
    [Header("Fade Text Stuff")]
    public GameObject clickText;
    public GameObject autoClicker;
    public Sprite coinSprite;
    public Sprite xpSprite;


    //Upgrade Stuff
    [Header("Upgrade Stuff")]
    public GameObject upgradePanel;
    public Button upgradeButtonPrefab;
    public List<Button> upgradeButtonList;
    public Button amountButton;

    //PowerUp Stuff
    [Header("PowerUp Stuff")]
    public Sprite clickPowerUpImage;
    public Sprite xpPowerUpImage;
    public Sprite buildingPowerUpImage;
    public Sprite critPowerUpImage;
    public Button powerUpButtonPrefab;
    public GameObject powerUpParent;
    public List<Button> powerUpButtons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


}
