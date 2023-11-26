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

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set levels
        SetLevels();

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
        /*
        if (moneyValue != PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString()))
        {
            // update values
            UpdateValues();

            // set money in player prefs
            PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

            // set money text
            moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

            // set the buttons
            SetStoreButtons();
        }
        */

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles text and money adjustments after purchase
    /// </summary>
    private void HandlePurchase()
    {
        // update values
        UpdateValues();

        // set money in player prefs
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

        // set money text
        moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

        // set the buttons
        SetStoreButtons();
    }

    /// <summary>
    /// Sets the texts
    /// </summary>
    private void SetText()
    {
        // set money value text
        moneyValueText.text = moneyValuePrefix + MoneyHandler.ConvertMoney(moneyValue);

        // set player stats text
        healthStat.text = healthStatPrefix + MathF.Round(healthStatValue, 2).ToString();
        moveStat.text = moveStatPrefix + MathF.Round(moveStatValue, 2).ToString();
        laserDamageStat.text = laserDamageStatPrefix + MathF.Round(laserDamageStatValue, 2).ToString();
        laserCooldownStat.text = laserCooldownStatPrefix + MathF.Round(laserCooldownStatValue, 2).ToString();
        laserSpeedStat.text = laserSpeedStatPrefix + MathF.Round(laserSpeedStatValue, 2).ToString();

        // set cost text
        healthCost.text = healthCostPrefix + MoneyHandler.ConvertMoney(healthCostValue);
        moveCost.text = moveCostPrefix + MoneyHandler.ConvertMoney(moveCostValue);
        laserDamageCost.text = laserDamageCostPrefix + MoneyHandler.ConvertMoney(laserDamageCostValue);
        laserCooldownCost.text = laserCooldownCostPrefix + MoneyHandler.ConvertMoney(laserCooldownCostValue);
        laserSpeedCost.text = laserSpeedCostPrefix + MoneyHandler.ConvertMoney(laserSpeedCostValue);

        // set multiplier text
        healthMultiplier.text = healthMultiplierPrefix + healthMultiplierValue.ToString();
        moveMultiplier.text = moveMultiplierPrefix + moveMultiplierValue.ToString();
        laserDamageMultiplier.text = laserDamageMultiplierPrefix + laserDamageMultiplierValue.ToString();
        laserCooldownMultiplier.text = laserCooldownMultiplierPrefix + laserCooldownMultiplierValue.ToString();
        laserSpeedMultiplier.text = laserSpeedMultiplierPrefix + laserSpeedMultiplierValue.ToString();
    }

    /// <summary>
    /// Sets initial values
    /// </summary>
    private void SetValues()
    {
        // set money value
        moneyValue = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);

        // set player stats values
        healthStatValue = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
        moveStatValue = ConfigUtils.Ship1MoveSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0));
        laserDamageStatValue = ConfigUtils.Ship1LaserDamage * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0));
        laserCooldownStatValue = ConfigUtils.Ship1LaserCooldown / (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0));
        laserSpeedStatValue = ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0));

        // set cost values
        healthCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost);
        moveCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost);
        laserDamageCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost);
        laserCooldownCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost);
        laserSpeedCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost);

        // set multiplier values
        healthMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0);
        moveMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0);
        laserDamageMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0);
        laserCooldownMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0);
        laserSpeedMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0);
    }

    /// <summary>
    /// Sets or retrieves stat levels
    /// </summary>
    private void SetLevels()
    {
        // base level 1 if no value stored in player prefs
        healthLevel = PlayerPrefs.GetInt(PlayerPrefNames.ShipHealthLevel.ToString(), 1);
        moveLevel = PlayerPrefs.GetInt(PlayerPrefNames.ShipMoveSpeedLevel.ToString(), 1);
        laserDamageLevel = PlayerPrefs.GetInt(PlayerPrefNames.ShipLaserDamageLevel.ToString(), 1);
        laserCooldownLevel = PlayerPrefs.GetInt(PlayerPrefNames.ShipLaserCooldownLevel.ToString(), 1);
        laserSpeedLevel = PlayerPrefs.GetInt(PlayerPrefNames.ShipLaserSpeedLevel.ToString(), 1);
    }

    /// <summary>
    /// Updates values
    /// </summary>
    private void UpdateValues()
    {
        // update stats
        if (healthStatValue != ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0)))
        {
            healthStatValue = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
            healthStat.text = healthStatPrefix + MathF.Round(healthStatValue, 2).ToString();
        }
        if (moveStatValue != ConfigUtils.Ship1MoveSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0)))
        {
            moveStatValue = ConfigUtils.Ship1MoveSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0));
            moveStat.text = moveStatPrefix + MathF.Round(moveStatValue, 2).ToString();
        }
        if (laserDamageStatValue != ConfigUtils.Ship1LaserDamage * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0)))
        {
            laserDamageStatValue = ConfigUtils.Ship1LaserDamage * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0));
            laserDamageStat.text = laserDamageStatPrefix + MathF.Round(laserDamageStatValue).ToString();
        }
        if (laserCooldownStatValue != ConfigUtils.Ship1LaserCooldown / (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0)))
        {
            laserCooldownStatValue = ConfigUtils.Ship1LaserCooldown / (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0));
            laserCooldownStat.text = laserCooldownStatPrefix + MathF.Round(laserCooldownStatValue, 2).ToString();
        }
        if (laserSpeedStatValue != ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0)))
        {
            laserSpeedStatValue = ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0));
            laserSpeedStat.text = laserSpeedStatPrefix + MathF.Round(laserSpeedStatValue, 2).ToString();
        }

        // update cost
        if (healthCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost))
        {
            healthCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString());
            healthCost.text = healthCostPrefix + MoneyHandler.ConvertMoney(healthCostValue);
        }
        if (moveCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost))
        {
            moveCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString());
            moveCost.text = moveCostPrefix + MoneyHandler.ConvertMoney(moveCostValue);
        }
        if (laserDamageCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost))
        {
            laserDamageCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString());
            laserDamageCost.text = laserDamageCostPrefix + MoneyHandler.ConvertMoney(laserDamageCostValue);
        }
        if (laserCooldownCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost))
        {
            laserCooldownCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString());
            laserCooldownCost.text = laserCooldownCostPrefix + MoneyHandler.ConvertMoney(laserCooldownCostValue);
        }
        if (laserSpeedCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost))
        {
            laserSpeedCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString());
            laserSpeedCost.text = laserSpeedCostPrefix + MoneyHandler.ConvertMoney(laserSpeedCostValue);
        }

        // update mutiplier
        if (healthMultiplierValue != (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0)))
        {
            healthMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0);
            healthMultiplier.text = healthMultiplierPrefix + healthMultiplierValue.ToString();
        }
        if (moveMultiplierValue != (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0)))
        {
            moveMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0);
            moveMultiplier.text = moveMultiplierPrefix + moveMultiplierValue.ToString();
        }
        if (laserDamageMultiplierValue != 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0))
        {
            laserDamageMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0);
            laserDamageMultiplier.text = laserDamageMultiplierPrefix + laserDamageMultiplierValue.ToString();
        }
        if (laserCooldownMultiplierValue != 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0))
        {
            laserCooldownMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0);
            laserCooldownMultiplier.text = laserCooldownMultiplierPrefix + laserCooldownMultiplierValue.ToString();
        }
        if (laserSpeedMultiplierValue != 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0))
        {
            laserSpeedMultiplierValue = 1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0);
            laserSpeedMultiplier.text = laserSpeedMultiplierPrefix + laserSpeedMultiplierValue.ToString();
        }
    }

    private void SetStoreButtons()
    {
        // player money
        float money = PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString());

        // check if player has enough money for purchases
        if (money < healthCostValue)
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

        if (money < moveCostValue)
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

        if (money < laserDamageCostValue)
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

        if (money < laserCooldownCostValue)
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

        if (money < laserSpeedCostValue)
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

        // load main menu
        SceneManager.LoadScene("MainMenu");
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
