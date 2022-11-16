using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TriggerCheck : MonoBehaviour
{
    public bool flourEnteredBowl = false;
  
    
    Rigidbody rb;
    public Transform flourHoldArea;
    public Transform saltSugarHoldArea;
    public Transform btJuiceHoldArea;
    public TMP_Text steps;
    
    public GameObject mixingUI;
    public GameObject mixingSlider;
    public ParticleSystem powder;
    public ParticleSystem splash;

    //step 4
    public Transform peanutArea;
    public TMP_Text emptyText;
    public GameObject noFillingsBowl;
    public GameObject bowlBlendedFillings;
    public GameObject blenderCap;
    public GameObject blendedPeanutArea;
    public bool readyToBlend = false;
    public ParticleSystem peanutToBlenderEffect;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bowlTrigger" )
        {
            if (gameObject.tag == "flour")
            {
                flourEnteredBowl = true;
                adjustComponents();
                gameObject.transform.position = flourHoldArea.position;
                powder.Play();
                
            }
           
            if(gameObject.tag == "saltnSugar")
            {
                
                adjustComponents();
               
                gameObject.transform.position = saltSugarHoldArea.position;
                steps.text = "tap all the buttons in the correct order to mix the dry ingredients";
               

                mixingUI.SetActive(true);
                mixingSlider.SetActive(true);
            }

            if(gameObject.tag == "btJuice")
            {
                adjustComponents();
                gameObject.transform.position= btJuiceHoldArea.position;
                steps.text = "tap all the buttons in the correct order to mix the ingredients into a dough";
                mixingUI.SetActive(true);
                mixingSlider.GetComponent<Slider>().value= 0;
                mixingSlider.SetActive(true);
                splash.Play();
            }
        }

        if(other.gameObject.tag == "blenderTrigger")
        {
            if(gameObject.tag == "peanutFillings")
            {
                adjustComponents();
                gameObject.transform.position = peanutArea.position;
                emptyText.text = "Empty Bowl";
                noFillingsBowl.SetActive(false);
                bowlBlendedFillings.SetActive(true);
                peanutToBlenderEffect.Play();
            }

            if(gameObject.tag == "blenderCap")
            {
                gameObject.SetActive(false);
                blenderCap.SetActive(true);
                steps.text = "Blending in process!";
                readyToBlend = true;
                mixingSlider.GetComponent<Slider>().value = 0;
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
                adjustComponents();
            }
        }

    }

    void adjustComponents()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.drag = 10;
        Destroy(gameObject.GetComponent<Lean.Touch.LeanDragTranslate>());
        Destroy(rb);
    }
    
}
