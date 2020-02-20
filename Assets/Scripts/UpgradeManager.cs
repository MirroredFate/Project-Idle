using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private List<Upgrade> upgrades;


    // Start is called before the first frame update
    private void Awake()
    {
        upgrades = new List<Upgrade>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitializeUpgrades()
    {
        //---------------------------------------------------------------------------
        Upgrade farm = new Upgrade(
            "Farm",             //Name
            0,                  //ID
            10,                 //Cost
            IncomeType.CPS,     //Type
            0,                  //Coins per Click
            2);                 //Coins per Second
        upgrades.Add(farm);
        //---------------------------------------------------------------------------
        Upgrade inn = new Upgrade(
            "Inn",              //Name
            1,                  //ID
            125,                //Cost
            IncomeType.CPS,     //Type
            0,                  //Coins per Click
            6);                 //Coins per Second
        upgrades.Add(inn);
        //---------------------------------------------------------------------------
        Upgrade blacksmith = new Upgrade(
            "Blacksmith",       //Name
            2,                  //ID
            600,                //Cost
            IncomeType.CPS,     //Type
            0,                  //Coins per Click
            20);                //Coins per Second
        upgrades.Add(blacksmith);
        //---------------------------------------------------------------------------
        Upgrade warriorBarracks = new Upgrade(
            "Warrior Barracks",             //Name
            3,                              //ID
            1800,                           //Cost
            IncomeType.CPS,                 //Type
            0,                              //Coins per Click
            65);                            //Coins per Second
        upgrades.Add(warriorBarracks);
        //---------------------------------------------------------------------------
        Upgrade knightsJousts = new Upgrade(
           "Knights Jousts",                //Name
           4,                               //ID
           5600,                            //Cost
           IncomeType.CPS,                  //Type
           0,                               //Coins per Click
           200);                            //Coins per Second
        upgrades.Add(knightsJousts);
        //---------------------------------------------------------------------------
        Upgrade wizardTower = new Upgrade(
           "Wizard Tower",                  //Name
           5,                               //ID
           38000,                           //Cost
           IncomeType.CPS,                  //Type
           0,                               //Coins per Click
           650);                            //Coins per Second
        upgrades.Add(wizardTower);
        //---------------------------------------------------------------------------
        Upgrade cathedral = new Upgrade(
           "Cathedral",                     //Name
           6,                               //ID
           442000,                          //Cost
           IncomeType.CPS,                  //Type
           0,                               //Coins per Click
           2000);                           //Coins per Second
        upgrades.Add(cathedral);
        //---------------------------------------------------------------------------
        Upgrade citadel = new Upgrade(
           "Citadel",              //Name
           7,                              //ID
           7300000,                   //Cost
           IncomeType.CPS,                 //Type
           0,                              //Coins per Click
           8500);                       //Coins per Second
        upgrades.Add(citadel);
        //---------------------------------------------------------------------------
        Upgrade royalCastle = new Upgrade(
           "Royal Castle",              //Name
           8,                              //ID
           145000000,                   //Cost
           IncomeType.CPS,                 //Type
           0,                              //Coins per Click
           100000);                       //Coins per Second
        upgrades.Add(royalCastle);
        //---------------------------------------------------------------------------
        Upgrade heavensGate = new Upgrade(
           "Heaven's Gate",              //Name
           9,                              //ID
           3200000000,                   //Cost
           IncomeType.CPS,                 //Type
           0,                              //Coins per Click
           1200000);                       //Coins per Second
        upgrades.Add(heavensGate);
        //---------------------------------------------------------------------------
        Upgrade hall = new Upgrade(
           "Hall of Legends",              //Name
           10,                              //ID
           200000000000,                   //Cost
           IncomeType.CPS,                 //Type
           0,                              //Coins per Click
           2500000);                       //Coins per Second
        upgrades.Add(hall);
        //---------------------------------------------------------------------------

    }



    public List<Upgrade> GetUpgradeList()
    {
        return upgrades;
    }
}
