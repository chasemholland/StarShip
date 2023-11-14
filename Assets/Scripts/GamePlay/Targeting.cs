using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the current target
/// </summary>
public class Targeting : MonoBehaviour
{
    #region Fields

    /*
    // targeting support
    SortedList<Target> targets = new SortedList<Target>();
    Target targetAlien = null;
    bool initialTargetSet = false;
    */

    List<GameObject> targets = new List<GameObject>();
    GameObject target;

    Rigidbody2D targetBody;

    #endregion

    #region Unity Methods
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // get referenc to body
        targetBody = GetComponent<Rigidbody2D>();

        // add as listener for alien died event in order to change targets
        AddTargets();

        // sets initial target
        SetTarget();

        GoToTarget();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (target == null)
        {
            
        }
    }

    /*
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == targetAlien.GameObject)
        {
            gameObject.transform.position = other.transform.position;
        }
    }

    #endregion

    #region Public Methods


    public void SetTarget(GameObject alien)
    {
        // create new target
        Target newTarget = new Target(alien, GameObject.FindWithTag("Player").transform.position);

        // set target
        targetAlien = newTarget;

        GoToTarget();

        initialTargetSet = true;

    }
    */

    /// <summary>
    /// Sets the target
    /// </summary>
    public void SetTarget()
    {
        target = targets[0];
    }
    
    /// <summary>
    /// Sends target game objcet to target
    /// </summary>
    public void GoToTarget()
    {
        /*
        Vector2 direction = new Vector2(
            target.transform.position.x - transform.position.x,
            target.transform.position.y - transform.position.y);
        direction.Normalize();
        targetBody.velocity = Vector2.zero;
        targetBody.AddForce(direction * -10,
            ForceMode2D.Impulse);
        */

    }
    

    /// <summary>
    /// Adds all targets to list of target game objects
    /// </summary>
    /// <param name="n">unused</param>
    public void AddTargets()
    {
        // find all aliens in the scene
        GameObject[] alien1Objects = GameObject.FindGameObjectsWithTag("Alien1");
        GameObject[] alien2Objects = GameObject.FindGameObjectsWithTag("Alien2");
        GameObject alien3Object = GameObject.FindWithTag("Alien3");

        // add alien1 objects as targets
        if (alien1Objects.Length > 0)
        {
            for (int i = 0; i < alien1Objects.Length; i++)
            {
                // add to sorted list
                targets.Add(alien1Objects[i]);
            }
        }

        // add alien2 objects as targets
        if (alien2Objects.Length > 0)
        {
            for (int i = 0; i < alien2Objects.Length; i++)
            {
                // add to sorted list
                targets.Add(alien2Objects[i]);
            }
        }

        // add alien3 object as target
        if (alien3Object != null)
        {
            // add to sorted list
            targets.Add(alien3Object);
        }

    }

    #endregion
}
