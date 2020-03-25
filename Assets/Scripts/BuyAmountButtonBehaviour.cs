using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAmountButtonBehaviour : MonoBehaviour
{
    int amount = 0; // 1, 5, 10, 25, 50, 100 | 0, 1, 2, 3, 4, 5
    int clicks = 0;



    public void OnClick()
    {
        clicks++;

        if(clicks > 5)
        {
            clicks = 0;
        }
        
    }

    public int GetAmount()
    {
        switch (clicks)
        {
            case 0:
                amount = 1;
                break;
            case 1:
                amount = 5;
                break;
            case 2:
                amount = 10;
                break;
            case 3:
                amount = 25;
                break;
            case 4:
                amount = 50;
                break;
            case 5:
                amount = 100;
                break;
            default:
                amount = 1;
                break;
        }


        return amount;
    }

    public string GetAmountText()
    {
        switch (clicks)
        {
            case 0:
                return "1x";
            case 1:
                return "5x";
            case 2:
                return "10x";
            case 3:
                return "25x";
            case 4:
                return "50x";
            case 5:
                return "100x";
            default:
                return "1x";
        }
    }

}
