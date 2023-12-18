using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

/// <summary>
/// Configuration data retrieved from csv or set to default values
/// </summary>
public class ConfigData
{
    #region Fields

    // file to load
    const string CONFIG_NAME = "ConfigData.csv";

    // dictionary to hold configuration data
    Dictionary<ConfigDataName, float> values =
        new Dictionary<ConfigDataName, float>();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the initial max shield value
    /// </summary>
    public float Ship1MaxShield
    {
        get { return values[ConfigDataName.Ship1MaxShield]; }
    }

    /// <summary>
    /// Gets the basic ship laser shoot cooldown
    /// </summary>
    public float Ship1LaserCooldown
    {
        get { return values[ConfigDataName.Ship1LaserCooldown]; }
    }

    /// <summary>
    /// Gets the speed of lasers
    /// </summary>
    public float Ship1LaserSpeed
    {
        get { return values[ConfigDataName.Ship1LaserSpeed]; }
    }

    /// <summary>
    /// Gets the basic ship movement speed
    /// </summary>
    public float Ship1MoveSpeed
    {
        get { return values[ConfigDataName.Ship1MoveSpeed]; }
    }

    /// <summary>
    /// Gets the basic ship health
    /// </summary>
    public float Ship1LifeAmount
    {
        get { return values[ConfigDataName.Ship1LifeAmount]; }
    }

    /// <summary>
    /// Gets the laser damage
    /// </summary>
    public float Ship1LaserDamage
    {
        get { return values[ConfigDataName.Ship1LaserDamage]; }
    }

    /// <summary>
    /// Gets the targeting chance
    /// </summary>
    public float Ship1TargetingChance
    {
        get { return values[ConfigDataName.Ship1TargetingChance]; }
    }

    /// <summary>
    /// Gets the critical hit chance
    /// </summary>
    public float Ship1CritChance
    {
        get { return values[ConfigDataName.Ship1CritChance]; }
    }

    /// <summary>
    /// Gets the critical hit multiplier
    /// </summary>
    public float Ship1CritDamageMulti
    {
        get { return values[ConfigDataName.Ship1CritDamageMulti]; }
    }

    /// <summary>
    /// Gets the money multiplier
    /// </summary>
    public float Ship1MoneyMultiplier
    {
        get { return values[ConfigDataName.Ship1MoneyMultiplier]; }
    }

    /// <summary>
    /// Gets the magnet range
    /// </summary>
    public float Ship1MagnetRange
    {
        get { return values[ConfigDataName.Ship1MagnetRange]; }
    }

    /// <summary>
    /// Gets the alien laser damage
    /// </summary>
    public float Alien1LaserDamage
    {
        get { return values[ConfigDataName.Alien1LaserDamage]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public float Alien1LaserCooldownMin
    {
        get { return values[ConfigDataName.Alien1LaserCooldownMin]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public float Alien1LaserCooldownMax
    {
        get { return values[ConfigDataName.Alien1LaserCooldownMax]; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public float Alien1LaserSpeed
    {
        get { return values[ConfigDataName.Alien1LaserSpeed]; }
    }

    /// <summary>
    /// Gets the alien x movement speed
    /// </summary>
    public float Alien1MoveSpeedX
    {
        get { return values[ConfigDataName.Alien1MoveSpeedX]; }
    }

    /// <summary>
    /// Gets the alien y movement speed
    /// </summary>
    public float Alien1MoveSpeedY
    {
        get { return values[ConfigDataName.Alien1MoveSpeedY]; }
    }

    /// <summary>
    /// Gets the minimum alien movement delay
    /// </summary>
    public float Alien1MoveDelayMin
    {
        get { return values[ConfigDataName.Alien1MoveDelayMin]; }
    }

    /// <summary>
    /// Gets the max alien movement delay
    /// </summary>
    public float Alien1MoveDelayMax
    {
        get { return values[ConfigDataName.Alien1MoveDelayMax]; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public float Alien1LifeAmount
    {
        get { return values[ConfigDataName.Alien1LifeAmount]; }
    }

    /// <summary>
    /// Gets the alien laser damage
    /// </summary>
    public float Alien2LaserDamage
    {
        get { return values[ConfigDataName.Alien2LaserDamage]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public float Alien2LaserCooldownMin
    {
        get { return values[ConfigDataName.Alien2LaserCooldownMin]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public float Alien2LaserCooldownMax
    {
        get { return values[ConfigDataName.Alien2LaserCooldownMax]; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public float Alien2LaserSpeed
    {
        get { return values[ConfigDataName.Alien2LaserSpeed]; }
    }

    /// <summary>
    /// Gets the alien x movement speed
    /// </summary>
    public float Alien2MoveSpeedX
    {
        get { return values[ConfigDataName.Alien2MoveSpeedX]; }
    }

    /// <summary>
    /// Gets the alien y movement speed
    /// </summary>
    public float Alien2MoveSpeedY
    {
        get { return values[ConfigDataName.Alien2MoveSpeedY]; }
    }

    /// <summary>
    /// Gets the minimum alien movement delay
    /// </summary>
    public float Alien2MoveDelayMin
    {
        get { return values[ConfigDataName.Alien2MoveDelayMin]; }
    }

    /// <summary>
    /// Gets the max alien movement delay
    /// </summary>
    public float Alien2MoveDelayMax
    {
        get { return values[ConfigDataName.Alien2MoveDelayMax]; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public float Alien2LifeAmount
    {
        get { return values[ConfigDataName.Alien2LifeAmount]; }
    }

    /// <summary>
    /// Gets the alien laser damage
    /// </summary>
    public float Alien3LaserDamage
    {
        get { return values[ConfigDataName.Alien3LaserDamage]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public float Alien3LaserCooldownMin
    {
        get { return values[ConfigDataName.Alien3LaserCooldownMin]; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public float Alien3LaserCooldownMax
    {
        get { return values[ConfigDataName.Alien3LaserCooldownMax]; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public float Alien3LaserSpeed
    {
        get { return values[ConfigDataName.Alien3LaserSpeed]; }
    }

    /// <summary>
    /// Gets the alien x movement speed
    /// </summary>
    public float Alien3MoveSpeedX
    {
        get { return values[ConfigDataName.Alien3MoveSpeedX]; }
    }

    /// <summary>
    /// Gets the alien y movement speed
    /// </summary>
    public float Alien3MoveSpeedY
    {
        get { return values[ConfigDataName.Alien3MoveSpeedY]; }
    }

    /// <summary>
    /// Gets the minimum alien movement delay
    /// </summary>
    public float Alien3MoveDelayMin
    {
        get { return values[ConfigDataName.Alien3MoveDelayMin]; }
    }

    /// <summary>
    /// Gets the max alien movement delay
    /// </summary>
    public float Alien3MoveDelayMax
    {
        get { return values[ConfigDataName.Alien3MoveDelayMax]; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public float Alien3LifeAmount
    {
        get { return values[ConfigDataName.Alien3LifeAmount]; }
    }

    /// <summary>
    /// Gets the laser cooldown cost
    /// </summary>
    public float LaserCooldownCost
    {
        get { return values[ConfigDataName.LaserCooldownCost]; }
    }

    /// <summary>
    /// Gets the lasep speed cost
    /// </summary>
    public float LaserSpeedCost
    {
        get { return values[ConfigDataName.LaserSpeedCost]; }
    }

    /// <summary>
    /// Gets the laser damage cost
    /// </summary>
    public float LaserDamageCost
    {
        get { return values[ConfigDataName.LaserDamageCost]; }
    }

    /// <summary>
    /// Gets the move speed cost
    /// </summary>
    public float MoveSpeedCost
    {
        get { return values[ConfigDataName.MoveSpeedCost]; }
    }

    /// <summary>
    /// Gets the life amount cost
    /// </summary>
    public float LifeAmountCost
    {
        get { return values[ConfigDataName.LifeAmountCost]; }
    }

    /// <summary>
    /// Gets the targeting system cost
    /// </summary>
    public float TargetingSystemCost
    {
        get { return values[ConfigDataName.TargetingSystemCost]; }
    }

    /// <summary>
    /// Gets the targeting chance cost
    /// </summary>
    public float TargetingChanceCost
    {
        get { return values[ConfigDataName.TargetingChanceCost]; }
    }

    /// <summary>
    /// Gets the critical hit chance cost
    /// </summary>
    public float CritChanceCost
    {
        get { return values[ConfigDataName.CritChanceCost]; }
    }

    /// <summary>
    /// Gets the critical hit damage cost
    /// </summary>
    public float CritDamageCost
    {
        get { return values[ConfigDataName.CritDamageCost]; }
    }

    /// <summary>
    /// Gets the money multiplier cost
    /// </summary>
    public float MoneyMultiplierCost
    {
        get { return values[ConfigDataName.MoneyMultiplierCost]; }
    }

    /// <summary>
    /// Gets the manget range cost
    /// </summary>
    public float MagnetRangeCost
    {
        get { return values[ConfigDataName.MagnetRangeCost]; }
    }

    /// <summary>
    /// Gets the updgrade amount
    /// </summary>
    public float UpgradeAmount
    {
        get { return values[ConfigDataName.UpgradeAmount]; }
    }

    /// <summary>
    /// Gets the upgrade cost multiplier
    /// </summary>
    public float UpgradeCostMultiplier
    {
        get { return values[ConfigDataName.UpgradeCostMultiplier]; }
    }

    /// <summary>
    /// Gets the max multiplier value for main store
    /// </summary>
    public float StoreMaxMultiplier
    {
        get { return values[ConfigDataName.StoreMaxMultiplier]; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Gets and sets data values from .csv file
    /// </summary>
    public ConfigData()
    {
        // reader for config file
        StreamReader input = null;

        // try to get values from config file
        try
        {
            // create reader
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, CONFIG_NAME));

            // fill in dictionary values
            string line = input.ReadLine();
            while (line != null)
            {
                string[] csvLine = line.Split(',');
                ConfigDataName name =
                    (ConfigDataName)Enum.Parse(
                        typeof(ConfigDataName), csvLine[0]);
                values.Add(name, float.Parse(csvLine[1]));
                line = input.ReadLine();
            }
        }
        // if anything breaks, set default values
        catch (Exception ex)
        {
            // log exception
            Debug.Log(ex.Message);

            // set values
            SetDefaults();
        }
        // always close the file
        finally
        {
            // if input isn't null then its open
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the default data values
    /// </summary>
    void SetDefaults()
    {
        // clear the dictionary in case program failed part way through
        values.Clear();

        // set all the default values
        values.Add(ConfigDataName.Ship1MaxShield, 3);
        values.Add(ConfigDataName.Ship1LaserCooldown, 0.5f);
        values.Add(ConfigDataName.Ship1LaserSpeed, 8);
        values.Add(ConfigDataName.Ship1MoveSpeed, 10);
        values.Add(ConfigDataName.Ship1LifeAmount, 600);
        values.Add(ConfigDataName.Ship1LaserDamage, 100);
        values.Add(ConfigDataName.Ship1TargetingChance, 0.5f);
        values.Add(ConfigDataName.Ship1CritChance, 0.1f);
        values.Add(ConfigDataName.Ship1CritDamageMulti, 2);
        values.Add(ConfigDataName.Ship1MoneyMultiplier, 0);
        values.Add(ConfigDataName.Ship1MagnetRange, 1);
        values.Add(ConfigDataName.Alien1LaserDamage, 0.5f);
        values.Add(ConfigDataName.Alien1LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien1LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien1LaserSpeed, 5);
        values.Add(ConfigDataName.Alien1MoveSpeedX, 5);
        values.Add(ConfigDataName.Alien1MoveSpeedY, 1.25f);
        values.Add(ConfigDataName.Alien1MoveDelayMin, 0.75f);
        values.Add(ConfigDataName.Alien1MoveDelayMax, 1.25f);
        values.Add(ConfigDataName.Alien1LifeAmount, 2);
        values.Add(ConfigDataName.Alien2LaserDamage, 0.75f);
        values.Add(ConfigDataName.Alien2LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien2LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien2LaserSpeed, 5);
        values.Add(ConfigDataName.Alien2MoveSpeedX, 4);
        values.Add(ConfigDataName.Alien2MoveSpeedY, 1);
        values.Add(ConfigDataName.Alien2MoveDelayMin, 0.75f);
        values.Add(ConfigDataName.Alien2MoveDelayMax, 1.25f);
        values.Add(ConfigDataName.Alien2LifeAmount, 10);
        values.Add(ConfigDataName.Alien3LaserDamage, 1);
        values.Add(ConfigDataName.Alien3LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien3LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien3LaserSpeed, 5);
        values.Add(ConfigDataName.Alien3MoveSpeedX, 7);
        values.Add(ConfigDataName.Alien3MoveSpeedY, 2);
        values.Add(ConfigDataName.Alien3MoveDelayMin, 1.25f);
        values.Add(ConfigDataName.Alien3MoveDelayMax, 1.75f);
        values.Add(ConfigDataName.Alien3LifeAmount, 25);
        values.Add(ConfigDataName.LaserCooldownCost, 10);
        values.Add(ConfigDataName.LaserDamageCost, 10);
        values.Add(ConfigDataName.LaserSpeedCost, 10);
        values.Add(ConfigDataName.MoveSpeedCost, 10);
        values.Add(ConfigDataName.LifeAmountCost, 10);
        values.Add(ConfigDataName.TargetingSystemCost, 2000);
        values.Add(ConfigDataName.TargetingChanceCost, 1000);
        values.Add(ConfigDataName.CritChanceCost, 1000);
        values.Add(ConfigDataName.CritDamageCost, 1000);
        values.Add(ConfigDataName.MoneyMultiplierCost, 1000);
        values.Add(ConfigDataName.MagnetRangeCost, 1000);
        values.Add(ConfigDataName.UpgradeAmount, 0.25f);
        values.Add(ConfigDataName.UpgradeCostMultiplier, 1.75f);
        values.Add(ConfigDataName.StoreMaxMultiplier, 4.33f);
    }

    #endregion
}
