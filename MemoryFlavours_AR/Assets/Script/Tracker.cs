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
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    private void Update()
    {
        UpdateObjectStatus();
    }

    public void ObjectTracked(GameObject objectToTrack)
    {   
        //If the object received is not null, set its value in the dictionary to true.
        if(objectToTrack != null)
        {
            // Set the bool of the key-value pair in the dictionary to true.
            trackedObjectStatus[objectToTrack] = true;
            displayText.text = objectToTrack.name + "Track Status: True";
        }

        else
        {
            return;
        }
    }

    public void ObjectLost(GameObject objectToTrack)
   {
        if(objectToTrack != null)
        {
            trackedObjectStatus[objectToTrack] = false;
            displayText.text = objectToTrack.name + "Track Status: false";
        }

        else
        {
            return;
        }
    }

    void UpdateObjectStatus()
    {
        if (trackedObjectStatus.Count > 0)
        {
            displayText.text = "";

            foreach (KeyValuePair<GameObject, bool> objectStatus in trackedObjectStatus)
            {
                displayText.text += objectStatus.Key.name + ": " + objectStatus.Value + '\n';
            }
        }
    }

}
