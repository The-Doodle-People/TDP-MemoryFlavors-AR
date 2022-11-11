using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCamera : MonoBehaviour
{
    private int zoomAmount;
    public Camera publicCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn()
    {
        if (zoomAmount >= 0 && zoomAmount <=100)
        publicCamera.fieldOfView = (zoomAmount + 50);
        zoomAmount += 50;
        Debug.Log("testing");
    }

    public void ZoomOut()
    {
        if (zoomAmount >= 0 && zoomAmount <= 100)
        {
            publicCamera.fieldOfView = (zoomAmount - 50);
            zoomAmount -= 50;
            Debug.Log("testing");

        }
    }
}
