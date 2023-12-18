using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// MainMenu Background (HUD)
/// </summary>
public class MainMenuBackground : MonoBehaviour
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI moneyText;
    float moneyValue;
    const string moneyTextPrefix = "Space Coins: ";

    #endregion

    #region Unity Methods
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set up money text
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);
        moneyText.text = moneyTextPrefix + MoneyHandler.ConvertMoney(moneyValue);

    }

    private void Update()
    {
        // update money in main menu
        if (moneyValue != PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString()))
        {
            // set correct value
            moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());

            // set money text
            moneyText.text = moneyTextPrefix + MoneyHandler.ConvertMoney(moneyValue);
        }
    }

    #endregion
}
