/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used to scale big and small for 3D cup model 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class scale in and out for 3d cup model
/// </summary>
public class ScaleInOut : MonoBehaviour
{
    /// <summary>
    /// to link cup model 
    /// </summary>
    public GameObject Object;

    /// <summary>
    /// bool to zoom in
    /// </summary>
    private bool _ZoomIn;

    /// <summary>
    /// bool to zoom out
    /// </summary>
    private bool _ZoomOut;

    //object scale speed
    public float Scale = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (_ZoomIn)
        {
            //make a bigger object
            Object.transform.localScale += new Vector3(Scale, Scale, Scale);
        }

        if (_ZoomOut)
        {
            //make a small object
            Object.transform.localScale -= new Vector3(Scale, Scale, Scale);
        }
    }

    /// <summary>
    /// Make object scaled big
    /// </summary>
    public void OnPressZoomIn()
    {
        _ZoomIn = true;
    }

    public void OnReleaseZoomIn()
    {
        _ZoomIn = false;
    }

    /// <summary>
    /// Make object scaled small
    /// </summary>
    public void OnPressZoomOut()
    {
        _ZoomOut = true;
    }

    public void OnReleaseZoomOut()
    {
        _ZoomOut = false;
    }
}
