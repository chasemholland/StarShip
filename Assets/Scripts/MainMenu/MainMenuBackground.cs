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
        // add as listener for add money event
        EventManager.AddListener(EventName.AddMoneyEvent, HandleAddMoneyEvent);

        // set up money text
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());
        moneyText.text = moneyTextPrefix + moneyValue.ToString();

    }

    private void Update()
    {
        // update money in main menu
        if (moneyValue != PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString()))
        {
            // set correct value
            moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());

            // set money text
            moneyText.text = moneyTextPrefix + moneyValue.ToString();
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles adding money event
    /// </summary>
    /// <param name="amount"></param>
    private void HandleAddMoneyEvent(float amount)
    {
        // increment money
        moneyValue += amount;

        // set money text
        moneyText.text = moneyTextPrefix + moneyValue.ToString();
    }

    #endregion
}
