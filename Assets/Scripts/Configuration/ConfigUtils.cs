using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to config data
/// </summary>
public static class ConfigUtils
{
    #region Fields

    // configdata object
    static ConfigData configData;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the initial max shield value
    /// </summary>
    public static float Ship1MaxShield
    {
        get { return configData.Ship1MaxShield; }
    }

    /// <summary>
    /// Gets the basic ship laser shoot cooldown
    /// </summary>
    public static float Ship1LaserCooldown
    {
        get { return configData.Ship1LaserCooldown; }
    }

    /// <summary>
    /// Gets the basic ship laser speed
    /// </summary>
    public static float Ship1LaserSpeed
    {
        get { return configData.Ship1LaserSpeed; }
    }

    /// <summary>
    /// Gets the basic ship movement speed
    /// </summary>
    public static float Ship1MoveSpeed
    {
        get { return configData.Ship1MoveSpeed; }
    }

    /// <summary>
    /// Gets the basic ship health
    /// </summary>
    public static float Ship1LifeAmount
    {
        get { return configData.Ship1LifeAmount; }
    }

    /// <summary>
    /// Gets the laser1 damage
    /// </summary>
    public static float Ship1LaserDamage
    {
        get { return configData.Ship1LaserDamage; }
    }

    /// <summary>
    /// Gets the targeting chance
    /// </summary>
    public static float Ship1TargetingChance
    {
        get { return configData.Ship1TargetingChance; }
    }

    /// <summary>
    /// Gets the critical hit chance
    /// </summary>
    public static float Ship1CritChance
    {
        get { return configData.Ship1CritChance; }
    }

    /// <summary>
    /// Gets the critical hit damage multiplier
    /// </summary>
    public static float Ship1CritDamageMulti
    {
        get { return configData.Ship1CritDamageMulti; }
    }

    /// <summary>
    /// Gets alien laser damage
    /// </summary>
    public static float Alien1LaserDamage
    {
        get { return configData.Alien1LaserDamage; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public static float Alien1LaserCooldownMin
    {
        get { return configData.Alien1LaserCooldownMin; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public static float Alien1LaserCooldownMax
    {
        get { return configData.Alien1LaserCooldownMax; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public static float Alien1LaserSpeed
    {
        get { return configData.Alien1LaserSpeed; }
    }

    /// <summary>
    /// Gets the alien movement speed
    /// </summary>
    public static float Alien1MoveSpeed
    {
        get { return configData.Alien1MoveSpeed; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public static float Alien1MoveDelay
    {
        get { return configData.Alien1MoveDelay; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public static float Alien1LifeAmount
    {
        get { return configData.Alien1LifeAmount; }
    }

    /// <summary>
    /// Gets the alien laser damage
    /// </summary>
    public static float Alien2LaserDamage
    {
        get { return configData.Alien2LaserDamage; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public static float Alien2LaserCooldownMin
    {
        get { return configData.Alien2LaserCooldownMin; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public static float Alien2LaserCooldownMax
    {
        get { return configData.Alien2LaserCooldownMax; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public static float Alien2LaserSpeed
    {
        get { return configData.Alien2LaserSpeed; }
    }

    /// <summary>
    /// Gets the alien movement speed
    /// </summary>
    public static float Alien2MoveSpeed
    {
        get { return configData.Alien2MoveSpeed; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public static float Alien2MoveDelay
    {
        get { return configData.Alien2MoveDelay; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public static float Alien2LifeAmount
    {
        get { return configData.Alien2LifeAmount; }
    }

    /// <summary>
    /// Gets the alien laser damage
    /// </summary>
    public static float Alien3LaserDamage
    {
        get { return configData.Alien3LaserDamage; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown minimum
    /// </summary>
    public static float Alien3LaserCooldownMin
    {
        get { return configData.Alien3LaserCooldownMin; }
    }

    /// <summary>
    /// Gets the alien laser shoot cooldown maximum
    /// </summary>
    public static float Alien3LaserCooldownMax
    {
        get { return configData.Alien3LaserCooldownMax; }
    }

    /// <summary>
    /// Gets the speed of the alien laser projectiles
    /// </summary>
    public static float Alien3LaserSpeed
    {
        get { return configData.Alien3LaserSpeed; }
    }

    /// <summary>
    /// Gets the alien movement speed
    /// </summary>
    public static float Alien3MoveSpeed
    {
        get { return configData.Alien3MoveSpeed; }
    }

    /// <summary>
    /// Gets the alien movement delay
    /// </summary>
    public static float Alien3MoveDelay
    {
        get { return configData.Alien3MoveDelay; }
    }

    /// <summary>
    /// Gets the alien health
    /// </summary>
    public static float Alien3LifeAmount
    {
        get { return configData.Alien3LifeAmount; }
    }

    /// <summary>
    /// Gets the laser cooldown cost
    /// </summary>
    public static float LaserCooldownCost
    {
        get { return configData.LaserCooldownCost; }
    }

    /// <summary>
    /// Gets the lasep speed cost
    /// </summary>
    public static float LaserSpeedCost
    {
        get { return configData.LaserSpeedCost; }
    }

    /// <summary>
    /// Gets the laser damage cost
    /// </summary>
    public static float LaserDamageCost
    {
        get { return configData.LaserDamageCost; }
    }

    /// <summary>
    /// Gets the move speed cost
    /// </summary>
    public static float MoveSpeedCost
    {
        get { return configData.MoveSpeedCost; }
    }

    /// <summary>
    /// Gets the life amount cost
    /// </summary>
    public static float LifeAmountCost
    {
        get { return configData.LifeAmountCost; }
    }

    /// <summary>
    /// Gets the targeting system cost
    /// </summary>
    public static float TargetingSystemCost
    {
        get { return configData.TargetingSystemCost; }
    }

    /// <summary>
    /// Gets the targeting chance cost
    /// </summary>
    public static float TargetingChanceCost
    {
        get { return configData.TargetingChanceCost;}
    }

    /// <summary>
    /// Gets the critical hit chance cost
    /// </summary>
    public static float CritChanceCost
    {
        get { return configData.CritChanceCost; }
    }

    /// <summary>
    /// Gets the critical hit damage cost
    /// </summary>
    public static float CritDamageCost
    {
        get { return configData.CritDamageCost; }
    }

    /// <summary>
    /// Gets the updgrade amount
    /// </summary>
    public static float UpgradeAmount
    {
        get { return configData.UpgradeAmount; }
    }

    /// <summary>
    /// Gets the upgrade cost multiplier
    /// </summary>
    public static float UpgradeCostMultiplier
    {
        get { return configData.UpgradeCostMultiplier; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the config utils
    /// </summary>
    public static void Initialize()
    {
        configData = new ConfigData();
    }

    #endregion
}
