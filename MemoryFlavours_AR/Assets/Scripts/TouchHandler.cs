/*
 * Author: Shi Jie, Anqi, Jessica
 * Date: 15/11/22
 * Description: Use Touch input and raycast to change display
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    /// <summary>
    /// Stores start animation button
    /// </summary>
    public GameObject startAnim;

    /// <summary>
    /// Stores stop animation button
    /// </summary>
    public GameObject stopAnim;

    /// <summary>
    /// Stores chinatown model 
    /// </summary>
    public GameObject chinaTown;

    /// <summary>
    /// Stores tutu kueh model 
    /// </summary>
    public GameObject kueh;

    /// <summary>
    /// Stores mooncake model 
    /// </summary>
    public GameObject mooncake;

    /// <summary>
    /// Stores mosque model 
    /// </summary>
    public GameObject mosque;

    /// <summary>
    /// Stores little india model 
    /// </summary>
    public GameObject littleIndia;

    /// <summary>
    /// Stores prata model 
    /// </summary>
    public GameObject prata;

    /// <summary>
    /// Stores chinatown image description 
    /// </summary>
    public GameObject imageChinaTown;

    /// <summary>
    /// Stores tutu kueh image description 
    /// </summary>
    public GameObject imageKueh;

    /// <summary>
    /// Stores mooncake image description 
    /// </summary>
    public GameObject imageMooncake;

    /// <summary>
    /// Stores mosque image description 
    /// </summary>
    public GameObject imageMosque;

    /// <summary>
    /// Stores little india image description 
    /// </summary>
    public GameObject imageLittleIndia;

    /// <summary>
    /// Stores prata image description 
    /// </summary>
    public GameObject imagePrata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // use raycast to find object hit and change display
    void OnTouchPress()
    {
        //Debug.Log("Test");
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        rayPosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo; // store information of any hit detected by raycast

        if (Physics.Raycast(ray, out hitInfo))
        {

            if (hitInfo.collider.tag == "Chinatown")
            {
                chinaTown.SetActive(false);
                mooncake.SetActive(true);
                imageChinaTown.SetActive(false);
                imageMooncake.SetActive(true);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }

            if (hitInfo.collider.tag == "Mooncake")
            {
                chinaTown.SetActive(true);
                mooncake.SetActive(false);
                imageChinaTown.SetActive(true);
                imageMooncake.SetActive(false);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }

            if (hitInfo.collider.tag == "Mosque")
            {
                mosque.SetActive(false);
                kueh.SetActive(true);
                imageMosque.SetActive(false);
                imageKueh.SetActive(true);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }

            if (hitInfo.collider.tag == "Kueh")
            {
                kueh.SetActive(false);
                mosque.SetActive(true);
                imageKueh.SetActive(false);
                imageMosque.SetActive(true);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }

            if (hitInfo.collider.tag == "LittleIndia")
            {
                littleIndia.SetActive(false);
                prata.SetActive(true);
                imageLittleIndia.SetActive(false);
                imagePrata.SetActive(true);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }

            if (hitInfo.collider.tag == "Prata")
            {
                prata.SetActive(false);
                littleIndia.SetActive(true);
                imagePrata.SetActive(false);
                imageLittleIndia.SetActive(true);
                startAnim.SetActive(true);
                stopAnim.SetActive(false);
            }
        }
    }
}
