using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/// <summary>
/// Store menu
/// </summary>
public class Store : IntEventInvoker
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI moneyValueText;
    int moneyValue;
    string moneyValuePrefix = "SPACE COINS: ";

    [SerializeField]
    TextMeshProUGUI healthStat;
    float healthStatValue;
    string healthStatPrefix = "HEALTH: ";

    [SerializeField]
    TextMeshProUGUI moveStat;
    float moveStatValue;
    string moveStatPrefix = "MOVE SPEED: ";

    [SerializeField]
    TextMeshProUGUI laserDamageStat;
    float laserDamageStatValue;
    string laserDamageStatPrefix = "LASER DAMAGE: ";

    [SerializeField]
    TextMeshProUGUI laserCooldownStat;
    float laserCooldownStatValue;
    string laserCooldownStatPrefix = "LASER COOLDOWN: ";

    [SerializeField]
    TextMeshProUGUI laserSpeedStat;
    float laserSpeedStatValue;
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
            laserDamageStat.text = laserDamageStatPrefix + MathF.Round(laserDamageStatValue, 2).ToString();
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
            healthCost.text = healthCostPrefix + MathF.Round(healthCostValue, 0).ToString();
        }
        if (moveCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost))
        {
            moveCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString());
            moveCost.text = moveCostPrefix + MathF.Round(moveCostValue, 0).ToString();
        }
        if (laserDamageCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost))
        {
            laserDamageCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString());
            laserDamageCost.text = laserDamageCostPrefix + MathF.Round(laserDamageCostValue, 0).ToString();
        }
        if (laserCooldownCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost))
        {
            laserCooldownCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString());
            laserCooldownCost.text = laserCooldownCostPrefix + MathF.Round(laserCooldownCostValue, 0).ToString();
        }
        if (laserSpeedCostValue != PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost))
        {
            laserSpeedCostValue = PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString());
            laserSpeedCost.text = laserSpeedCostPrefix + MathF.Round(laserSpeedCostValue, 0).ToString();
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

        if (moneyValue != PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()))
        {
            // set money in player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.PlayerMoney.ToString(), moneyValue);

            // set money text
            moneyValueText.text = moneyValuePrefix + moneyValue.ToString();

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
        moneyValueText.text = moneyValuePrefix + moneyValue.ToString();

        // set player stats text
        healthStat.text = healthStatPrefix + MathF.Round(healthStatValue, 2).ToString();
        moveStat.text = moveStatPrefix + MathF.Round(moveStatValue, 2).ToString();
        laserDamageStat.text = laserDamageStatPrefix + MathF.Round(laserDamageStatValue, 2).ToString();
        laserCooldownStat.text = laserCooldownStatPrefix + MathF.Round(laserCooldownStatValue, 2).ToString();
        laserSpeedStat.text = laserSpeedStatPrefix + MathF.Round(laserSpeedStatValue, 2).ToString();

        // set cost text
        healthCost.text = healthCostPrefix + MathF.Round(healthCostValue, 0).ToString();
        moveCost.text = moveCostPrefix + MathF.Round(moveCostValue, 0).ToString();
        laserDamageCost.text = laserDamageCostPrefix + MathF.Round(laserDamageCostValue, 0).ToString();
        laserCooldownCost.text = laserCooldownCostPrefix + MathF.Round(laserCooldownCostValue, 0).ToString();
        laserSpeedCost.text = laserSpeedCostPrefix + MathF.Round(laserSpeedCostValue, 0).ToString();

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
        moneyValue = PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString(), 0);

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

    private void SetStoreButtons()
    {
        if (PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()) < healthCostValue)
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

        if (PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()) < moveCostValue)
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

        if (PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()) < laserDamageCostValue)
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

        if (PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()) < laserCooldownCostValue)
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

        if (PlayerPrefs.GetInt(PlayerPrefNames.PlayerMoney.ToString()) < laserSpeedCostValue)
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

        Destroy(GameObject.FindGameObjectWithTag("StoreMenu"));
    }

    /// <summary>
    /// Handles store increase laser speed button
    /// </summary>
    public void HandleStoreIncreaseLaserSpeedButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // decrease money
        moneyValue -= (int)PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString());

        // set laser speed multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString()) + ConfigUtils.UpgradeAmount);

        // set health cost
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost) * ConfigUtils.UpgradeCostMultiplier);

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
        moneyValue -= (int)PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString());

        // set laser damage multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString()) + ConfigUtils.UpgradeAmount);

        // set health cost
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserDamageCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost) * ConfigUtils.UpgradeCostMultiplier);

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
        moneyValue -= (int)PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString());

        // set laser cooldown multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString()) + ConfigUtils.UpgradeAmount);

        // set health cost
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost) * ConfigUtils.UpgradeCostMultiplier);

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
        moneyValue -= (int)PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString());

        // set move speed multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString()) + ConfigUtils.UpgradeAmount);

        // set move speed cost
        PlayerPrefs.SetFloat(PlayerPrefNames.MoveCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost) * ConfigUtils.UpgradeCostMultiplier);

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
        moneyValue -= (int)PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString());

        // set health multiplier
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString()) + ConfigUtils.UpgradeAmount);

        // set health cost
        PlayerPrefs.SetFloat(PlayerPrefNames.HealthCost.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost) * ConfigUtils.UpgradeCostMultiplier);

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

    }

    #endregion
}
