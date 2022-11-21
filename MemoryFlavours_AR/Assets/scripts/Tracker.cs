/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for tracker 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// class for tracker
/// </summary>
public class Tracker : MonoBehaviour
{
    Dictionary<GameObject, bool> objectTrackedStatus = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI displayText;

    /// <summary>
    /// when object is tracked 
    /// </summary>
    /// <param name="objectToTrack"></param>
    public void ObjectTracked(GameObject objectToTrack)
    {
        if(objectToTrack != null)
        {
            objectTrackedStatus[objectToTrack] = true;
            displayText.text = objectToTrack.name + " Track Status: true";
        }
    }

    /// <summary>
    /// when object is lost
    /// </summary>
    /// <param name="objectToTrack"></param>
    public void ObjectLost(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            objectTrackedStatus[objectToTrack] = false;
            displayText.text = objectToTrack.name + " Track Status: false";
        }
    }
}
