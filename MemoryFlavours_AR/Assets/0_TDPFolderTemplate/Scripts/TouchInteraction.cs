/*
* Author: Jasmine Giam
* Date: 20/11/2022
* Description: 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class TouchInteraction : MonoBehaviour
{
    //3d models
    public GameObject flour;
    public GameObject saltnSugar;

    /// <summary>
    /// refering to triggerCheck sript
    /// </summary>
    TriggerCheck trigger;

    //UI elements
    public Scrollbar mixingSlider;
    public GameObject mixingUI;
    public GameObject[] mixingUIChild;

    /// <summary>
    /// counter to increase the number of the tag name
    /// </summary>
    int mixClickPos = 1;

    /// <summary>
    /// string to store current tag of game object;
    /// </summary>
    string currentTag;

    /// <summary>
    /// refering to ui manager script
    /// </summary>
    public UIManager uiManager;

    /// <summary>
    /// powder particle system
    /// </summary>
    public ParticleSystem powder;

    /// <summary>
    /// mixing sfx
    /// </summary>
    public AudioSource mixing;

    
    //referring to firebase script
    public FirebaseScript firebaseScript;



    void OnTouchPress()
    {

        //x n y coordinated of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        trigger = flour.GetComponent<TriggerCheck>();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "flour")
            {
                flour.AddComponent<Lean.Touch.LeanDragTranslate>();
            }

            if (hitInfo.collider.tag == "saltnSugar" && trigger.flourEnteredBowl == true)
            {
                Debug.Log("touchedS&S");
                saltnSugar.AddComponent<Lean.Touch.LeanDragTranslate>();
            }

            //tag of gameobject player should click
            currentTag = "mixingBtn" + mixClickPos.ToString();

            if (hitInfo.collider.tag == currentTag) //checking if lpayer is clicking on the correct tag
            {
                mixClickPos++;
                hitInfo.collider.gameObject.SetActive(false);
                mixingSlider.size += 0.25f; 
                powder.Play();
                mixing.Play();

                if (mixingSlider.size == 1)
                {
                    mixingUI.SetActive(false);
                    mixingSlider.gameObject.SetActive(false);
                    uiManager.HideStepOneOrTwo();
                    mixClickPos = 1;
                    foreach (GameObject sprite in mixingUIChild)
                    {
                        sprite.SetActive(true); //show ui for next mising interaction
                    }

                }
            }
            if (mixClickPos > 4)
            {
                mixClickPos = 1; //if player clicked 4 times set it back to one
            }

        }

        if (GameManager.instance.enterMemories == true) //when player is viewing responses and they tap on the screen => get data from db
        {

            firebaseScript.GetMemory(); 

        }
    }
}

