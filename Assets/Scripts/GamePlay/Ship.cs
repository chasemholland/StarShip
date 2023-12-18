using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Player ship mechanics
/// </summary>
public class Ship : FloatEventInvoker
{
    #region Fields

    // ship laser
    [SerializeField]
    GameObject prefabShipLaser1;

    // ship explosion
    [SerializeField]
    GameObject prefabShipExplosion;

    // alien1 laser explosion
    [SerializeField]
    GameObject prefabAlien1LaserExplosion;
    float alien1LaserDamage;

    // alien2 laser explosion
    [SerializeField]
    GameObject prefabAlien2LaserExplosion;
    float alien2LaserDamage;

    // alien3 laser explosion
    [SerializeField]
    GameObject prefabAlien3LaserExplosion;
    float alien3LaserDamage;

    // laser support
    float laserCooldown;

    bool canShoot = true;
    Timer cooldownTimer;

    // gameplay support
    float moveSpeed;
    float healthLeft;
    float maxHealth;
    float shieldLeft;
    float maxShield;
    float moneyMultiplier;
    float magnetRange;

    // ship body reference
    Rigidbody2D shipBody;

    // ship dimensions
    BoxCollider2D shipCollider;
    float shipWidth;
    float shipHeight;

    // money values
    private readonly int coinValue = 50;
    private readonly int coinBagValue = 150;


    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set ship values
        SetShipValues();

        // set alien laser damage
        alien1LaserDamage = ConfigUtils.Alien1LaserDamage;
        alien2LaserDamage = ConfigUtils.Alien2LaserDamage;
        alien3LaserDamage = ConfigUtils.Alien3LaserDamage;

        // get reference to ship body
        shipBody = GetComponent<Rigidbody2D>();

        // get ship dimensions
        shipCollider = GetComponent<BoxCollider2D>();
        shipWidth = shipCollider.size.x;
        shipHeight = shipCollider.size.y;

        // set laser cooldown timer
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = laserCooldown;

        // add lsitener to handle cooldown timer finished
        cooldownTimer.AddTimerFinishedListener(HandleCooldownFinished);

        // add as invoker for add money event
        unityEvents.Add(EventName.AddMoneyEvent, new AddMoneyEvent());
        EventManager.AddInvoker(EventName.AddMoneyEvent, this);

        // add as invoker for increase health event
        unityEvents.Add(EventName.IncreaseHealthEvent, new IncreaseHealthEvent());
        EventManager.AddInvoker(EventName.IncreaseHealthEvent, this);

        // add as invoker for decrease health event
        unityEvents.Add(EventName.DecreaseHealthEvent, new DecreaseHealthEvent());
        EventManager.AddInvoker(EventName.DecreaseHealthEvent, this);

        // add as invoker for increase shield event
        unityEvents.Add(EventName.IncreaseShieldEvent, new IncreaseShieldEvent());
        EventManager.AddInvoker(EventName.IncreaseShieldEvent, this);

        // add as invoker for decrease shield event
        unityEvents.Add(EventName.DecreaseShieldEvent, new DecreaseShieldEvent());
        EventManager.AddInvoker(EventName.DecreaseShieldEvent, this);

    }

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // take user input to move ship
        // get the value of input
        float direction = Input.GetAxis("Horizontal");

        if (direction != 0)
        {

            // check fro clamping
            float newX = CheckClamping(transform.position.x + (direction * moveSpeed * Time.deltaTime));

            // move ship left or right
            shipBody.MovePosition(new Vector2(newX, transform.position.y));

        }

        // take user input to fire lasers
        if (Input.GetAxis("Fire") != 0)
        {
            
            // shoot laser if player can shoot
            if (canShoot)
            {
                // play laser sound
                AudioManager.Play(AudioName.ShipLaser);

                Instantiate(prefabShipLaser1, new Vector3(transform.position.x, transform.position.y + (shipHeight / 2), 0f), Quaternion.identity);

                // check if timer has been started
                if (!cooldownTimer.Running)
                {
                    cooldownTimer.Run();
                    canShoot = false;
                }

            }
        }
    }

    /// <summary>
    /// Collision triggered event
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alien1Laser"))
        {
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // get laser height
            float otherHalfheight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn explosion
            Instantiate(prefabAlien1LaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y - otherHalfheight, 0f), Quaternion.identity);

            // destroy alien laser
            Destroy(other.gameObject);

            // hurt player
            DecreaseHealthOrShield(alien1LaserDamage);
        }

        if (other.CompareTag("Alien2Laser"))
        {
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // get laser height
            float otherHalfheight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn explosion
            Instantiate(prefabAlien2LaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y - otherHalfheight, 0f), Quaternion.identity);

            // destroy alien laser
            Destroy(other.gameObject);

            // hurt player
            DecreaseHealthOrShield(alien2LaserDamage);
        }

        if (other.CompareTag("Alien3Laser"))
        {
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // get laser height
            float otherHalfheight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn explosion
            Instantiate(prefabAlien3LaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y - otherHalfheight, 0f), Quaternion.identity);

            // destroy alien laser
            Destroy(other.gameObject);

            // hurt player
            DecreaseHealthOrShield(alien3LaserDamage);
        }

        // check if dead
        if (healthLeft <= 0)
        {
            // play explosion
            AudioManager.Play(AudioName.Explosion);

            // spawn explosion
            Instantiate(prefabShipExplosion, transform.position, Quaternion.identity);

            // destroy player
            Destroy(gameObject);
        }

        // check for coin pickup
        if (other.CompareTag("Coin"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            Destroy(other.gameObject);
            unityEvents[EventName.AddMoneyEvent].Invoke(MathF.Round(coinValue * moneyMultiplier, 0));
        }

        // check for coin bag pickup
        if (other.CompareTag("CoinBag"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            Destroy(other.gameObject);
            unityEvents[EventName.AddMoneyEvent].Invoke(MathF.Round(coinBagValue * moneyMultiplier, 0));
        }

        // check for health pickup
        if (other.CompareTag("Heart"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            // check if health amount can be added
            if (healthLeft + 100 <= maxHealth)
            {
                healthLeft += 100;
                unityEvents[EventName.IncreaseHealthEvent].Invoke(100);
            }
            else
            {
                float healthAddedAmount = maxHealth - healthLeft;
                healthLeft += healthAddedAmount;
                unityEvents[EventName.IncreaseHealthEvent].Invoke(healthAddedAmount);
            }
            Destroy(other.gameObject);

        }

        // check for shield pickup
        if (other.CompareTag("Shield"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            // check if shield amount can be added
            if (shieldLeft + maxShield / 4 <= maxShield)
            {
                shieldLeft += maxShield / 4;
                unityEvents[EventName.IncreaseShieldEvent].Invoke(maxShield / 4);
            }
            else
            {
                float shieldAddedAmount = maxShield - shieldLeft;
                shieldLeft += shieldAddedAmount;
                unityEvents[EventName.IncreaseShieldEvent].Invoke(shieldAddedAmount);
            }
            Destroy(other.gameObject);

        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Checks for upgrades and sets ship values accordingly
    /// </summary>
    private void SetShipValues()
    {
        // check for laser cooldown upgrades
        laserCooldown = ConfigUtils.Ship1LaserCooldown / (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0));

        // check for move speed upgrades
        moveSpeed = ConfigUtils.Ship1MoveSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0));

        // check for health upgrades
        healthLeft = ConfigUtils.Ship1LifeAmount * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0));
        maxHealth = healthLeft;

        // set shield
        shieldLeft = 0;
        maxShield = ConfigUtils.Ship1MaxShield * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipMaxShield.ToString(), 0));

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

        // set magnet range
        if (PlayerPrefs.GetInt(PlayerPrefNames.HasMagnet.ToString(), 0) == 10)
        {
            magnetRange = ConfigUtils.Ship1MagnetRange * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString(), 0));
        }
        else
        {
            // default to 0, no range
            magnetRange = 0;
        }
        

    }

    /// <summary>
    /// Clamp ship within the bounds of the screen
    /// </summary>
    private float CheckClamping(float xPosition)
    {
        if (xPosition + (shipWidth / 2) >= ScreenUtils.ScreenRight)
        {
            return ScreenUtils.ScreenRight - (shipWidth / 2);
        }
        else if (xPosition - (shipWidth / 2) <= ScreenUtils.ScreenLeft)
        {
            return ScreenUtils.ScreenLeft + (shipWidth / 2);
        }
        else
        {
            return xPosition;
        }
    }

    /// <summary>
    /// Handles laser cooldown timer finish
    /// </summary>
    private void HandleCooldownFinished()
    {
        canShoot = true;
        cooldownTimer.Stop();
    }

    /// <summary>
    /// Handles player being damaged
    /// </summary>
    /// <param name="damageAmount"></param>
    private void DecreaseHealthOrShield(float damageAmount)
    {
        if (shieldLeft == 0)
        {
            // keep track of health
            healthLeft -= damageAmount;

            // decrease halth bar
            unityEvents[EventName.DecreaseHealthEvent].Invoke(damageAmount);
        }
        else
        {
            // keep track of shield
            if (shieldLeft - damageAmount <= 0)
            {
                // decrease shield bar
                unityEvents[EventName.DecreaseShieldEvent].Invoke(shieldLeft);

                // set shield value to zero
                shieldLeft = 0;
            }
            else
            {
                // decrease shield bar
                unityEvents[EventName.DecreaseShieldEvent].Invoke(damageAmount);

                // decrement shield value
                shieldLeft -= damageAmount;
            }
        }
    }

    #endregion

}
