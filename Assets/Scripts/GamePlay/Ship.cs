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

    [SerializeField]
    float laserSpeed;
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

    // ship body reference
    Rigidbody2D shipBody;

    // ship dimensions
    BoxCollider2D shipCollider;
    float shipWidth;
    float shipHeight;

    // money values
    int coinValue = 10;
    int coinBagValue = 50;


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

                GameObject laser = Instantiate(prefabShipLaser1, new Vector3(transform.position.x, transform.position.y + (shipHeight / 2), 0f), Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, laserSpeed), ForceMode2D.Impulse);

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
            unityEvents[EventName.AddMoneyEvent].Invoke(coinValue);
        }

        // check for coin bag pickup
        if (other.CompareTag("CoinBag"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            Destroy(other.gameObject);
            unityEvents[EventName.AddMoneyEvent].Invoke(coinBagValue);
        }

        // check for health pickup
        if (other.CompareTag("Heart"))
        {
            // play pickup sound
            AudioManager.Play(AudioName.Pickup);

            // check if health amount can be added
            if (healthLeft + 1 <= maxHealth)
            {
                healthLeft += 1;
                unityEvents[EventName.IncreaseHealthEvent].Invoke(1);
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
            if (shieldLeft + 1 <= maxShield)
            {
                shieldLeft += 1;
                unityEvents[EventName.IncreaseShieldEvent].Invoke(1);
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
        // check for laser speed upgrades
        laserSpeed = ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0));

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
