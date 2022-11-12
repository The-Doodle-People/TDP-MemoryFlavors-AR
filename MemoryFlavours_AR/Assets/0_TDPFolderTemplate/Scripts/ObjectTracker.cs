using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;

public class ObjectTracker : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI gameText;
    public GameObject startImageTarget;
    public GameObject uiController;

    UIController uiControllerScript;
    
    void Start()
    {
        uiControllerScript = uiController.GetComponent<UIController>();
    }

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            // Update Dictionary
            Debug.Log((objectToTrack.name) + " is being tracked.");
            trackedObjectStatus[objectToTrack] = true;
            Debug.Log(objectToTrack.name);

            // Check if object to track is the start image target
            if (objectToTrack == startImageTarget && !uiControllerScript.tablePlaced)
            {
                Debug.Log("Change Text");
                // Change Game text
                gameText.text = "CLICK ANYWHERE ON THE\nSCREEN TO PLACE\nTHE TABLE!";
                uiControllerScript.canPlaceTable = true;
            }
        }

        else
        {
            return;
        }
    }

    public void ObjectLost(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            Debug.Log((objectToTrack.name) + " is no longer being tracked.");
            trackedObjectStatus[objectToTrack] = false;
            if (objectToTrack == startImageTarget && !uiControllerScript.tablePlaced)
            {
                Debug.Log("Change Text");
                // Change Game text
                gameText.text = "SCAN THE\nPOSTCARD IMAGE!";
                uiControllerScript.canPlaceTable = false;
            }
        }

        else
        {
            return;
        }
    }
}
