using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public GameObject startBtn;
    public TMP_Text step1a;

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

            if (objectToTrack.name == "table_Model")
            {
                step1a.text = "drag and drop flour, salt and sugar into red mixing bowl";
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
