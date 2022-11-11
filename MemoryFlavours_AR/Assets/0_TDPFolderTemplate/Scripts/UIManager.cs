using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public GameObject startBtn;

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            trackedObjectStatus[objectToTrack] = true;
            Debug.Log(objectToTrack.name + "Tracked");
            if(objectToTrack.name == "AngKuKueh_Model")
            {
                startBtn.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
