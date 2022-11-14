using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class NewBehaviourScript : MonoBehaviour
{
    public int childCount = 0;
    /// <summary>
    /// Item 1
    /// </summary>
    public GameObject theTrackable;
    private void Start()
    {
        
    }
    /// <summary>
    /// when touch , run swapmodel
    /// </summary>
    public void SwapModel()

    {
        if (childCount <= (theTrackable.transform.childCount))
        {
            GameObject trackableGameObject = theTrackable.gameObject;

            Transform child = trackableGameObject.transform.GetChild(childCount);

            ///disables first child
            child.gameObject.active = false;


            ///enables next child
            Transform child2 = trackableGameObject.transform.GetChild(childCount + 1);
            child2.gameObject.active = true;


            childCount++;
            Debug.Log(childCount);
        }
        else
        {
            Resetting();
        }



        ///putting childCount back to 0



    }
    public void Resetting()
    {
        GameObject trackableGameObject = theTrackable.gameObject;

        
            Transform child3 = trackableGameObject.transform.GetChild(childCount + 1);
            Debug.Log(child3);

            childCount = 0;
            Debug.Log("Resetted");
            Debug.Log(childCount);

        
    }
}
