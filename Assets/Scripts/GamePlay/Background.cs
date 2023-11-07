using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Background (HUD)
/// </summary>
public class Background : MonoBehaviour
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI moneyText;
    int moneyValue;
    const string moneyTextPrefix = "Space Coins: ";

    [SerializeField]
    TextMeshProUGUI roundText;
    int roundValue = 0;
    const string roundTextPrefix = "Rounds Completed: ";

    [SerializeField]
    Image healthBar;
    float healthAmount;
    float maxHealth;

    #endregion

    #region Unity Methods
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // add as listener for add money event
        EventManager.AddListener(EventName.AddMoneyEvent, HandleAddMoneyEvent);

        // add as listener for loose money event
        EventManager.AddListener(EventName.InvasionCompleteEvent, HandleInvasionCompleteEvent);

        // add as listener for add round event
        EventManager.AddListener(EventName.AddRoundEvent, HandleAddRoundEvent);

        // add as listener for increase health event
        EventManager.AddListener(EventName.IncreaseHealthEvent, HandleIncreaseHealthEvent);

        // add as listener for decrease health event
        EventManager.AddListener(EventName.DecreaseHealthEvent, HandleDecreaseHealthEvent);

        // set up money text
        moneyValue = PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString(), 0);
        moneyText.text = moneyTextPrefix + moneyValue.ToString();

        // set up round text
        roundText.text = roundTextPrefix + roundValue.ToString();

        // get health amount
        healthAmount = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
        maxHealth = healthAmount;

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles adding money event
    /// </summary>
    /// <param name="amount"></param>
    private void HandleAddMoneyEvent(int amount)
    {
        // increment money
        moneyValue += amount;
        
        // set money text
        moneyText.text = moneyTextPrefix + moneyValue.ToString();
    }
    /*
    /// <summary>
    /// Handles removing money event
    /// </summary>
    /// <param name="amount"></param>
    private void HandleLooseMoneyEvent(int amount)
    {
        // decrement health
        moneyValue -= amount;

        // set money text
        moneyText.text = moneyTextPrefix + moneyValue.ToString();
    }
    */

    /// <summary>
    /// Handles incrementing the rounds completed
    /// </summary>
    /// <param name="number"></param>
    private void HandleAddRoundEvent(int number)
    {
        // increment round number
        roundValue += number;

        // save money on round complete
        PlayerPrefs.SetInt(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

        // save rounds completed
        PlayerPrefs.SetInt(PlayerPrefNames.RoundsCompleted.ToString(), roundValue);

        // set round text
        roundText.text = roundTextPrefix + roundValue.ToString();
    }

    /// <summary>
    /// Handles player loosing health
    /// </summary>
    /// <param name="amount"></param>
    private void HandleDecreaseHealthEvent(int amount)
    {
        // decrement health
        healthAmount -= amount;

        // set health bar
        healthBar.fillAmount = healthAmount / maxHealth;
    }

    /// <summary>
    /// Handles player gaining health
    /// </summary>
    /// <param name="amount"></param>
    private void HandleIncreaseHealthEvent(int amount)
    {
        // increment health
        healthAmount += amount;

        // clamp health to range between 0 and max health
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        // set health bar
        healthBar.fillAmount = healthAmount / maxHealth;
    }

    /// <summary>
    /// Handles player death
    /// </summary>
    /// <param name="n">unused</param>
    private void HandleInvasionCompleteEvent(int n)
    {
        // load death scene
        SceneManager.LoadScene("InvasionOver");
        
    }

    #endregion
}
