/*
* Author: Wong Qing Wei, Jasmine Giam, Janel Lim
* Date: 20/11/2022
* Description: trigger check script to check for user inputs
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TriggerCheck : MonoBehaviour
{
    /// <summary>
    /// checking if the flour has entered the bowl
    /// </summary>
    public bool flourEnteredBowl = false;

    
    Rigidbody rb;
    /// <summary>
    /// position for flour to be
    /// </summary>
    public Transform flourHoldArea;

    /// <summary>
    /// position for salt&sugar to be
    /// </summary>
    public Transform saltSugarHoldArea;
    /// <summary>
    /// position for beet root juice to be
    /// </summary>
    public Transform btJuiceHoldArea;
    
    /// <summary>
    /// ui that shows the steps player needs to take
    /// </summary>
    public TMP_Text steps;
    
    /// <summary>
    /// ui to show where the player shld click to mix
    /// </summary>
    public GameObject mixingUI;
    /// <summary>
    /// gameobject that stores the mixing scrollbar that shows progress of mixing
    /// </summary>
    public GameObject mixingSlider;

    /// <summary>
    /// powder pparticle system
    /// </summary>
    public ParticleSystem powder;
    /// <summary>
    /// splash particle system
    /// </summary>
    public ParticleSystem splash;

    //sound effects
    public AudioSource puff;
    public AudioSource waterDrop;
    public AudioSource pouring;


    /// <summary>
    /// Transform position of empty game object
    /// </summary>
    public Transform peanutArea;

    /// <summary>
    /// to store text
    /// </summary>
    public TMP_Text emptyText;

    /// <summary>
    /// game object to store empty bowl
    /// </summary>
    public GameObject noFillingsBowl;

    /// <summary>
    /// game object to contain bowl with fillings inside 
    /// </summary>
    public GameObject bowlBlendedFillings;

    /// <summary>
    /// game object of blender cap
    /// </summary>
    public GameObject blenderCap;

    /// <summary>
    /// game object to store done up peanut area 
    /// </summary>
    public GameObject blendedPeanutArea;

    /// <summary>
    /// boolean to check if blender is ready to blend
    /// </summary>
    public bool readyToBlend = false;

    /// <summary>
    /// particle system for effects of putting fillings to blender sound 
    /// </summary>
    public ParticleSystem peanutToBlenderEffect;


    //step 5
    public bool doughInMold = false;
    public bool fillingInMold = false;

    public Transform doughPosition;
    public Transform fillingPosition;

    public GameObject moldingUI;

    public ParticleSystem moldingPower;

    //Step 6
    public bool kuehInSteamer = false;
    public bool steamerCapOn = false;

    public Transform angkkPos;
    public Transform steamerCapPos;
    public UIManager uiMgr;

    public GameObject coverLabel;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bowlTrigger" )
        {
            if (gameObject.tag == "flour") //once flour is inside the bowl
            {
                flourEnteredBowl = true;
                adjustComponents();
                gameObject.transform.position = flourHoldArea.position; //setting position of model
                powder.Play();
                puff.Play();
                
            }
           
            if(gameObject.tag == "saltnSugar") //once salt n sugar is inside bowl
            {
                
                adjustComponents();
               
                gameObject.transform.position = saltSugarHoldArea.position; //setting position of model
                steps.text = "Tap the circles in the correct order to mix the ingredients";
                pouring.Play();

                mixingUI.SetActive(true);
                mixingSlider.SetActive(true);
            }

            if(gameObject.tag == "btJuice") //once beet root juice is inside bowl;
            {
                adjustComponents();
                gameObject.transform.position= btJuiceHoldArea.position; //setting position of model
                steps.text = "Tap the circles in the correct order to mix the ingredients";
                mixingUI.SetActive(true);
                mixingSlider.GetComponent<Scrollbar>().size= 0;
                mixingSlider.SetActive(true);
                waterDrop.Play();
                splash.Play();
            }
        }

        // once fillings has interacted with blender trigger
        if(other.gameObject.tag == "blenderTrigger")
        {
            // once peanut fillings detected
            if(gameObject.tag == "peanutFillings")             
            {
                adjustComponents();
                gameObject.transform.position = peanutArea.position;
                emptyText.text = "Empty Bowl";
                noFillingsBowl.SetActive(false);
                bowlBlendedFillings.SetActive(true);
                peanutToBlenderEffect.Play();
            }

            // once blender acp is placed
            if(gameObject.tag == "blenderCap")
            {
                gameObject.SetActive(false);
                blenderCap.SetActive(true);
                steps.text = "Blending in process!";
                readyToBlend = true;
                mixingSlider.GetComponent<Scrollbar>().size = 0;
                mixingSlider.SetActive(true);
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        if(other.gameObject.tag == "placePeanutTrigger")
        {
            if(gameObject.tag == "blendedPeanuts")
            {
                gameObject.SetActive(false);
                emptyText.text = "Blended Fillings";
                Destroy(gameObject.GetComponent<Lean.Touch.LeanDragTranslate>());
                blendedPeanutArea.SetActive(true);
                uiMgr.TicksUI();
                adjustComponents();
            }
        }

        if (other.gameObject.tag == "moldTrigger")
        {
            if (gameObject.tag == "dough")
            {
                doughInMold = true;
                adjustComponents();
                gameObject.transform.position = doughPosition.position;
            }
            if (gameObject.tag == "fillings")
            {
                fillingInMold = true;
                moldingUI.SetActive(true);
                steps.text = "Tap the circles in the correct order to shape the Ang Ku Kueh";
                gameObject.transform.position = fillingPosition.position;
                adjustComponents();
            }
        }

        if (other.gameObject.tag == "steamTrigger")
        {
            if (gameObject.tag == "kueh")
            {
                kuehInSteamer = true;
                gameObject.transform.position = angkkPos.position;
                adjustComponents();
            }
            if (gameObject.tag == "steamerCap")
            {
                gameObject.transform.position = steamerCapPos.position;
                adjustComponents();
                steamerCapOn = true;
                steps.text = "Steaming in process!";
                coverLabel.SetActive(false);
            }
        }

    }

    /// <summary>
    /// make model not moveable
    /// </summary>
    void adjustComponents()
    {
        rb = gameObject.GetComponent<Rigidbody>(); 
        rb.drag = 10;
        Destroy(gameObject.GetComponent<Lean.Touch.LeanDragTranslate>());
        Destroy(rb);
    }
    
}
