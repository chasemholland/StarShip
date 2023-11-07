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
    int bonusCoinsValue;
    string bonusCoinsPrefix = "BONUS COINS: ";

    [SerializeField]
    TextMeshProUGUI spaceCoinsTotalText;
    int spaceCoinsTotalValue;
    string spaceCoinsTotalPrefix = "SPACE COINS: ";

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
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
        mothershipsDefeatedValue = PlayerPrefs.GetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0);
        bonusCoinsValue = (5 * aliensDefeatedValue) + (500 * mothershipsDefeatedValue);
        spaceCoinsTotalValue = PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString(), 0) + bonusCoinsValue;

        // set new player money value
        PlayerPrefs.SetInt(PlayerPrefNames.PlayerMoney.ToString(), spaceCoinsTotalValue);
    }

    /// <summary>
    /// Sets the text
    /// </summary>
    private void SetText()
    {
        roundsCompletedText.text = roundsCompletedPrefix + roundsCompletedValue.ToString();
        aliensDefeatedText.text = aliensDefeatedPrefix + aliensDefeatedValue.ToString();
        mothershipsDefeatedText.text = mothershipsDefeatedPrefix + mothershipsDefeatedValue.ToString();
        bonusCoinsText.text = bonusCoinsPrefix + "(" + aliensDefeatedValue.ToString() + " x 5) + (" + mothershipsDefeatedValue + " x 500) = " + bonusCoinsValue.ToString();
        spaceCoinsTotalText.text = spaceCoinsTotalPrefix + spaceCoinsTotalValue.ToString();
    }

    /// <summary>
    /// Resets the player prefs
    /// </summary>
    private void RemoveStats()
    {
        PlayerPrefs.SetInt(PlayerPrefNames.RoundsCompleted.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0);
    }

    #endregion
}
