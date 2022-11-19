using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchInteractionThree : MonoBehaviour
{
    public GameObject kuehMold;
    public GameObject dough;
    public GameObject peanutFilling;
    TriggerCheck trigger;
    TriggerCheck triggerCheck;

    public UIManager managerUI;

    public GameObject steamerCap;
    public GameObject kueh;

    public Scrollbar mixingSlider;
    int moldClickPos = 1;
    string moldTag;
    public GameObject moldUI;
    public GameObject akkModel;

    public ParticleSystem moldingPowder;
    public ParticleSystem finishMolding;
    public ParticleSystem steam;

    public AudioSource moldingSound;
    public AudioSource steaming;    

    private void Update()
    {
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

            


            moldTag = "moldCircle" + moldClickPos.ToString();

            if (hitInfo.collider.tag == moldTag)
            {
                moldClickPos++;
                hitInfo.collider.gameObject.SetActive(false);
                mixingSlider.size += 0.5f;
                moldingPowder.Play();
                moldingSound.Play();


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
