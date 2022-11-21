/*
 * Author: Wrigley Studios
 * Date: 20/11/22
 * Description: The Safe area is a responsive UI script, it use when the any phone layout is obstructing the UI.
 * Such as th phone notchs or hole punch camera. It will shift the panel down to fit until it is not obstructed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SafeArea : MonoBehaviour
{
    /// <summary>
    /// Get the main canvas
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// For the panel to have the safe area within the main canvas
    /// </summary>
    RectTransform panelSafeArea;

    /// <summary>
    /// Set the currentSafeArea and the screen Orientation
    /// </summary>
    Rect currentSafeArea = new Rect();
    ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;

    /// <summary>
    /// Initialize the variable and called the function
    /// </summary>
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();

        //Store current values
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

        ApplySafeArea();

    }

    /// <summary>
    /// The function calculate the space that is overlap by the notch or the camera hole punch
    /// And Create the space where there is no obstruction
    /// </summary>
     void ApplySafeArea()
     {
         if (panelSafeArea == null)
         
             return;
         
             Rect safeArea = Screen.safeArea;

             Vector2 anchorMin = safeArea.position;
             Vector2 anchorMax = safeArea.position + safeArea.size;

             anchorMin.x /= canvas.pixelRect.width;
             anchorMin.y /= canvas.pixelRect.height;

             anchorMax.x /= canvas.pixelRect.width;
             anchorMax.y /= canvas.pixelRect.height;

             panelSafeArea.anchorMin = anchorMin;
             panelSafeArea.anchorMax = anchorMax;
     }

    /// <summary>
    /// When the game is running, it will find the area where there no obstruction and creates a responsive UI
    /// </summary>
    void Update()
    {
        if ((currentOrientation != Screen.orientation) || (currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
