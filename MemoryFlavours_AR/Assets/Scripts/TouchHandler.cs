using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    public GameObject startAnim;
    public GameObject stopAnim;
    public GameObject chinaTown;
    public GameObject kueh;
    public GameObject mooncake;
    public GameObject mosque;
    public GameObject littleIndia;
    public GameObject prata;
    public GameObject imageChinaTown;
    public GameObject imageKueh;
    public GameObject imageMooncake;
    public GameObject imageMosque;
    public GameObject imageLittleIndia;
    public GameObject imagePrata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTouchPress()
    {
        Debug.Log("Test");
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
