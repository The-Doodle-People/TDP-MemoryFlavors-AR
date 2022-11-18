using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracker : MonoBehaviour
{
    Dictionary<GameObject, bool> objectTrackedStatus = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI displayText;

    public void ObjectTracked(GameObject objectToTrack)
    {
        if(objectToTrack != null)
        {
            objectTrackedStatus[objectToTrack] = true;
            displayText.text = objectToTrack.name + " Track Status: true";
        }
    }

    public void ObjectLost(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            objectTrackedStatus[objectToTrack] = false;
            displayText.text = objectToTrack.name + " Track Status: false";
        }
    }
}
