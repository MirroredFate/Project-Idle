using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollector : MonoBehaviour
{
    //General Click Stuff
    [Header("General Click Stuff")]
    public Text coinStatText;
    public Text cpsStatText;
    public Button clickButton;

    //XP Bar Stuff
    [Header("XP Bar")]
    public GameObject xpBar;
    public Text levelText;

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
    public Button exitButton;

    //FadeText Stuff
    [Header("Fade Text Stuff")]
    public GameObject clickText;
    public GameObject autoClicker;


    //Upgrade Stuff
    [Header("Upgrade Stuff")]
    public GameObject upgradePanel;
    public Button upgradeButtonPrefab;
    public List<Button> upgradeButtonList;

    

}
