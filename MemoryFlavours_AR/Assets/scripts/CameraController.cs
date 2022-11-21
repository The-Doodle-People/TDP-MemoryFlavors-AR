/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for camera (to toggle on and off)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// class for camera toggle 
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// link for AR object (coffee shop) 
    /// </summary>
    public GameObject coffeeShopBackground;

    /// <summary>
    /// link for AR object (3d model of aunty) 
    /// </summary>
    public GameObject Aunty;

    /// <summary>
    /// link for AR object (coffee shop sound) 
    /// </summary>
    public GameObject coffeeShopSound;

    /// <summary>
    /// link for AR object (3d cup model) 
    /// </summary>
    public GameObject cup;

    /// <summary>
    /// link for AR object (3d bandung syrup model) 
    /// </summary>
    public GameObject syrup;

    /// <summary>
    /// link for AR object (3d evaporated milk model ) 
    /// </summary>
    public GameObject milk;

    /// <summary>
    /// function to toggle camera 
    /// </summary>
    public void ToggleCamera()
    {
        // Check if Vuforia is running
        if(VuforiaBehaviour.Instance.enabled)
        {
            //make camera false
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
            //make AR objects false 
            coffeeShopBackground.SetActive(false);
            Aunty.SetActive(false);
            coffeeShopSound.SetActive(false);
            cup.SetActive(false);
            syrup.SetActive(false);
            milk.SetActive(false);

        }
        else
        {
            //else make camera true
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
            //make AR objects true 
            coffeeShopBackground.SetActive(true);
            Aunty.SetActive(true);
            coffeeShopSound.SetActive(true);
            cup.SetActive(true);
            syrup.SetActive(true);
            milk.SetActive(true);
        }
    }
}
