using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracker : MonoBehaviour
{
    Dictionary<GameObject, bool> objectTrackedStatus = new Dictionary<GameObject, bool>();

    public void ObjectTracked(GameObject trackedObj)
    {

    }

    public void ObjectLost(Gameobject trackedObj)
    {

    }
}
