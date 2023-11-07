using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event Manager
/// </summary>
public static class EventManager
{
    #region Fields

    static Dictionary<EventName, List<IntEventInvoker>> invokers = 
        new Dictionary<EventName, List<IntEventInvoker>>();

    static Dictionary<EventName, List<UnityAction<int>>> listeners = 
        new Dictionary<EventName, List<UnityAction<int>>>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Initialize the event dictionaries
    /// </summary>
    public static void Initialize()
    {
        // create empty list for each name in dictionary
        foreach (EventName name in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(name))
            {
                invokers.Add(name, new List<IntEventInvoker>());
                listeners.Add(name, new List<UnityAction<int>>());
            }
            else
            {
                invokers[name].Clear();
                listeners[name].Clear();
            }
        }
    }

    /// <summary>
    /// Adds invoker for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="invoker">Invoker</param>
    public static void AddInvoker(EventName name, IntEventInvoker invoker)
    {
        // add listeners to new invoker and add invoker to dictionary
        foreach (UnityAction<int> listener in listeners[name])
        {
            invoker.AddListener(name, listener);
        }
        invokers[name].Add(invoker);
    }

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName/param>
    /// <param name="listener">Listener</param>
    public static void AddListener(EventName name, UnityAction<int> listener)
    {
        // add as listener to all invokers and add new listener to dictionary
        foreach (IntEventInvoker invoker in invokers[name])
        {
            invoker.AddListener(name, listener);
        }
        listeners[name].Add(listener);
    }

    /// <summary>
    /// Removes invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveInvoker(EventName name, IntEventInvoker invoker)
    {
        // remove invoker from dictionary
        invokers[name].Remove(invoker);
    }

    #endregion
}
