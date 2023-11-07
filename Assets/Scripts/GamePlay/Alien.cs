using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Alien behaviour
/// </summary>
public class Alien : MonoBehaviour
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

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    GameObject prefabAlienExplosion;

    [SerializeField]
    GameObject prefabShipLaserExplosion;

    // chance of holding money and which type
    bool hasCoin = false;
    bool hasCoinBag = false;
    int coinCount;
    int coinBagCount;

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

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // get player damage
        playerDamage = ConfigUtils.Ship1LaserDamage * (1f + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0));
        
        // get reference to alien
        alienBody = GetComponent<Rigidbody2D>();

        // get dimensions
        alienCollider = GetComponent<BoxCollider2D>();
        alienWidth = alienCollider.size.x;
        alienHeight = alienCollider.size.y;

        // set healthbar value
        healthBar.value = 1;

        // get whether or not holding money and which type
        MoneyChance();

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
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // get height of laser
            float otherHalfHeight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn laser explosion
            Instantiate(prefabShipLaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y + otherHalfHeight, 0f), Quaternion.identity);

            // destroy laser
            Destroy(other.gameObject);

            // damage alien
            healthRemaining -= playerDamage;

            // adjust healthbar
            healthBar.value = healthRemaining / maxHealth;

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
    /// Handles alien death
    /// </summary>
    private void HandleAlienDeath()
    {
        // play explosion sound
        AudioManager.Play(AudioName.Explosion);

        if (gameObject.tag == "Alien1" || gameObject.tag == "Alien2")
        {
            // check if holding money
            if (hasCoin)
            {
                // create coin and get moving
                GameObject coin = Instantiate(prefabCoin, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -3f), ForceMode2D.Impulse);
            }
            else if (hasCoinBag)
            {
                // create coin bag and get moving
                GameObject coinBag = Instantiate(prefabCoinBag, transform.position, Quaternion.identity);
                coinBag.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -3f), ForceMode2D.Impulse);
            }

            // spawn explosions
            if (gameObject.tag == "Alien1")
            {
                Instantiate(prefabAlienExplosion, transform.position, Quaternion.identity);
            }
            else if (gameObject.tag == "Alien2")
            {
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x - (alienWidth / 6), transform.position.y, 0f), Quaternion.identity);
                Instantiate(prefabAlienExplosion, new Vector3(transform.position.x + (alienWidth / 6), transform.position.y, 0f), Quaternion.identity);
            }
            // destroy alien
            Destroy(gameObject);

            // add alien defeated to player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.AliensDefeated.ToString(), 0) + 1);
        }
        else if (gameObject.tag == "Alien3")
        {
            for (int i = 1; i <= coinCount; i++)
            {
                // create coin and get moving
                GameObject coin = Instantiate(prefabCoin, new Vector3(Random.Range(transform.position.x - (alienWidth / 2f), transform.position.x + (alienWidth / 2f)), transform.position.y, 0f), Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -3f), ForceMode2D.Impulse);
            }

            for (int i = 1; i <= coinBagCount; i++)
            {
                // create coin bag and get moving
                GameObject coinBag = Instantiate(prefabCoinBag, new Vector3(Random.Range(transform.position.x - (alienWidth / 2f), transform.position.x + (alienWidth / 2f)), transform.position.y, 0f), Quaternion.identity);
                coinBag.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -3f), ForceMode2D.Impulse);
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

            // add mother ship defeated to player prefs
            PlayerPrefs.SetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), PlayerPrefs.GetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0) + 1);
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
        if (gameObject.tag == "Alien1")
        {
            // spawn laser and get moving
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x, transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);

            // set new cooldown
            shootTimer.Duration = Random.Range(ConfigUtils.Alien1LaserCooldownMin, ConfigUtils.Alien1LaserCooldownMax);
        }
        else if (gameObject.tag == "Alien2")
        {
            // spawn lasers and get moving
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x, transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x + (alienWidth / 4f), transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);
            Instantiate(prefabAlienLaser, new Vector3(transform.position.x - (alienWidth / 4f), transform.position.y - (alienHeight / 2f), 0f), Quaternion.identity);

            // set new cooldown
            shootTimer.Duration = Random.Range(ConfigUtils.Alien2LaserCooldownMin, ConfigUtils.Alien2LaserCooldownMax);
        }
        else if (gameObject.tag == "Alien3")
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
    /// Gets whether holding money or not and what type
    /// </summary>
    private void MoneyChance()
    {
        if (gameObject.tag == "Alien1" || gameObject.tag == "Alien2")
        {
            float hasMoney = Random.Range(0, 11);
            if (hasMoney <= 8)
            {
                float hasType = Random.Range(0, 11);
                if (gameObject.tag == "Alien1")
                {
                    if (hasType <= 8)
                    {
                        hasCoin = true;
                    }
                    else
                    {
                        hasCoinBag = true;
                    }
                }
                else if (gameObject.tag == "Alien2")
                {
                    if (hasType <= 6)
                    {
                        hasCoin = true;
                    }
                    else
                    {
                        hasCoinBag = true;
                    }
                }
            }
        }
        else if (gameObject.tag == "Alien3")
        {
            coinCount = Random.Range(10, 21);
            coinBagCount = Random.Range(4, 11);
        }
    }

    /// <summary>
    /// Sets the alien traits
    /// </summary>
    private void SetTraitValues()
    {
        // get laser speed, move speed, and health
        if (gameObject.tag == "Alien1")
        {
            moveSpeed = ConfigUtils.Alien1MoveSpeed;
            healthRemaining = ConfigUtils.Alien1LifeAmount;
            maxHealth = healthRemaining;
        }
        else if (gameObject.tag == "Alien2")
        {
            moveSpeed = ConfigUtils.Alien2MoveSpeed;
            healthRemaining = ConfigUtils.Alien2LifeAmount;
            maxHealth = healthRemaining;
        }
        else if (gameObject.tag == "Alien3")
        {
            moveSpeed = ConfigUtils.Alien3MoveSpeed;
            healthRemaining = ConfigUtils.Alien3LifeAmount;
            maxHealth = healthRemaining;
        }

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
        if (gameObject.tag == "Alien1")
        {
            shootTimer.Duration = Random.Range(ConfigUtils.Alien1LaserCooldownMin, ConfigUtils.Alien1LaserCooldownMax);
            moveTimer.Duration = ConfigUtils.Alien1MoveDelay;
        }
        else if (gameObject.tag == "Alien2")
        {
            shootTimer.Duration = Random.Range(ConfigUtils.Alien2LaserCooldownMin, ConfigUtils.Alien2LaserCooldownMax);
            moveTimer.Duration = ConfigUtils.Alien2MoveDelay;
        }
        else if (gameObject.tag == "Alien3")
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

    #endregion
}
