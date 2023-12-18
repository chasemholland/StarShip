using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Invasion stats
/// </summary>
public class InvasionOver : MonoBehaviour
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI roundsCompletedText;
    int roundsCompletedValue;
    string roundsCompletedPrefix = "ROUNDS COMPLETED: ";

    [SerializeField]
    TextMeshProUGUI aliensDefeatedText;
    int aliensDefeatedValue;
    string aliensDefeatedPrefix = "ALIENS DEFEATED: ";

    [SerializeField]
    TextMeshProUGUI mothershipsDefeatedText;
    int mothershipsDefeatedValue;
    string mothershipsDefeatedPrefix = "MOTHERSHIPS DEFEATED: ";

    [SerializeField]
    TextMeshProUGUI bonusCoinsText;
    float bonusCoinsValue;

    [SerializeField]
    TextMeshProUGUI bonusCoinsCalcText;
    int redsDefeatedValue;
    int greensDefeatedValue;

    [SerializeField]
    TextMeshProUGUI spaceCoinsTotalText;
    float spaceCoinsTotalValue;
    string spaceCoinsTotalPrefix = "SPACE COINS: ";

    float moneyMultiplier;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        // play background music
        if (LoopingAudioManager.Playing == AudioName.GamePlayAmbient)
        {
            LoopingAudioManager.Switch(AudioName.Ambient);
        }

        // set money multiplier
        if (PlayerPrefs.GetInt(PlayerPrefNames.HasMoneyMultiplier.ToString(), 0) == 1)
        {
            moneyMultiplier = ConfigUtils.Ship1MoneyMultiplier * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), 0));
        }
        else
        {
            // default to 1, no multiplier
            moneyMultiplier = 1;
        }

        // set values
        SetValues();

        // set text
        SetText();

        // remove stats from player prefs
        RemoveStats();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the values
    /// </summary>
    private void SetValues()
    {
        roundsCompletedValue = PlayerPrefs.GetInt(PlayerPrefNames.RoundsCompleted.ToString(), 0);
        aliensDefeatedValue = PlayerPrefs.GetInt(PlayerPrefNames.AliensDefeated.ToString(), 0);
        redsDefeatedValue = PlayerPrefs.GetInt(PlayerPrefNames.RedsDefeated.ToString(), 0);
        greensDefeatedValue = PlayerPrefs.GetInt(PlayerPrefNames.GreensDefeated.ToString(), 0);
        mothershipsDefeatedValue = PlayerPrefs.GetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0);
        bonusCoinsValue = MathF.Round(((50 * redsDefeatedValue) + (100 * greensDefeatedValue) + (2000 * mothershipsDefeatedValue)) * moneyMultiplier, 0);
        spaceCoinsTotalValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0) + bonusCoinsValue;

        // set new player money value
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), spaceCoinsTotalValue);
    }

    /// <summary>
    /// Sets the text
    /// </summary>
    private void SetText()
    {
        roundsCompletedText.text = roundsCompletedPrefix + roundsCompletedValue.ToString();
        aliensDefeatedText.text = aliensDefeatedPrefix + aliensDefeatedValue.ToString();
        mothershipsDefeatedText.text = mothershipsDefeatedPrefix + mothershipsDefeatedValue.ToString();
        bonusCoinsText.text = MoneyHandler.ConvertMoney(bonusCoinsValue);
        bonusCoinsCalcText.text = "Reds(" + redsDefeatedValue.ToString() + " x 50) Greens(" + greensDefeatedValue.ToString() + " x 100) Blues(" + mothershipsDefeatedValue.ToString() + " x 2000)" + " x " + moneyMultiplier;
        spaceCoinsTotalText.text = spaceCoinsTotalPrefix + MoneyHandler.ConvertMoney(spaceCoinsTotalValue);
    }

    /// <summary>
    /// Resets the player prefs
    /// </summary>
    private void RemoveStats()
    {
        PlayerPrefs.SetInt(PlayerPrefNames.RoundsCompleted.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.RedsDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.GreensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0);
    }

    #endregion
}
