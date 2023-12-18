using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    /// <summary>
    ///
    /// </summary>
public class AlienStore : MonoBehaviour
{
    #region Fields

    [SerializeField]
    Button IncreaseTargetingChanceButton;

    [SerializeField]
    Button IncreaseMoneyMultiplierButton;

    [SerializeField]
    Button IncreaseMagnetRangeButton;

    [SerializeField]
    Button IncreaseCriticalChanceButton;

    [SerializeField]
    Button IncreaseCriticalDamageButton;

    [SerializeField]
    TextMeshProUGUI moneyValueText;
    float moneyValue;
    string moneyValuePrefix = "SPACE COINS: ";

    [SerializeField]
    TextMeshProUGUI targetingChanceStat;
    float targetingChanceStatValue;
    string targetingChanceStatPrefix = "TARGETING CHANCE: ";

    [SerializeField]
    TextMeshProUGUI criticalChanceStat;
    float criticalChanceStatValue;
    string criticalChanceStatPrefix = "CRITICAL CHANCE: ";

    [SerializeField]
    TextMeshProUGUI criticalDamageStat;
    float criticalDamageStatValue;
    string criticalDamageStatPrefix = "CRITICAL DAMAGE: ";

    [SerializeField]
    TextMeshProUGUI moneyMultiplierStat;
    float moneyMultiplierStatValue;
    string moneyMultiplierStatPrefix = "MONEY MULTIPLIER: ";

    [SerializeField]
    TextMeshProUGUI magnetRangeStat;
    float magnetRangeStatValue;
    string magnetRangeStatPrefix = "MAGNET RANGE: ";

    [SerializeField]
    TextMeshProUGUI targetingChanceCost;
    float targetingChanceCostValue;
    string targetingChanceCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI criticalChanceCost;
    float criticalChanceCostValue;
    string criticalChanceCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI criticalDamageCost;
    float criticalDamageCostValue;
    string criticalDamageCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI moneyMultiplierCost;
    float moneyMultiplierCostValue;
    string moneyMultiplierCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI magnetRangeCost;
    float magnetRangeCostValue;
    string magnetRangeCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI targetingChanceMultiplier;
    float targetingChanceMultiplierValue;
    string targetingChanceMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI criticalChanceMultiplier;
    float criticalChanceMultiplierValue;
    string criticalChanceMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI criticalDamageMultiplier;
    float criticalDamageMultiplierValue;
    string criticalDamageMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI moneyMultiplierMultiplier;
    float moneyMultiplierMultiplierValue;
    string moneyMultiplierMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI magnetRangeMultiplier;
    float magnetRangeMultiplierValue;
    string magnetRangeMultiplierPrefix = "x ";

    float targetingChanceMaxMultiplier = 5.000f;
    float criticalChanceMaxMultiplier = 5.000f;
    float criticalDamageMaxMultiplier = 8.000f;
    float moneyMultiplierMaxMultiplier = 10.000f;
    float magnetRangeMaxMultiplier = 8.00f;
    string max = "MAX";

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set money value
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);

        // set money value text
        moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

        // set values and text
        SetValuesAndText();

        // set store buttons
        SetStoreButtons();

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles text and money adjustments after purchase
    /// </summary>
    private void HandlePurchase()
    {
        // update values
        SetValuesAndText();

        // set money in player prefs
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

        // set money text
        moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

        // set the buttons
        SetStoreButtons();
    }

    /// <summary>
    /// Sets initial values and text
    /// </summary>
    private void SetValuesAndText()
    {
        //
        // set player stats values
        //

        // set targeting chnace value
        targetingChanceStatValue = ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChance.ToString(), 0));
        // set critical chance value
        criticalChanceStatValue = ConfigUtils.Ship1CritChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChance.ToString(), 0));
        // set critical damage value
        criticalDamageStatValue = ConfigUtils.Ship1CritDamageMulti * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamage.ToString(), 0));
        // set money multiplier value
        moneyMultiplierStatValue = ConfigUtils.Ship1MoneyMultiplier * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), 0));
        // set magnet value
        magnetRangeStatValue = ConfigUtils.Ship1MagnetRange * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString(), 0));

        //
        // set player stats text
        //

        // set targeting chance text
        targetingChanceStat.text = targetingChanceStatPrefix + MathF.Round(targetingChanceStatValue, 2).ToString();
        // set critical chance text
        criticalChanceStat.text = criticalChanceStatPrefix + MathF.Round(criticalChanceStatValue, 2).ToString();
        // set critical damage text
        criticalDamageStat.text = criticalDamageStatPrefix + MathF.Round(criticalDamageStatValue, 2).ToString();
        // set money multiplier text
        moneyMultiplierStat.text = moneyMultiplierStatPrefix + MathF.Round(moneyMultiplierStatValue, 2).ToString();
        // set magnet range text
        magnetRangeStat.text = magnetRangeStatPrefix + MathF.Round(magnetRangeStatValue, 2).ToString();

        //
        // set multiplier values
        //

        // set trageting chance multiplier
        targetingChanceMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChance.ToString(), 0);
        // set critical chance multiplier
        criticalChanceMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChance.ToString(), 0);
        // set critical damage multiplier
        criticalDamageMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamage.ToString(), 0);
        // set money multiplier multiplier
        moneyMultiplierMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), 0);
        // set magnet range multiplier
        magnetRangeMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString(), 0);

        //
        // set multiplier text
        //

        // set targeting chance multiplier text
        targetingChanceMultiplier.text = targetingChanceMultiplierPrefix + targetingChanceMultiplierValue.ToString();
        // set critical chance multiplier text
        criticalChanceMultiplier.text = criticalChanceMultiplierPrefix + criticalChanceMultiplierValue.ToString();
        // set critical damage multiplier text
        criticalDamageMultiplier.text = criticalDamageMultiplierPrefix + criticalDamageMultiplierValue.ToString();
        // set money multiplier multiplier text
        moneyMultiplierMultiplier.text = moneyMultiplierMultiplierPrefix + moneyMultiplierMultiplierValue.ToString();
        // set magnet range multiplier text
        magnetRangeMultiplier.text = magnetRangeMultiplierPrefix + magnetRangeMultiplierValue.ToString();

        //
        // set cost values
        //

        // set targeting chance cost and text
        targetingChanceCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost);
        if (targetingChanceMultiplierValue >= targetingChanceMaxMultiplier)
        {
            targetingChanceCost.text = max;

            // adjust multiplier to cap and adjust multiplier text
            PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChance.ToString(), 4);
            targetingChanceMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChance.ToString());
            targetingChanceMultiplier.text = targetingChanceMultiplierPrefix + targetingChanceMultiplierValue.ToString();

            // adjust stat and stat text
            targetingChanceStatValue = ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChance.ToString()));
            targetingChanceStat.text = targetingChanceStatPrefix + MathF.Round(targetingChanceStatValue, 2).ToString();

        }
        else
        {
            targetingChanceCost.text = targetingChanceCostPrefix + MoneyHandler.ConvertMoney(targetingChanceCostValue);
        }

        // set critical chance cost and text
        criticalChanceCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChanceCost.ToString(), ConfigUtils.CritChanceCost);
        if (criticalChanceMultiplierValue >= criticalChanceMaxMultiplier)
        {
            criticalChanceCost.text = max;

            // adjust multiplier to cap and adjust multiplier text
            PlayerPrefs.SetFloat(PlayerPrefNames.CriticalChance.ToString(), 4);
            criticalChanceMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChance.ToString());
            criticalChanceMultiplier.text = criticalChanceMultiplierPrefix + criticalChanceMultiplierValue.ToString();

            // adjust stat and stat text
            criticalChanceStatValue = ConfigUtils.Ship1CritChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChance.ToString()));
            criticalChanceStat.text = criticalChanceStatPrefix + MathF.Round(criticalChanceStatValue, 2).ToString();
        }
        else
        {
            criticalChanceCost.text = criticalChanceCostPrefix + MoneyHandler.ConvertMoney(criticalChanceCostValue);
        }

        // set critical damage cost and text
        criticalDamageCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamageCost.ToString(), ConfigUtils.CritDamageCost);
        if (criticalDamageMultiplierValue >= criticalDamageMaxMultiplier)
        {
            criticalDamageCost.text = max;

            // adjust multiplier to cap and adjust multiplier text
            PlayerPrefs.SetFloat(PlayerPrefNames.CriticalDamage.ToString(), 7);
            criticalDamageMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamage.ToString());
            criticalDamageMultiplier.text = criticalDamageMultiplierPrefix + criticalDamageMultiplierValue.ToString();

            // adjust stat and stat text
            criticalDamageStatValue = ConfigUtils.Ship1CritDamageMulti * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamage.ToString()));
            criticalDamageStat.text = criticalDamageStatPrefix + MathF.Round(criticalDamageStatValue, 2).ToString();
        }
        else
        {
            criticalDamageCost.text = criticalDamageCostPrefix + MoneyHandler.ConvertMoney(criticalDamageCostValue);
        }

        // set money multiplier cost and text
        moneyMultiplierCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplierCost.ToString(), ConfigUtils.MoneyMultiplierCost);
        if (moneyMultiplierMultiplierValue >= moneyMultiplierMaxMultiplier)
        {
            moneyMultiplierCost.text = max;

            // adjust multiplier to cap and adjust multiplier text
            PlayerPrefs.SetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), 9);
            moneyMultiplierMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString());
            moneyMultiplierMultiplier.text = moneyMultiplierMultiplierPrefix + moneyMultiplierMultiplierValue.ToString();

            // adjust stat and stat text
            moneyMultiplierStatValue = ConfigUtils.Ship1MoneyMultiplier * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString()));
            moneyMultiplierStat.text = moneyMultiplierStatPrefix + MathF.Round(moneyMultiplierStatValue, 2).ToString();
        }
        else
        {
            moneyMultiplierCost.text = moneyMultiplierCostPrefix + MoneyHandler.ConvertMoney(moneyMultiplierCostValue);
        }

        // set magnet range cost and text
        magnetRangeCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRangeCost.ToString(), ConfigUtils.MagnetRangeCost);
        if (magnetRangeMultiplierValue >= magnetRangeMaxMultiplier)
        {
            magnetRangeCost.text = max;

            // adjust multiplier to cap and adjust multiplier text
            PlayerPrefs.SetFloat(PlayerPrefNames.MagnetRange.ToString(), 7);
            magnetRangeMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString());
            magnetRangeMultiplier.text = magnetRangeMultiplierPrefix + magnetRangeMultiplierValue.ToString();

            // adjust stat and stat text
            magnetRangeStatValue = ConfigUtils.Ship1MagnetRange * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString()));
            magnetRangeStat.text = magnetRangeStatPrefix + MathF.Round(magnetRangeStatValue, 2).ToString();
        }
        else
        {
            magnetRangeCost.text = magnetRangeCostPrefix + MoneyHandler.ConvertMoney(magnetRangeCostValue);
        }
    }

    /// <summary>
    /// Sets store buttons interactability
    /// </summary>
    private void SetStoreButtons()
    {
        // player money
        float money = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());

        // check if player has enough money for purchases
        if (money < targetingChanceCostValue || targetingChanceMultiplierValue >= targetingChanceMaxMultiplier)
        {
            // disable the targeting chance button
            IncreaseTargetingChanceButton.enabled = false;
            IncreaseTargetingChanceButton.interactable = false;
        }
        else
        {
            // enable the targeting chance button
            IncreaseTargetingChanceButton.enabled = true;
            IncreaseTargetingChanceButton.interactable = true;
        }

        if (money < criticalChanceCostValue || criticalChanceMultiplierValue >= criticalChanceMaxMultiplier)
        {
            // disable the critical chance button
            IncreaseCriticalChanceButton.enabled = false;
            IncreaseCriticalChanceButton.interactable = false;
        }
        else
        {
            // enable the critical chance button
            IncreaseCriticalChanceButton.enabled = true;
            IncreaseCriticalChanceButton.interactable = true;
        }

        if (money < criticalDamageCostValue || criticalDamageMultiplierValue >= criticalDamageMaxMultiplier)
        {
            // disable the critical damage button
            IncreaseCriticalDamageButton.enabled = false;
            IncreaseCriticalDamageButton.interactable = false;
        }
        else
        {
            // enable the critical damage button
            IncreaseCriticalDamageButton.enabled = true;
            IncreaseCriticalDamageButton.interactable = true;
        }

        if (money < moneyMultiplierCostValue || moneyMultiplierMultiplierValue >= moneyMultiplierMaxMultiplier)
        {
            // disable the money multiplier button
            IncreaseMoneyMultiplierButton.enabled = false;
            IncreaseMoneyMultiplierButton.interactable = false;
        }
        else
        {
            // enable the money multiplier button
            IncreaseMoneyMultiplierButton.enabled = true;
            IncreaseMoneyMultiplierButton.interactable = true;
        }

        if (money < magnetRangeCostValue || magnetRangeMultiplierValue >= magnetRangeMaxMultiplier)
        {
            // disable the magnet range button
            IncreaseMagnetRangeButton.enabled = false;
            IncreaseMagnetRangeButton.interactable = false;
        }
        else
        {
            // enable the magnet range0 button
            IncreaseMagnetRangeButton.enabled = true;
            IncreaseMagnetRangeButton.interactable = true;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Handles store exit button
    /// </summary>
    public void HandleStoreExitButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("MainMenu");
    }

    /// <summary>
    /// Handles store increase targeting chance button
    /// </summary>
    public void HandleStoreIncreaseTargetingChanceButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString());

        // set targteing chance multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChance.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChance.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set targeting chance cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased critical chance button
    /// </summary>
    public void HandleStoreIncreaseCriticalChanceButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChanceCost.ToString());

        // set critical chance multiplier rounded to three demicals
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalChance.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChance.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set critical chance cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalChanceCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.CriticalChanceCost.ToString(), ConfigUtils.CritChanceCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store decreased critical damage button
    /// </summary>
    public void HandleStoreIncreaseCritcalDamageButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamageCost.ToString());

        // set critical damage multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalDamage.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamage.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set critical damage cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalDamageCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.CriticalDamageCost.ToString(), ConfigUtils.CritDamageCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased money multiplier button
    /// </summary>
    public void HandleStoreIncreaseMoneyMultiplierButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplierCost.ToString());

        // set money multiplier multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString()) + (ConfigUtils.UpgradeAmount), 3));

        // set money multiplier cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.MoneyMultiplierCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplierCost.ToString(), ConfigUtils.MoneyMultiplierCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased magnet range button
    /// </summary>
    public void HandleStoreIncreaseMagnetRangeButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRangeCost.ToString());

        // set magnet range multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.MagnetRange.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set magnet range cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.MagnetRangeCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRangeCost.ToString(), ConfigUtils.MagnetRangeCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    #endregion
}