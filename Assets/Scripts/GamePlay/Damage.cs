using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

/// <summary>
/// Damage amount behaviour
/// </summary>
public class Damage : MonoBehaviour
{
    #region Fields

    // damage text set top damage amount
    //[SerializeField]
    //TextMeshPro damageText;

    // used for tweening
    Rigidbody2D damageBody;

    // life span
    Timer lifeTimer;

    // scale timer
    Timer scaleTimer;
    bool upScaled = false;
    int scaleAmount = 0;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // get refernece to damage body
        damageBody = GetComponent<Rigidbody2D>();

        // start tweening
        damageBody.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);

        // set life timer
        lifeTimer = gameObject.AddComponent<Timer>();
        lifeTimer.Duration = 1;
        lifeTimer.Run();
        lifeTimer.AddTimerFinishedListener(HandleDespawn);

        // set scale timer
        scaleTimer = gameObject.AddComponent<Timer>();
        scaleTimer.Duration = 0.05f;
        scaleTimer.Run();
        scaleTimer.AddTimerFinishedListener(HandleScaling);
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
    /// Destroys game object at timer finish
    /// </summary>
    private void HandleDespawn()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Scales the text
    /// </summary>
    private void HandleScaling()
    {
        if (scaleAmount > 10)
        {
            upScaled = true;
        }

        if (!upScaled)
        {
            Vector3 textScale = gameObject.transform.localScale;
            textScale.x = textScale.x * 1.05f;
            textScale.y = textScale.y * 1.05f;
            gameObject.transform.localScale = textScale;
            scaleAmount += 1;
        }
        else
        {
            Vector3 textScale = gameObject.transform.localScale;
            textScale.x = textScale.x / 1.05f;
            textScale.y = textScale.y / 1.05f;
            gameObject.transform.localScale = textScale;
        }

        scaleTimer.Run();
    }

    #endregion
}
