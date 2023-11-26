using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Projectiles
/// </summary>
public class ShipLaser : FloatEventInvoker
{
    #region Fields

    // despawn timer
    Timer despawnTimer;

    Rigidbody2D laser;
    float laserSpeed;

    List<GameObject> aliens = new List<GameObject>();

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // get laser info
        laser = gameObject.GetComponent<Rigidbody2D>();
        laserSpeed = ConfigUtils.Ship1LaserSpeed * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0));

        if (PlayerPrefs.GetInt(PlayerPrefNames.HasTargetingSystem.ToString()) == 1)
        {
            // get all aliens
            GetAliens();

            // target chance
            float lockedOn = Random.Range(0.1f, 1.1f);
            float targetingChance = ConfigUtils.Ship1TargetingChance * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.TargetingChanceAmount.ToString(), 0));
            if (lockedOn <= targetingChance)
            {
                // check if aliens on list
                if (aliens.Count > 0)
                {
                    // get player
                    GameObject alien = aliens[0];

                    // get direction
                    Vector3 direction = alien.transform.position - gameObject.transform.position;

                    // get bullet moving towards player
                    laser.velocity = new Vector2(direction.x, direction.y).normalized * laserSpeed;

                    // get rotation amount and rotate laser
                    float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                laser.AddForce(new Vector2(0f, laserSpeed), ForceMode2D.Impulse);
            }  
        }
        else
        {
            laser.AddForce(new Vector2(0f, laserSpeed), ForceMode2D.Impulse);
        }

        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.Duration = 4;
        despawnTimer.Run();
        despawnTimer.AddTimerFinishedListener(HandleLaserDespawn);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Destroys the game object on timer finish
    /// </summary>
    private void HandleLaserDespawn()
    {
        Destroy(gameObject);
    }

    private void GetAliens()
    {
        GameObject[] alienOnes = GameObject.FindGameObjectsWithTag("Alien1");
        GameObject[] alienTwos = GameObject.FindGameObjectsWithTag("Alien2");
        GameObject alienThree = GameObject.FindWithTag("Alien3");

        if (alienOnes.Length > 0)
        {
            for (int i = 0; i < alienOnes.Length; i++)
            {
                aliens.Add(alienOnes[i]);
            }
        }

        if (alienTwos.Length > 0)
        {
            for (int i = 0; i < alienTwos.Length; i++)
            {
                aliens.Add(alienTwos[i]);
            }
        }

        if (alienThree != null)
        {
            aliens.Add(alienThree);
        }
    }

    #endregion
}
