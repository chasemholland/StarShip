using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
    /// Gets the laser1 damage
    /// </summary>
    public float Ship1LaserDamage
    {
        get { return values[ConfigDataName.Ship1LaserDamage]; }
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
    /// Gets the alien movement speed
    /// </summary>
    public float Alien1MoveSpeed
    {
        get { return values[ConfigDataName.Alien1MoveSpeed]; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public float Alien1MoveDelay
    {
        get { return values[ConfigDataName.Alien1MoveDelay]; }
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
    /// Gets the alien movement speed
    /// </summary>
    public float Alien2MoveSpeed
    {
        get { return values[ConfigDataName.Alien2MoveSpeed]; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public float Alien2MoveDelay
    {
        get { return values[ConfigDataName.Alien2MoveDelay]; }
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
    /// Gets the alien movement speed
    /// </summary>
    public float Alien3MoveSpeed
    {
        get { return values[ConfigDataName.Alien3MoveSpeed]; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public float Alien3MoveDelay
    {
        get { return values[ConfigDataName.Alien3MoveDelay]; }
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
        values.Add(ConfigDataName.Ship1LaserCooldown, 0.5f);
        values.Add(ConfigDataName.Ship1LaserSpeed, 8);
        values.Add(ConfigDataName.Ship1MoveSpeed, 10);
        values.Add(ConfigDataName.Ship1LifeAmount, 6);
        values.Add(ConfigDataName.Ship1LaserDamage, 1);
        values.Add(ConfigDataName.Alien1LaserDamage, 0.5f);
        values.Add(ConfigDataName.Alien1LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien1LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien1LaserSpeed, 5);
        values.Add(ConfigDataName.Alien1MoveSpeed, 5);
        values.Add(ConfigDataName.Alien1MoveDelay, 0.5f);
        values.Add(ConfigDataName.Alien1LifeAmount, 2);
        values.Add(ConfigDataName.Alien2LaserDamage, 0.75f);
        values.Add(ConfigDataName.Alien2LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien2LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien2LaserSpeed, 5);
        values.Add(ConfigDataName.Alien2MoveSpeed, 4);
        values.Add(ConfigDataName.Alien2MoveDelay, 0.5f);
        values.Add(ConfigDataName.Alien2LifeAmount, 10);
        values.Add(ConfigDataName.Alien3LaserDamage, 1);
        values.Add(ConfigDataName.Alien3LaserCooldownMin, 3);
        values.Add(ConfigDataName.Alien3LaserCooldownMax, 6);
        values.Add(ConfigDataName.Alien3LaserSpeed, 5);
        values.Add(ConfigDataName.Alien3MoveSpeed, 3);
        values.Add(ConfigDataName.Alien3MoveDelay, 0.5f);
        values.Add(ConfigDataName.Alien3LifeAmount, 25);
        values.Add(ConfigDataName.LaserCooldownCost, 10);
        values.Add(ConfigDataName.LaserDamageCost, 10);
        values.Add(ConfigDataName.LaserSpeedCost, 10);
        values.Add(ConfigDataName.MoveSpeedCost, 10);
        values.Add(ConfigDataName.LifeAmountCost, 10);
        values.Add(ConfigDataName.UpgradeAmount, 0.25f);
        values.Add(ConfigDataName.UpgradeCostMultiplier, 1.75f);
    }

    #endregion
}
