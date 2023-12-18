using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Alien projectiles
/// </summary>
public class AlienLaser : FloatEventInvoker
{
    #region Fields

    [SerializeField]
    GameObject prefabShipLaserExplosion;

    // despawn timer
    Timer despawnTimer;

    // laser body
    Rigidbody2D laser;
    float laserSpeed;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (gameObject.CompareTag("Alien1Laser"))
        {
            laserSpeed = ConfigUtils.Alien1LaserSpeed;
        }
        else if (gameObject.CompareTag("Alien2Laser"))
        {
            laserSpeed = ConfigUtils.Alien2LaserSpeed;
        }
        else if (gameObject.CompareTag("Alien3Laser"))
        {
            laserSpeed = ConfigUtils.Alien3LaserSpeed;
        }

        // get laser rigidbody2d
        laser = gameObject.GetComponent<Rigidbody2D>();

        // check if player alive
        if (GameObject.FindWithTag("Player") != null)
        {
            // get player
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // get direction
            Vector3 direction = player.transform.position - gameObject.transform.position;

            // get bullet moving towards player
            laser.velocity = new Vector2(direction.x, direction.y).normalized * laserSpeed;

            // get rotation amount and rotate laser
            float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        }
        // destroy unnecessary lasers
        else
        {
            Destroy(gameObject);
        }

        // set and run despawn timer
        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.Duration = 4;
        despawnTimer.Run();
        despawnTimer.AddTimerFinishedListener(HandleLaserDespawn);
    }

    /// <summary>
    /// Collision triggered event
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ShipLaser1"))
        {
            // play laser hit
            AudioManager.Play(AudioName.ShipLaserHit);

            // get laser height
            float otherHalfheight = other.GetComponent<BoxCollider2D>().size.y / 2;

            // spawn explosion
            Instantiate(prefabShipLaserExplosion, new Vector3(other.transform.position.x, other.transform.position.y - otherHalfheight, 0f), Quaternion.identity);

            // destroy alien laser
            Destroy(other.gameObject);

            // destroy alien laser
            Destroy(gameObject);
        }

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

    #endregion
}
