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
    TextMeshProUGUI moneyValueText;
    float moneyValue;
    string moneyValuePrefix = "SPACE COINS: ";

    [SerializeField]
    TextMeshProUGUI targetingChanceStat;
    float targetingChanceStatValue;
    string targetingChanceStatPrefix = "TARGETING CHANCE: ";

    [SerializeField]
    TextMeshProUGUI targetingSystemCost;
    float targetingSystemCostValue;
    string targetingSystemCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI targetingChanceCost;
    float targetingChanceCostValue;
    string targetingChanceCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI targetingChanceMultiplier;
    float targetingChanceMultiplierValue;
    string targetingChanceMultiplierPrefix = "x ";

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set values
        SetValues();

        // set texts
        SetText();

        // set store buttons
        SetStoreButtons();

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // update stats
        if (targetingChanceStatValue != ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0)))
        {
            targetingChanceStatValue = ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0));
            targetingChanceStat.text = targetingChanceStatPrefix + MathF.Round(targetingChanceStatValue, 2).ToString();
        }

        // update cost
        if (targetingChanceCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost))
        {
            targetingChanceCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost);
            targetingChanceCost.text = targetingChanceCostPrefix + MathF.Round(targetingChanceCostValue, 0).ToString();
        }

        // update mutiplier
        if (targetingChanceMultiplierValue != (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0)))
        {
            targetingChanceMultiplierValue = (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0));
            targetingChanceMultiplier.text = targetingChanceMultiplierPrefix + targetingChanceMultiplierValue.ToString();
        }

        if (moneyValue != PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString()))
        {
            // set money in player prefs
            PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

            // set money text
            moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

            // set the buttons
            SetStoreButtons();
        }

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the texts
    /// </summary>
    private void SetText()
    {
        // set money value text
        moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

        if (PlayerPrefs.GetInt(PlayerPrefNames.HasTargetingSystem.ToString(), 0) == 1)
        {
            // set player stats text
            targetingChanceStat.text = targetingChanceStatPrefix + MathF.Round(targetingChanceStatValue, 2).ToString();
            // set cost text
            targetingChanceCost.text = targetingChanceCostPrefix + MathF.Round(targetingChanceCostValue, 0).ToString();
            targetingSystemCost.text = "";
            // set multiplier text
            targetingChanceMultiplier.text = targetingChanceMultiplierPrefix + targetingChanceMultiplierValue.ToString();
        }
        else
        {
            // set player stats text
            targetingChanceStat.text = targetingChanceStatPrefix;
            // set cost text
            targetingChanceCost.text = "";
            targetingSystemCost.text = targetingSystemCostPrefix + targetingSystemCostValue.ToString();
            // set multiplier text
            targetingChanceMultiplier.text = "";
        }
        
    }

    /// <summary>
    /// Sets initial values
    /// </summary>
    private void SetValues()
    {
        // set money value
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);

        // initial updgrade purchase
        targetingSystemCostValue = ConfigUtils.TargetingSystemCost;

        // set player stats values
        targetingChanceStatValue = ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0));

        // set cost values
        targetingChanceCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost);

        // set multiplier values
        targetingChanceMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0);
    }

    private void SetStoreButtons()
    {
        // get player money
        float money = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());

        // initial upgrade purchase
        if (PlayerPrefs.GetInt(PlayerPrefNames.HasTargetingSystem.ToString()) == 1)
        {
            // disable the targeting system button
            Button buttonSys = GameObject.FindGameObjectWithTag("TargetingSystem").GetComponent<Button>();
            buttonSys.enabled = false;
            buttonSys.interactable = false;

            // check if player has enough money
            if (money < targetingChanceCostValue)
            {
                // disable the targeting chance button
                Button button = GameObject.FindGameObjectWithTag("TargetingChance").GetComponent<Button>();
                button.enabled = false;
                button.interactable = false;
            }
            else
            {
                // enable the targeting chance button
                Button button = GameObject.FindGameObjectWithTag("TargetingChance").GetComponent<Button>();
                button.enabled = true;
                button.interactable = true;
            } 
        }
        else
        {
            // disable the targeting chance button
            Button button = GameObject.FindGameObjectWithTag("TargetingChance").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;

            if (money < targetingSystemCostValue)
            {
                // disable the targeting system button
                Button buttonSys = GameObject.FindGameObjectWithTag("TargetingSystem").GetComponent<Button>();
                buttonSys.enabled = false;
                buttonSys.interactable = false;
            }
            else
            {
                // enable the targeting system button
                Button buttonSys = GameObject.FindGameObjectWithTag("TargetingSystem").GetComponent<Button>();
                buttonSys.enabled = true;
                buttonSys.interactable = true;
            }  
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

        // load main menu
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Handles store targeting system button
    /// </summary>
    public void HandleTargetingSysytemButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // set health cost
        PlayerPrefs.SetInt(PlayerPrefNames.HasTargetingSystem.ToString(), 1);

        // decrease money
        moneyValue -= ConfigUtils.TargetingSystemCost;

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increase targeting chance button
    /// </summary>
    public void HandleTargetingChanceButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString()), 0);

        // set targeting chnace multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString()) + ConfigUtils.UpgradeAmount);

        // set targeting chance cost
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost) * ConfigUtils.UpgradeCostMultiplier);

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    #endregion
}
