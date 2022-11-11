using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTracker : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI displayText;

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            //Update the dictionary
            trackedObjectStatus[objectToTrack] = true;

            Debug.Log(objectToTrack.name + "is being tracked");
            displayText.text = objectToTrack.name + " is being tracked";
        }
        else
        {
            return;
        }
    }
}
