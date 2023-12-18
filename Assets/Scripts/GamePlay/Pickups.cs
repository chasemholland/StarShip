using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Money behavior (coin & coin bag)
/// </summary>
public class Pickups : MonoBehaviour
{
    #region Fields

    // despawn timer
    Timer despawnTimer;

    // rotation
    Timer rotationTimer;
    float rotationAmount = 10;

    // ship magnnet rannge
    float magnetRange;
    bool magnetized = false;
    bool coinOrBag = false;
    GameObject player;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // set up magnet trait
        SetupMagnet();

        // despawn support
        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.Duration = 3;
        despawnTimer.Run();
        despawnTimer.AddTimerFinishedListener(HandlePickupDespawn);

        // rotation support
        rotationTimer = gameObject.AddComponent<Timer>();
        rotationTimer.Duration = 0.1f;
        rotationTimer.Run();
        rotationTimer.AddTimerFinishedListener(HandlePickupRotation);
    }

    /// <summary>
    /// Fixed Update is called 50 per second
    /// </summary>
    private void FixedUpdate()
    {
        // only do other calculationn if is a coin or bag
        if (coinOrBag)
        {
            // check if magnet active
            if (magnetRange > 0 && !magnetized)
            {
                // if pickup was within magnet range, move towards to player
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= magnetRange)
                {
                    magnetized = true;
                }
            }

            if (magnetized)
            {
                // get direction
                Vector3 direction = player.transform.position - gameObject.transform.position;

                // get bullet moving towards player
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 15;
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets up magnet trait if active
    /// </summary>
    private void SetupMagnet()
    {
        // check if coin or bag for magnet
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("CoinBag"))
        {
            coinOrBag = true;

            // get ship magnet range
            if (PlayerPrefs.GetInt(PlayerPrefNames.HasMagnet.ToString(), 0) == 1)
            {
                // get player reference
                player = GameObject.FindWithTag("Player");

                // get range
                magnetRange = ConfigUtils.Ship1MagnetRange * (1 + PlayerPrefs.GetFloat(PlayerPrefNames.MagnetRange.ToString(), 0));
            }
            else
            {
                // default to 0, no range
                magnetRange = 0;
            }
        }
        else
        {
            // default to 0, no range
            magnetRange = 0;
        }
    }

    /// <summary>
    /// Destroys the game object on timer finish
    /// </summary>
    private void HandlePickupDespawn()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Rotates the given game object on timer finished event
    /// </summary>
    private void HandlePickupRotation()
    {
        // rotate game object
        gameObject.transform.Rotate(0, rotationAmount, 0);

        // reset timer
        rotationTimer.Run();
    }
    #endregion
}
