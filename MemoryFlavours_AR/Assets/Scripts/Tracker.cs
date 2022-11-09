using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class Tracker : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    /// <summary>
    /// This dictionary holds the tracked status of AR objects
    /// </summary>
    Dictionary<GameObject, bool> objectsTracked = new Dictionary<GameObject, bool>();

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            objectsTracked[objectToTrack] = true;
            displayText.text = objectToTrack.name + "Track Status: True";
        }
    }

    public void ObjectLost(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            objectsTracked[objectToTrack] = false;
            displayText.text = objectToTrack.name + "Track Status: False";
        }
    }
}
