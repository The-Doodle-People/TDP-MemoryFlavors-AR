/*
* Author: Janel Lim
* Date: 20/11/2022
* Description: When touch interacted for step 5 and 6
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchInteractionThree : MonoBehaviour
{
    /// <summary>
    /// GameObjects fo step 5 
    /// </summary>
    public GameObject kuehMold;
    public GameObject dough;
    public GameObject peanutFilling;
    /// <summary>
    /// Trigger checks for to ensure steps before are completed 
    /// </summary>
    TriggerCheck trigger;
    TriggerCheck triggerCheck;

    /// <summary>
    /// To call UI Manager script 
    /// </summary>
    public UIManager managerUI;


    /// <summary>
    /// GameObjects for step 6
    /// </summary>
    public GameObject steamerCap;
    public GameObject kueh;

    /// <summary>
    /// scrollbar reference to progrss bar; show if user is done with the step 
    /// </summary>
    public Scrollbar mixingSlider;

    /// <summary>
    /// increase number in tag name
    /// </summary>
    int moldClickPos = 1;
    /// <summary>
    /// call circles on the UI for molding
    /// </summary>
    string moldTag;
    public GameObject moldUI;
    /// <summary>
    /// Ang Ku Kueh model to show the user has finished molding 
    /// </summary>
    public GameObject akkModel;

    /// <summary>
    /// Particle systems for step 5 and 6
    /// </summary>
    public ParticleSystem moldingPowder;
    public ParticleSystem finishMolding;
    public ParticleSystem steam;

    /// <summary>
    /// Audio source for steps 5 and 6
    /// </summary>
    public AudioSource moldingSound;
    public AudioSource steaming;    

    private void Update()
    {
        // to move the progress bar as the kueh is steaming
        trigger = steamerCap.GetComponent<TriggerCheck>();
       
        if(trigger.steamerCapOn == true)
        {
            mixingSlider.gameObject.SetActive(true);
            if (mixingSlider.size < 1f)
            {
                if(!steam.isPlaying)
                {
                    steam.Play();
                }

                if(!steaming.isPlaying)
                {
                    steaming.Play();
                }

                mixingSlider.size += 0.2f * Time.deltaTime;
            }
            else if(mixingSlider.size ==1f)
            {
                steam.Stop();
                steaming.Stop();
                mixingSlider.gameObject.SetActive(false);
                mixingSlider.size = 0f;
                managerUI.TicksUI();
                managerUI.stepsText.text = "Ready to serve, Enjoy!";
                managerUI.nextBtn.SetActive(true);
                trigger.steamerCapOn = false;                
            }
        }
       
    }


    void OnTouchPress()
    {
        
        //x n y coordinates of the touch 
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things 
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        //get the user to be able to move the dough and the fillinsg to a specific spot
        trigger = dough.GetComponent<TriggerCheck>();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "dough")
            {
                dough.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
            if (hitInfo.collider.tag == "fillings" && trigger.doughInMold == true)
            {
                Debug.Log("on peaunt filling");
                peanutFilling.AddComponent<Lean.Touch.LeanDragTranslate>();
            }

            

            //to get the tag the player should click to mold 
            moldTag = "moldCircle" + moldClickPos.ToString();

            if (hitInfo.collider.tag == moldTag)
            {
                moldClickPos++;
                hitInfo.collider.gameObject.SetActive(false);
                mixingSlider.size += 0.5f;
                moldingPowder.Play();
                moldingSound.Play();

                //once mixing done 
                if (mixingSlider.size == 1)
                {
                    moldClickPos = 1;
                    moldUI.SetActive(false);
                    mixingSlider.gameObject.SetActive(false);
                    mixingSlider.size = 0f;
                    dough.gameObject.SetActive(false);
                    peanutFilling.gameObject.SetActive(false);
                    akkModel.gameObject.SetActive(true);
                    finishMolding.Play();
                    managerUI.TicksUI();
                    managerUI.nextBtn.SetActive(true);
                    managerUI.stepsText.text = "So far so good!";
                }
            }
           
        }

        //to let user be able to move the kueh and the stamer cover to a specific spot
        trigger = kueh.GetComponent<TriggerCheck>();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "kueh")
            {
                kueh.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
            if (hitInfo.collider.tag == "steamerCap" && trigger.kuehInSteamer == true)
            {
                steamerCap.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
            
        }

    }

}
