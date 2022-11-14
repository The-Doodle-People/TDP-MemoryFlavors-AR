using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracker : MonoBehaviour
{
    ///summary
    ///This dictionary holds the tracked status of the AR objects

    Dictionary<GameObject, bool> objectsTracked = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI displayText;
    public void ObjectTracked(GameObject objectToTrack)
    {

        if (objectToTrack != null)
        {
            {
                objectsTracked[objectToTrack] = true;
                displayText.text = objectToTrack.name + " Track status : true";
            }
        }

    }
    public void ObjectLost(GameObject objectToTrack)
    {

        if (objectToTrack != null)
        {
            {
                objectsTracked[objectToTrack] = false;

                displayText.text = objectToTrack.name + " Track status : false";

            }
        }

    }
}

