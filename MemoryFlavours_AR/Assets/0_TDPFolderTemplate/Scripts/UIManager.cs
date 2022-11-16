using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public GameObject startBtn;
    public GameObject angKuKueh;
    public TMP_Text stepsText;
    public GameObject step2;
    public GameObject step1;
    public GameObject step3;
    public GameObject step4;
    public GameObject tableBowl;
   
    public GameObject mixedDryIngreModel;
    public GameObject nextBtn;
    
    public GameObject mixedDryIngreWWell;
    public GameObject mixedLiquid;
    int currentStep = 1;

    //step 3
    public Slider kneadSlider;
    public Slider mixingSlider;
    public GameObject kneadCanva;
    public GameObject kneadedDough;
    public GameObject guideArrow;
    int targetKnead = 20;
    int currentKnead;
    bool targetTF;

    //step4
    TriggerCheck triggerCheck;
    public GameObject blenderCap; // recall script bool 
    public GameObject aftBlenderCap;
    public GameObject blendedPeanuts;
    public GameObject notBlendedPeanuts;
    public GameObject aftBlendedPeanuts;
    public GameObject blendedCapUi;
    public bool finishBlend;

    public ParticleSystem kneadParticles;
    public ParticleSystem blendingEffect;
    public ParticleSystem peanutBlendedEffect;
    public ParticleSystem aftKneadingEffect;

    public AudioSource kneadSound;
    public AudioSource blendingSound;

    private void Update()
    {
        if (kneadCanva.activeSelf)
        {            
            kneadSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

            if (kneadSlider.value == kneadSlider.maxValue) // complete one round of kneading
            {
                var input = EventSystem.current.GetComponent<StandaloneInputModule>(); // deactivate constant drag slider
                targetTF = true; //means it has rch the max point
                input.DeactivateModule();
                if(targetTF)
                {                    
                    currentKnead += 2;
                    mixingSlider.value += 0.1f;
                    targetTF = false;
                    kneadSlider.value = 0f;
                }
            }

            if(currentKnead == targetKnead) // once kneading done
            {
                if (mixingSlider.value == 1)
                {
                    mixingSlider.gameObject.SetActive(false);
                    aftKneadingEffect.Play();
                    mixingSlider.value = 0f;
                    mixedLiquid.SetActive(false);
                    guideArrow.SetActive(false);
                    kneadedDough.SetActive(true);
                    stepsText.text = "Good Job! Dough looking good";
                    nextBtn.SetActive(true);
                }
            };
        }

        if(step4.activeSelf)
        {
            triggerCheck = blenderCap.GetComponent<TriggerCheck>();

            if (triggerCheck.readyToBlend == true)
            {
                blendedCapUi.SetActive(false);
                
                if (mixingSlider.value < 1f)
                {
                    if(!blendingEffect.isPlaying)
                    {
                        blendingEffect.Play();
                    }
                    if(!blendingSound.isPlaying)
                    {
                        blendingSound.Play();
                    }
                    mixingSlider.value += 0.2f * Time.deltaTime;
                }
                else if(mixingSlider.value == 1f)
                {
                    blendingEffect.Stop();
                    if (blendingSound.isPlaying)
                    {
                        blendingSound.Stop();
                    }

                    aftBlenderCap.SetActive(false);
                    stepsText.text = "Take out the peanut fillings and place to empty bowl";
                    notBlendedPeanuts.SetActive(false);
                    blendedPeanuts.SetActive(true);
                    mixingSlider.gameObject.SetActive(false);
                    finishBlend = true;
                }
            } 
            
            if(aftBlendedPeanuts.activeSelf)
            {
                blendedPeanuts.SetActive(false);
                if(!peanutBlendedEffect.isPlaying)
                {
                    peanutBlendedEffect.Play();
                }
                stepsText.text = "Well done!";
                nextBtn.SetActive(true);
            }
        }
    }

    public void ValueChangeCheck() // a callback function to check if slider value has change, if yes means user is using the slider
    {
        mixedLiquid.transform.Rotate(0, 90 * Time.deltaTime, 0, Space.World);
        kneadParticles.Play();
        kneadSound.Play();
    }

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {

            trackedObjectStatus[objectToTrack] = true;
            Debug.Log(objectToTrack.name + "Tracked");
            if(objectToTrack.name == "AngKuKueh_Model")
            {
                startBtn.SetActive(true);
            }else if (objectToTrack.name == "table_model")
            {
                stepsText.text = "drag and drop flour, salt and sugar into yellow mixing bowl";
               
            }else if(objectToTrack.name == "BlenderBase")
            {
                stepsText.text = "drag and drop peanut fillings to blender";
                startBtn.SetActive(false);
            }

        }
        else
        {
            return;
        }
    }
    
    
    

    public void HideStepOneOrTwo()
    {
        if(step2.gameObject.activeSelf)
        {
            step2.SetActive(false);
            mixedLiquid.SetActive(true);
            nextBtn.SetActive(true);

        } else if(step1.gameObject.activeSelf)
        {
            step1.SetActive(false);
            mixedDryIngreModel.SetActive(true);
            mixedLiquid.SetActive(false);
            nextBtn.SetActive(true);            
        }

    }

    public void NextBtnFunctions()
    {
        if(currentStep == 1)
        {
            angKuKueh.SetActive(false);
            startBtn.SetActive(false);
            currentStep++;

        }else if(currentStep== 2)
        {
            step2.SetActive(true);
            mixedDryIngreModel.SetActive(false);
            currentStep++;
            stepsText.text = "Drag and drop the beetrot juice into the dry ingredients";
           

        } else if(currentStep== 3)
        {
            step3.SetActive(true);
            stepsText.text = "Drag up and down continuously to knead the dough!";
            mixingSlider.GetComponent<Slider>().value = 0;
            mixingSlider.gameObject.SetActive(true);
            mixedLiquid.SetActive(true);
            kneadCanva.SetActive(true);
            currentStep++;

        } else if(currentStep == 4)
        {
            stepsText.text = "Scan the blender section";
            step3.SetActive(false);
            tableBowl.SetActive(false);
            kneadCanva.SetActive(false);
            step4.SetActive(true);
            currentStep++;

        } else if(currentStep == 5)
        {
            stepsText.text = "Scan the mold section";
            step4.SetActive(false);
            currentStep++;
        }
    }
}
