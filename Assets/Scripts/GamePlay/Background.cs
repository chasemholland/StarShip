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
    float moneyValue;
    const string moneyTextPrefix = "Space Coins: ";

    [SerializeField]
    TextMeshProUGUI roundText;
    float roundValue = 0;
    const string roundTextPrefix = "Rounds Completed: ";

    [SerializeField]
    Image healthBar;
    float healthAmount;
    float maxHealth;

    [SerializeField]
    Image shieldBarBoarder;
    Color shieldBarBoarderColor;
    Color shieldPulseColor;

    [SerializeField]
    Image shieldBar;
    float shieldAmount;
    float maxShield;

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

        // add as listener for increase shield event
        EventManager.AddListener(EventName.IncreaseShieldEvent, HandleIncreaseShieldEvent);

        // add as listener for decrease shield event
        EventManager.AddListener(EventName.DecreaseShieldEvent, HandleDecreaseShieldEvent);

        // set up money text
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);
        moneyText.text = moneyTextPrefix + moneyValue.ToString();

        // set up round text
        roundText.text = roundTextPrefix + roundValue.ToString();

        // get health amount
        healthAmount = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
        maxHealth = healthAmount;

        // set shield boarder visibillity
        shieldBarBoarderColor = shieldBarBoarder.color;
        shieldBarBoarderColor.a = 0;
        shieldBarBoarder.color = shieldBarBoarderColor;

        // set shield amount
        shieldAmount = 0;
        maxShield = ConfigUtils.Ship1MaxShield * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMaxShield.ToString(), 0));
        shieldBar.fillAmount = 0;
        shieldPulseColor = GameObject.FindGameObjectWithTag("ShieldPulse").GetComponent<SpriteRenderer>().color;
        shieldPulseColor.a = 0;
        GameObject.FindGameObjectWithTag("ShieldPulse").GetComponent<SpriteRenderer>().color = shieldPulseColor;
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

    /// <summary>
    /// Handles incrementing the rounds completed
    /// </summary>
    /// <param name="number"></param>
    private void HandleAddRoundEvent(float number)
    {
        // increment round number
        roundValue += number;

        // save money on round complete
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

        // save rounds completed
        PlayerPrefs.SetFloat(PlayerPrefNames.RoundsCompleted.ToString(), roundValue);

        // set round text
        roundText.text = roundTextPrefix + roundValue.ToString();
    }

    /// <summary>
    /// Handles player loosing health
    /// </summary>
    /// <param name="amount"></param>
    private void HandleDecreaseHealthEvent(float amount)
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
    private void HandleIncreaseHealthEvent(float amount)
    {
        // increment health
        healthAmount += amount;

        // clamp health to range between 0 and max health
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        // set health bar
        healthBar.fillAmount = healthAmount / maxHealth;
    }

    /// <summary>
    /// Handles player gaining shield
    /// </summary>
    /// <param name="amount"></param>
    private void HandleIncreaseShieldEvent(float amount)
    {
        // set sheild visible
        if (shieldBarBoarderColor.a == 0)
        {
            shieldBarBoarderColor.a = 1;
            shieldBarBoarder.color = shieldBarBoarderColor;

            // activate shield pulse
            shieldPulseColor.a = 1;
            GameObject.FindGameObjectWithTag("ShieldPulse").GetComponent<SpriteRenderer>().color = shieldPulseColor;
        }

        // increment shield amount
        shieldAmount += amount;

        // clamp shield to range between 0 and max shield
        shieldAmount = Mathf.Clamp(shieldAmount, 0, maxShield);

        // set sheild bar fill
        shieldBar.fillAmount = shieldAmount / maxShield;
    }

    /// <summary>
    /// Handles player loosing shield
    /// </summary>
    /// <param name="amount"></param>
    private void HandleDecreaseShieldEvent(float amount)
    {
        // decrement shield amount
        shieldAmount -= amount;

        if (shieldAmount == 0)
        {
            // make boarder invisible
            shieldBarBoarderColor.a = 0;
            shieldBarBoarder.color = shieldBarBoarderColor;

            // set shield bar fill
            shieldBar.fillAmount = 0;

            // deactivate shield pulse
            shieldPulseColor.a = 0;
            GameObject.FindGameObjectWithTag("ShieldPulse").GetComponent<SpriteRenderer>().color = shieldPulseColor;
        }
        else
        {
            // set shield bar fill
            shieldBar.fillAmount = shieldAmount / maxShield;
        }
    }

    /// <summary>
    /// Handles player death
    /// </summary>
    /// <param name="n">unused</param>
    private void HandleInvasionCompleteEvent(float n)
    {
        // load death scene
        SceneManager.LoadScene("InvasionOver");
        
    }

    #endregion
}
