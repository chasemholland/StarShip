using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides screen utilies (edges, height, width)
/// </summary>
public static class ScreenUtils
{
    #region Fields

    // width and height to support resolution changes
    static int screenWidth;
    static int screenHeight;

    // boundry checking
    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    #endregion

    #region Properties

    /// <summary>
    /// Gets screen left in world coordinates
    /// </summary>
    public static float ScreenLeft
    {
        get
        {
            CheckScreenSizeChanged();
            return screenLeft;
        }
    }

    /// <summary>
    /// Gets screen right in world coordinates
    /// </summary>
    public static float ScreenRight
    {
        get
        {
            CheckScreenSizeChanged();
            return screenRight;
        }
    }

    /// <summary>
    /// Gets screen top in world coordinates
    /// </summary>
    public static float ScreenTop
    {
        get
        {
            CheckScreenSizeChanged();
            return screenTop;
        }
    }

    /// <summary>
    /// Gets screen bottom in world coordinates
    /// </summary>
    public static float ScreenBottom
    {
        get
        {
            CheckScreenSizeChanged();
            return screenBottom;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize screen utils
    /// </summary>
    public static void Initialize()
    {
        // support screen resolution changes
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // get screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCorner = new Vector3(0, 0, screenZ);
        Vector3 upperRightCorner = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCorner);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCorner);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;

    }

    /// <summary>
    /// Checks for screen size change and re initializes if changes are found
    /// </summary>
    static void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            Initialize();
        }
    }

    #endregion
}
