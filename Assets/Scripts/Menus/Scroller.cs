using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class Scroller : MonoBehaviour
{
    [SerializeField]
    RawImage image;

    [SerializeField]
    float x, y;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
