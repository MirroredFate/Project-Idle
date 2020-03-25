using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    Upgrade upgrade;

    public Upgrade GetUpgrade()
    {
        return upgrade;
    }
    
    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
    }
}
