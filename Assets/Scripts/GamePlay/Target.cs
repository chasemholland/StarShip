using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds all targets
/// </summary>
public class Target : IComparable
{
    #region Fields

    // target game object
    GameObject gameObject;

    // distance to the target
    float distance;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructs a target with given game object and
    /// the position of the player ship
    /// </summary>
    /// <param name="gameObject">game object</param>
    /// <param name="position">ship position</param>
    public Target(GameObject gameObject, Vector3 position)
    {
        this.gameObject = gameObject;
        UpdateDistance(position);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the target game object
    /// </summary>
    public GameObject GameObject
    {
        get { return gameObject; }
    }

    /// <summary>
    /// Gets the distance to the target
    /// </summary>
    public float Distance
    {
        get { return distance; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the distance from the targetToBe game object
    /// to the given player ship position
    /// </summary>
    /// <param name="position">ship position</param>
    public void UpdateDistance(Vector3 position)
    {
        distance = Vector3.Distance(gameObject.transform.position, position);
    }

    /// <summary>
    /// Compares the current target with another target 
    /// and returns an integer that indicates whether the current target 
    /// precedes, follows, or occurs in the same position in the sort order
    /// as the other target.
    /// </summary>
    /// <param name="obj">given target</param>
    /// <returns>relative order of current target and given target</returns>
    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        Target otherTarg = obj as Target;
        if (otherTarg != null)
        {
            if (Distance < otherTarg.Distance)
            {
                return -1;
            }
            else if (Distance == otherTarg.Distance)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            // if not comparable throw exception
            throw new ArgumentException("Object is not a GameObjet");
        }
    }

    /// <summary>
    /// Converts the target to a string
    /// </summary>
    /// <returns>the string for the taget</returns>
    public override string ToString()
    {
        return distance.ToString();
    }

    #endregion
}
