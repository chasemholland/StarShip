using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Store menu
/// </summary>
public class Store : FloatEventInvoker
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI moneyValueText;
    float moneyValue;
    string moneyValuePrefix = "SPACE COINS: ";

    [SerializeField]
    TextMeshProUGUI healthStat;
    float healthStatValue;
    int healthLevel;
    string healthStatPrefix = "HEALTH: ";

    [SerializeField]
    TextMeshProUGUI moveStat;
    float moveStatValue;
    int moveLevel;
    string moveStatPrefix = "MOVE SPEED: ";

    [SerializeField]
    TextMeshProUGUI laserDamageStat;
    float laserDamageStatValue;
    int laserDamageLevel;
    string laserDamageStatPrefix = "LASER DAMAGE: ";

    [SerializeField]
    TextMeshProUGUI laserCooldownStat;
    float laserCooldownStatValue;
    int laserCooldownLevel;
    string laserCooldownStatPrefix = "LASER COOLDOWN: ";

    [SerializeField]
    TextMeshProUGUI laserSpeedStat;
    float laserSpeedStatValue;
    int laserSpeedLevel;
    string laserSpeedStatPrefix = "LASER SPEED: ";

    [SerializeField]
    TextMeshProUGUI healthCost;
    float healthCostValue;
    string healthCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI moveCost;
    float moveCostValue;
    string moveCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI laserDamageCost;
    float laserDamageCostValue;
    string laserDamageCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI laserCooldownCost;
    float laserCooldownCostValue;
    string laserCooldownCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI laserSpeedCost;
    float laserSpeedCostValue;
    string laserSpeedCostPrefix = "$";

    [SerializeField]
    TextMeshProUGUI healthMultiplier;
    float healthMultiplierValue;
    string healthMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI moveMultiplier;
    float moveMultiplierValue;
    string moveMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI laserDamageMultiplier;
    float laserDamageMultiplierValue;
    string laserDamageMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI laserCooldownMultiplier;
    float laserCooldownMultiplierValue;
    string laserCooldownMultiplierPrefix = "x ";

    [SerializeField]
    TextMeshProUGUI laserSpeedMultiplier;
    float laserSpeedMultiplierValue;
    string laserSpeedMultiplierPrefix = "x ";

    // default cost max text
    float maxMultiplier;
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

        // get max multiplier
        maxMultiplier = ConfigUtils.StoreMaxMultiplier;

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

        // set health value
        healthStatValue = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
        // set move speed value
        moveStatValue = ConfigUtils.Ship1MoveSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0));
        // set laser damage value
        laserDamageStatValue = ConfigUtils.Ship1LaserDamage * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0));
        // set laser cooldown value
        laserCooldownStatValue = ConfigUtils.Ship1LaserCooldown / (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0));
        // set laser speed value
        laserSpeedStatValue = ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0));

        //
        // set player stats text
        //

        // set health text
        healthStat.text = healthStatPrefix + MathF.Round(healthStatValue, 2).ToString();
        // set move speed text
        moveStat.text = moveStatPrefix + MathF.Round(moveStatValue, 2).ToString();
        // set laser damage text
        laserDamageStat.text = laserDamageStatPrefix + MathF.Round(laserDamageStatValue, 2).ToString();
        // set laser cooldown text
        laserCooldownStat.text = laserCooldownStatPrefix + MathF.Round(laserCooldownStatValue, 2).ToString();
        // set laser speed text
        laserSpeedStat.text = laserSpeedStatPrefix + MathF.Round(laserSpeedStatValue, 2).ToString();

        //
        // set multiplier values
        //

        // set health multiplier
        healthMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0);
        // set move speed multiplier
        moveMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0);
        // set laser damage multiplier
        laserDamageMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0);
        // set laser coolodwn multiplier
        laserCooldownMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0);
        // set laser speed multiplier
        laserSpeedMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0);

        //
        // set multiplier text
        //

        // set health multiplier text
        healthMultiplier.text = healthMultiplierPrefix + healthMultiplierValue.ToString();
        // set move speed multiplier text
        moveMultiplier.text = moveMultiplierPrefix + moveMultiplierValue.ToString();
        // set laser damage multiplier text
        laserDamageMultiplier.text = laserDamageMultiplierPrefix + laserDamageMultiplierValue.ToString();
        // set laser coolodwn multiplier text
        laserCooldownMultiplier.text = laserCooldownMultiplierPrefix + laserCooldownMultiplierValue.ToString();
        // set laser speed multiplier text
        laserSpeedMultiplier.text = laserSpeedMultiplierPrefix + laserSpeedMultiplierValue.ToString();

        //
        // set cost values
        //

        // set health cost and text
        healthCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost);
        if (healthMultiplierValue >= maxMultiplier)
        {
            healthCost.text = max;
        }
        else
        {
            healthCost.text = healthCost.text = healthCostPrefix + MoneyHandler.ConvertMoney(healthCostValue);
        }

        // set move speed cost and text
        moveCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost);
        if (moveMultiplierValue >= maxMultiplier)
        {
            moveCost.text = max;
        }
        else
        {
            moveCost.text = moveCostPrefix + MoneyHandler.ConvertMoney(moveCostValue);
        }

        // set laser damage cost and text
        laserDamageCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost);
        if (laserDamageMultiplierValue >= maxMultiplier)
        {
            laserDamageCost.text = max;
        }
        else
        {
            laserDamageCost.text = laserDamageCostPrefix + MoneyHandler.ConvertMoney(laserDamageCostValue);
        }

        // set laser cooldown cost and text
        laserCooldownCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost);
        if (laserCooldownMultiplierValue >= maxMultiplier)
        {
            laserCooldownCost.text = max;
        }
        else
        {
            laserCooldownCost.text = laserCooldownCostPrefix + MoneyHandler.ConvertMoney(laserCooldownCostValue);
        }

        // set laser speed cost and text
        laserSpeedCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost);
        if (laserSpeedMultiplierValue >= maxMultiplier)
        {
            laserSpeedCost.text = max;
        }
        else
        {
            laserSpeedCost.text = laserSpeedCostPrefix + MoneyHandler.ConvertMoney(laserSpeedCostValue);
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
        if (money < healthCostValue || healthMultiplierValue >= maxMultiplier)
        {
            // disable the health button
            Button button = GameObject.FindGameObjectWithTag("IncreaseHealth").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;
        }
        else
        {
            // enable the health button
            Button button = GameObject.FindGameObjectWithTag("IncreaseHealth").GetComponent<Button>();
            button.enabled = true;
            button.interactable = true;
        }

        if (money < moveCostValue || moveMultiplierValue >= maxMultiplier)
        {
            // disable the move speed button
            Button button = GameObject.FindGameObjectWithTag("IncreaseMoveSpeed").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;
        }
        else
        {
            // enable the move speed button
            Button button = GameObject.FindGameObjectWithTag("IncreaseMoveSpeed").GetComponent<Button>();
            button.enabled = true;
            button.interactable = true;
        }

        if (money < laserDamageCostValue || laserDamageMultiplierValue >= maxMultiplier)
        {
            // diasable the laser damage button
            Button button = GameObject.FindGameObjectWithTag("IncreaseLaserDamage").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;
        }
        else
        {
            // enable the laser damage button
            Button button = GameObject.FindGameObjectWithTag("IncreaseLaserDamage").GetComponent<Button>();
            button.enabled = true;
            button.interactable = true;
        }

        if (money < laserCooldownCostValue || laserCooldownMultiplierValue >= maxMultiplier)
        {
            // disable the laser cooldown button
            Button button = GameObject.FindGameObjectWithTag("DecreaseLaserCooldown").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;
        }
        else
        {
            // enable the laser cooldown button
            Button button = GameObject.FindGameObjectWithTag("DecreaseLaserCooldown").GetComponent<Button>();
            button.enabled = true;
            button.interactable = true;
        }

        if (money < laserSpeedCostValue || laserSpeedMultiplierValue >= maxMultiplier)
        {
            // disable the laser speed button
            Button button = GameObject.FindGameObjectWithTag("IncreaseLaserSpeed").GetComponent<Button>();
            button.enabled = false;
            button.interactable = false;
        }
        else
        {
            // enable the laser speed button
            Button button = GameObject.FindGameObjectWithTag("IncreaseLaserSpeed").GetComponent<Button>();
            button.enabled = true;
            button.interactable = true;
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
    /// Handles store increase laser speed button
    /// </summary>
    public void HandleStoreIncreaseLaserSpeedButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString());

        // set laser speed multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set laser speed cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased laser damage button
    /// </summary>
    public void HandleStoreIncreaseLaserDamageButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString());

        // set laser damage multiplier rounded to three demicals
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set laser damage cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserDamageCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store decreased laser cooldown button
    /// </summary>
    public void HandleStoreDecreaseLaserCooldownButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString());

        // set laser cooldown multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set laser cooldown cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased move speed button
    /// </summary>
    public void HandleStoreIncreaseMoveSpeedButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString());

        // set move speed multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set move speed cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.MoveCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Handles store increased health the button
    /// </summary>
    public void HandleStoreIncreaseHealthButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString());

        // set health multiplier rounded to three decimals
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString()) + ConfigUtils.UpgradeAmount, 3));

        // set health cost rounded to whole number
        PlayerPrefs.SetFloat(PlayerPrefNames.HealthCost.ToString(), MathF.Round(PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost) * ConfigUtils.UpgradeCostMultiplier, 0));

        // adjust text and money
        HandlePurchase();

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    #endregion
}
