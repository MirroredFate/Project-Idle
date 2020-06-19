using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onCoinChangeTrigger;
    public event Action onXPChange;
    public void CoinChangeTrigger()
    {
        onCoinChangeTrigger?.Invoke();
    }

    public void XPChange()
    {
        onXPChange?.Invoke();
    }
}
