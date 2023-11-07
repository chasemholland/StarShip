using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Money behavior (coin & coin bag)
/// </summary>
public class Money : MonoBehaviour
{
    #region Fields

    // despawn timer
    Timer despawnTimer;

    // rotation
    Timer rotationTimer;
    float rotationAmount = 10;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // despawn support
        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.Duration = 3;
        despawnTimer.Run();
        despawnTimer.AddTimerFinishedListener(HandleMoneyDespawn);

        // rotation support
        rotationTimer = gameObject.AddComponent<Timer>();
        rotationTimer.Duration = 0.1f;
        rotationTimer.Run();
        rotationTimer.AddTimerFinishedListener(HandleMoneyRotation);
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
    private void HandleMoneyDespawn()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Rotates the given game object on timer finished event
    /// </summary>
    private void HandleMoneyRotation()
    {
        // rotate game object
        gameObject.transform.Rotate(0, rotationAmount, 0);

        // reset timer
        rotationTimer.Run();
    }
    #endregion
}
