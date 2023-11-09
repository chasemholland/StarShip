using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Explosion animation control
/// </summary>
public class Explosion : FloatEventInvoker
{
    [SerializeField]
    Sprite endSprite;

    /// <summary>
    /// Start is called before the first update
    /// </summary>
    private void Start()
    {
        // add as invoker for invasion complete event
        unityEvents.Add(EventName.InvasionCompleteEvent, new InvasionCompleteEvent());
        EventManager.AddInvoker(EventName.InvasionCompleteEvent, this);
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == endSprite)
        {
            // check if player dead and playing last explosion
            if (gameObject.tag == "ShipExplosion")
            {
                // invoke invasion complete
                unityEvents[EventName.InvasionCompleteEvent].Invoke(0);
            }
            
            // destroy animation
            Destroy(gameObject);
        }
    }
}
