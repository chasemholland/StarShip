using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Alien behaviour
/// </summary>
public class Alien : FloatEventInvoker
{
    #region Fields

    // prefab alien laser
    [SerializeField]
    GameObject prefabAlienLaser;

    // prefab coin
    [SerializeField]
    GameObject prefabCoin;

    // prefab coinbag
    [SerializeField]
    GameObject prefabCoinBag;

    // prefab heart
    [SerializeField]
    GameObject prefabHeart;

    // prefab shield
    [SerializeField]
    GameObject prefabShield;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    GameObject prefabAlienExplosion;

    [SerializeField]
    GameObject prefabShipLaserExplosion;

    [SerializeField]
    GameObject prefabDamageText;

    // chance of what holding
    bool hasCoin = false;
    bool hasCoinBag = false;
    bool hasHeart = false;
    bool hasShield = false;
    int coinCount;
    int coinBagCount;
    int heartCount;
    int shieldCount;

    // alien movement support
    Timer moveTimer;
    int moveDirection;
    float moveSpeed;
    Rigidbody2D alienBody;

    // alien behavior support
    Timer shootTimer;
    float healthRemaining;
    float maxHealth;
    BoxCollider2D alienCollider;
    float alienWidth;
    float alienHeight;

    // damage amount
    float playerDamage;
    float playerCritChance;
    float playerCritMultiplier;
    float damage;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // get player damage
        playerDamage = ConfigUtils.Ship1LaserDamage * (1f + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0));

        // get player crit
        playerCritChance = ConfigUtils.Ship1CritChance;
        playerCritMultiplier = ConfigUtils.Ship1CritDamageMulti;

        // get reference to alien
        alienBody = GetComponent<Rigidbody2D>();

        // get dimensions
        alienCollider = GetComponent<BoxCollider2D>();
        alienWidth = alienCollider.size.x;
        alienHeight = alienCollider.size.y;

        // get what holding
        PickupChance();

        // set trait values
        SetTraitValues();

        // set shoot and move timers
        SetTimers();

    }

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // check for clamping
        float newX = CheckClamping(transform.position.x + (moveDirection * moveSpeed * Time.deltaTime));

        // move the alien
        alienBody.MovePosition(new Vector2(newX, transform.position.y));

    }

    /// <summary>
    /// Collision triggered event
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if collider is player laser
        if (other.CompareTag("ShipLaser1"))
        {
            // handle alien damaged
            HandleDamage();

            // get height of laser
            float otherHalfHeight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn laser explosion
            Instantiate(prefabShipLaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y + otherHalfHeight, 0f), Quaternion.identity);

            // destroy laser
            Destroy(other.gameObject);

            // store mothership health for next waves
            if (gameObject.CompareTag("Alien3"))
            {
                PlayerPrefs.SetFloat(PlayerPrefNames.MothershipLifeAmount.ToString(), PlayerPrefs.GetFloat(PlayerPrefNames.MothershipLifeAmount.ToString()) - playerDamage);
            }

            // check if alien dead
            if (healthRemaining <= 0)
            {
                HandleAlienDeath();  
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles alien damaged
    /// </summary>
    private void HandleDamage()
    {
        // spawn damage text
        GameObject damageText = Instantiate(prefabDamageText, transform.position, Quaternion.identity);

        if (Random.Range(0.0f, 1.1f) < playerCritChance)
        {
            // play laser hit ------ TO BE CHANGED TO SEPERATE NOISE ------------------------------
            AudioManager.Play(AudioName.ShipLaserHit);

            // set damage
            damage = playerDamage * playerCritMultiplier;

        }
        else
        {
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // set damage
            damage = playerDamage;

        }

        // set damage text
        damageText.GetComponent<TextMeshPro>().SetText(damage.ToString());

        // adjust health
        healthRemaining -= damage;

        // adjust healthbar
        healthBar.value = healthRemaining / maxHealth;

    }

    /// <summary>
    /// Handles alien death
    /// </summary>
    private void HandleAlienDeath()
    {
        // play explosion sound
        AudioManager.Play(AudioName.Explosion);

        // add to lifetime aliens defeated
        PlayerPrefs.SetInt(PlayerPrefNames.LifetimeAliensDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.LifetimeAliensDefeated.ToString(), 0) + 1);

        if (gameObject.CompareTag("Alien1") || gameObject.CompareTag("Alien2"))
        {
            // check if holding money
            if (hasCoin)
            {
                // create coin and get moving
                SpawnPickups(prefabCoin);
            }
            else if (hasCoinBag)
            {
                // create coin bag and get moving
                SpawnPickups(prefabCoinBag);
            }
            else if (hasHeart)
            {
                // create heart and get moving
                SpawnPickups(prefabHeart);
            }
            else if (hasShield)
            {
                // create shield and get moving
                SpawnPickups(prefabShield);
            }

            // spawn explosions
            if (gameObject.CompareTag("Alien1"))
            {
                Instantiate(prefabAlienExplosion, transform.position, Quaternion.identity);

                // add alien defeated to player prefs
                PlayerPrefs.SetInt(PlayerPrefNames.RedsDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.RedsDefeated.ToString(), 0) + 1);
            }
            else if (gameObject.CompareTag("Alien2"))
            {
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x - (alienWidth / 6), transform.position.y, 0f), Quaternion.identity);
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x + (alienWidth / 6), transform.position.y, 0f), Quaternion.identity);

                // add alien defeated to player prefs
                PlayerPrefs.SetInt(PlayerPrefNames.GreensDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.GreensDefeated.ToString(), 0) + 1);
            }
            // destroy alien
            Destroy(gameObject);

            // add alien defeated to player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.AliensDefeated.ToString(), 0) + 1);
        }
        else if (gameObject.CompareTag("Alien3"))
        {
            // spawn coins
            for (int i = 1; i <= coinCount; i++)
            {
                // create coin and get moving
                SpawnPickups(prefabCoin);
            }

            // spawn coin bags
            for (int i = 1; i <= coinBagCount; i++)
            {
                // create coin bag and get moving
                SpawnPickups(prefabCoinBag);
            }

            // spawn hearts
            for (int i = 1; i <= heartCount; i++)
            {
                // create heart and get moving
                SpawnPickups(prefabHeart);
            }

            // spawn shields
            for (int i = 1; i <= shieldCount; i++)
            {
                // create shield and get moving
                SpawnPickups(prefabShield);
            }

            // spawn explosions
            for (int e = 0; e < 6; e++)
            {
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x - (alienWidth / Random.Range(2, 20)), transform.position.y - (alienHeight / Random.Range(2, 20)), 0f), Quaternion.identity);
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x + (alienWidth / Random.Range(2, 20)), transform.position.y + (alienHeight / Random.Range(2, 20)), 0f), Quaternion.identity);
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x - (alienWidth / Random.Range(2, 20)), transform.position.y + (alienHeight / Random.Range(2, 20)), 0f), Quaternion.identity);
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x + (alienWidth / Random.Range(2, 20)), transform.position.y - (alienHeight / Random.Range(2, 20)), 0f), Quaternion.identity);
            }

            // destroy alien
            Destroy(gameObject);

            // add alien defeated to player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.AliensDefeated.ToString(), 0) + 1);

            // add mother ship defeated to player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0) + 1);

            // set alien store unlocked
            PlayerPrefs.SetInt(PlayerPrefNames.AlienStoreUnlocked.ToString(), 1);
        }
    }

    /// <summary>
    /// Clamp ship within the bounds of the screen
    /// </summary>
    private float CheckClamping(float xPosition)
    {
        if (xPosition + (alienWidth / 2) >= ScreenUtils.ScreenRight)
        {
            moveDirection *= -1;
            return ScreenUtils.ScreenRight - (alienWidth / 2);
        }
        else if (xPosition - (alienWidth / 2) <= ScreenUtils.ScreenLeft)
        {
            moveDirection *= -1;;
            return ScreenUtils.ScreenLeft + (alienWidth / 2);
        }
        else
        {
            return xPosition;
        }
    }

    /// <summary>
    /// Moves the alien when timer finished
    /// </summary>
    private void HandleMoveTimerFinished()
    {
        // pick a random direction
        moveDirection = (Random.Range(0, 2) * 2 - 1);

        // restart the move timer
        moveTimer.Run();

    }

    /// <summary>
    /// Handles shoot timer finished event
    /// </summary>
    private void HandleShootTimerFinished()
    {
        // shoot lasers
        if (gameObject.CompareTag("Alien1"))
        {
            // spawn laser and get moving
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x, transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);

            // set new cooldown
            shootTimer.Duration = Random.Range(ConfigUtils.Alien1LaserCooldownMin, ConfigUtils.Alien1LaserCooldownMax);
        }
        else if (gameObject.CompareTag("Alien2"))
        {
            // spawn lasers and get moving
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x, transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x + (alienWidth / 4f), transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x - (alienWidth / 4f), transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);

            // set new cooldown
            shootTimer.Duration = Random.Range(ConfigUtils.Alien2LaserCooldownMin, ConfigUtils.Alien2LaserCooldownMax);
        }
        else if (gameObject.CompareTag("Alien3"))
        {
            // spawn lasers and get moving
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x, transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x + (alienWidth / 8f * 3f), transform.position.y - (alienHeight / 2.6f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x + (alienWidth / 8f * 1.4f), transform.position.y - (alienHeight / 2.4f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x - (alienWidth / 8f * 3f), transform.position.y - (alienHeight / 2.6f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x - (alienWidth / 8f * 1.4f), transform.position.y - (alienHeight / 2.4f), 0f), Quaternion.identity);

            // set new cooldown
            shootTimer.Duration = Random.Range(ConfigUtils.Alien3LaserCooldownMin, ConfigUtils.Alien3LaserCooldownMax);
        }
        
        // run the shoot timer
        shootTimer.Run();
    }

    /// <summary>
    /// Gets what holding
    /// </summary>
    private void PickupChance()
    {
        if (gameObject.CompareTag("Alien1") || gameObject.CompareTag("Alien2"))
        {
            int hasPickup = Random.Range(0, 11);
            if (hasPickup <= 8)
            {
                int hasType = Random.Range(0, 21);
                if (gameObject.CompareTag("Alien1"))
                {
                    if (hasType <= 1)
                    {
                        hasHeart = true;
                    }
                    else if (hasType > 1 && hasType <= 3)
                    {
                        hasShield = true;
                    }
                    else if (hasType > 3 && hasType <= 17)
                    {
                        hasCoin = true;
                    }
                    else if (hasType > 17 && hasType <= 20)
                    {
                        hasCoinBag = true;
                    }
                }
                // higher change of heart, shield, and coin bag
                else if (gameObject.CompareTag("Alien2"))
                {
                    if (hasType <= 3)
                    {
                        hasHeart = true;
                    }
                    else if (hasType > 3 && hasType <= 7)
                    {
                        hasShield = true;
                    }
                    else if (hasType > 7 && hasType <= 14)
                    {
                        hasCoin = true;
                    }
                    else if (hasType > 14 && hasType <= 20)
                    {
                        hasCoinBag = true;
                    }
                }
            }
        }
        else if (gameObject.CompareTag("Alien3"))
        {
            coinCount = Random.Range(10, 21);
            coinBagCount = Random.Range(4, 11);
            heartCount = Random.Range(1, 4);
            shieldCount = Random.Range(1, 4);
        }
    }

    /// <summary>
    /// Sets the alien traits
    /// </summary>
    private void SetTraitValues()
    {
        // get laser speed, move speed, and health
        if (gameObject.CompareTag("Alien1"))
        {
            moveSpeed = ConfigUtils.Alien1MoveSpeed;
            healthRemaining = ConfigUtils.Alien1LifeAmount;
            maxHealth = healthRemaining;
        }
        else if (gameObject.CompareTag("Alien2"))
        {
            moveSpeed = ConfigUtils.Alien2MoveSpeed;
            healthRemaining = ConfigUtils.Alien2LifeAmount;
            maxHealth = healthRemaining;
        }
        else if (gameObject.CompareTag("Alien3"))
        {
            moveSpeed = ConfigUtils.Alien3MoveSpeed;
            healthRemaining = PlayerPrefs.GetFloat(PlayerPrefNames.MothershipLifeAmount.ToString());
            maxHealth = ConfigUtils.Alien3LifeAmount;
        }

        // set healthbar value
        healthBar.value = healthRemaining / maxHealth;

        // set initial move direction
        moveDirection = (Random.Range(0, 2) * 2 - 1);

    }

    /// <summary>
    /// Sets the alien timers
    /// </summary>
    private void SetTimers()
    {
        // initialize the shoot, move, and tracking timers
        shootTimer = gameObject.AddComponent<Timer>();
        moveTimer = gameObject.AddComponent<Timer>();
        if (gameObject.CompareTag("Alien1"))
        {
            shootTimer.Duration = Random.Range(ConfigUtils.Alien1LaserCooldownMin, ConfigUtils.Alien1LaserCooldownMax);
            moveTimer.Duration = ConfigUtils.Alien1MoveDelay;
        }
        else if (gameObject.CompareTag("Alien2"))
        {
            shootTimer.Duration = Random.Range(ConfigUtils.Alien2LaserCooldownMin, ConfigUtils.Alien2LaserCooldownMax);
            moveTimer.Duration = ConfigUtils.Alien2MoveDelay;
        }
        else if (gameObject.CompareTag("Alien3"))
        {
            shootTimer.Duration = Random.Range(ConfigUtils.Alien3LaserCooldownMin, ConfigUtils.Alien3LaserCooldownMax);
            moveTimer.Duration = ConfigUtils.Alien3MoveDelay;
        }

        // run the timers
        shootTimer.Run();
        moveTimer.Run();

        // add timer as listener for shoot laser event
        shootTimer.AddTimerFinishedListener(HandleShootTimerFinished);

        // add timer as listener for move right event
        moveTimer.AddTimerFinishedListener(HandleMoveTimerFinished);

    }

    /// <summary>
    /// Handles spwning pickups
    /// </summary>
    /// <param name="gameObject"></param>
    private void SpawnPickups(GameObject gameObject)
    {
        // create pickup and get moving
        GameObject pickup = Instantiate(gameObject, new Vector3(
            Random.Range(transform.position.x - (alienWidth / 2f), transform.position.x + (alienWidth / 2f)), 
            Random.Range(transform.position.y - (alienHeight / 2f), transform.position.y + (alienHeight / 2f)), 0f), 
            Quaternion.identity);
        pickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -3f), ForceMode2D.Impulse);

    }

    #endregion
}
