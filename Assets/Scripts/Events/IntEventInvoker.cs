using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Extends MonoBehaviour to support invoking one integer argument events
/// </summary>
public class IntEventInvoker : MonoBehaviour
{
    #region Fields

    protected Dictionary<EventName, UnityEvent<int>> unityEvents = 
        new Dictionary<EventName, UnityEvent<int>>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public void AddListener(EventName name, UnityAction<int> listener)
    {
        // add listeners for supported events
        if (unityEvents.ContainsKey(name))
        {
            unityEvents[name].AddListener(listener);
        }
    }

    #endregion
}
