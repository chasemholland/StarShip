using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{

    void Awake()
    {
        // initialize config  utils
        ConfigUtils.Initialize();

        // initialize screen utils
        ScreenUtils.Initialize();

        // initialize event manager
        EventManager.Initialize();

        // set edge collider point to screen size on bottom left, top left, top right, and bottom right
        //List<Vector2> edgePoints = new List<Vector2>();

        //edgePoints.Add(new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenBottom));
        //edgePoints.Add(new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenTop));
        //edgePoints.Add(new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop));
        //edgePoints.Add(new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenBottom));

        //Camera.main.GetComponent<EdgeCollider2D>().SetPoints(edgePoints);
    }
}
