using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class TouchHandler : MonoBehaviour
{
    public Animator ingredients;
    public IceKacangTracker ice;
    public GameObject infoTxt;
    async void OnTouchPress()
    {
        ///Get the position of the touch input, which is the primaryTouch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        ///Assign the nearClipPlane value of the camera as the xÅEcoordinate
        rayPosition.z = Camera.main.nearClipPlane;

        ///The function returns a Ray, so we create a Ray variable to store it.
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);

        ///RaycastHit variable to store the information of anything the raycast detects
        RaycastHit hitInfo;

        ///Function to shoot our ray
        if (Physics.Raycast(ray,out hitInfo))
        {
            ///Checking if the object we hit has an AstronautController component 
            if (hitInfo.collider.tag=="Ice" && ice.table.GetBool("isTable")!=true && ingredients.GetBool("isIngre")!=true)
            {
                ingredients.SetBool("isIngre", true);
                ice.touchTxt.SetActive(false);
                ice.resetBtn.SetActive(true);
                ice.listBtn.SetActive(true);
                await Task.Delay(1000);
                infoTxt.SetActive(true);
                await Task.Delay(2500);
                infoTxt.SetActive(false);
                ///Touch is detected
                Debug.Log("Touch is detected");
            }
            else if (hitInfo.collider.tag == "Chendol")
            {
                ice.isChendol = true;
                ice.Toppings();
            }
            else if (hitInfo.collider.tag == "Jelly")
            {
                ice.isJelly = true;
                ice.Toppings();
            }
            else if (hitInfo.collider.tag == "Seed")
            {
                ice.isSeed = true;
                ice.Toppings();
            }
            else if (hitInfo.collider.tag == "Corn")
            {
                ice.isCorn = true;
                ice.Toppings();
            }
            else if (hitInfo.collider.tag == "Redbean")
            {
                ice.isBean = true;
                ice.Toppings();
            }
            else if (hitInfo.collider.tag == "RedS")
            {
                ice.isRedS = true;
                ice.Syrups();
            }
            else if (hitInfo.collider.tag == "BlueS")
            {
                ice.isBlueS = true;
                ice.Syrups();
            }
            else if (hitInfo.collider.tag == "GreenS")
            {
                ice.isGreenS = true;
                ice.Syrups();
            }
            else if (hitInfo.collider.tag == "Sugar")
            {
                ice.Sugar();
            }
            else if (hitInfo.collider.tag == "Milk")
            {
                ice.Milk();
            }
        }

    }

}
