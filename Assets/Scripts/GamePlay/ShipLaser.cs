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

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.Duration = 1;
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

    #endregion
}
