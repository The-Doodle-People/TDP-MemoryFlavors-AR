using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public GameObject step5;
    public GameObject step6;
    public GameObject tableBowl;
    public GameObject stepsUI;

    public GameObject mixedDryIngreModel;
    public GameObject nextBtn;

    public GameObject mixedDryIngreWWell;
    public GameObject mixedLiquid;
    public int currentStep = 1;

    //step 3
    public Slider kneadSlider;
    public Scrollbar mixingSlider;
    public GameObject kneadCanva;
    public GameObject kneadedDough;
    public GameObject guideArrow;
    int targetKnead = 10;
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

    public GameObject[] stepsTickUI;
    int q = 0;

    public ParticleSystem kneadParticles;
    public ParticleSystem blendingEffect;
    public ParticleSystem peanutBlendedEffect;
    public ParticleSystem aftKneadingEffect;

    public AudioSource kneadSound;
    public AudioSource blendingSound;
    public AudioSource voiceOver;

    public AudioSource bgMusic;

    bool audioIsPlaying = false;

    public GameObject angKuKuehFinal;


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
                if (targetTF)
                {
                    currentKnead += 2;
                    mixingSlider.size += 0.2f;
                    targetTF = false;
                    kneadSlider.value = 0f;
                }
            }

            if (currentKnead == targetKnead) // once kneading done
            {
                if (mixingSlider.size == 1)
                {
                    mixingSlider.gameObject.SetActive(false);
                    aftKneadingEffect.Play();
                    mixingSlider.size = 0f;
                    mixedLiquid.SetActive(false);
                    guideArrow.SetActive(false);
                    kneadedDough.SetActive(true);
                    stepsText.text = "Good Job! Dough looking good";
                    TicksUI();
                    nextBtn.SetActive(true);
                }
            };
        }

        if (step4.activeSelf)
        {
            triggerCheck = blenderCap.GetComponent<TriggerCheck>();

            if (triggerCheck.readyToBlend == true)
            {
                blendedCapUi.SetActive(false);

                if (mixingSlider.size < 1f)
                {
                    if (!blendingEffect.isPlaying)
                    {
                        blendingEffect.Play();
                    }
                    if (!blendingSound.isPlaying)
                    {
                        blendingSound.Play();
                    }
                    mixingSlider.size += 0.2f * Time.deltaTime;
                }
                else if (mixingSlider.size == 1f)
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
                    mixingSlider.size = 0f;
                    finishBlend = true;
                    triggerCheck.readyToBlend = false;
                }
            }

            if (aftBlendedPeanuts.activeSelf)
            {
                blendedPeanuts.SetActive(false);
                if (!peanutBlendedEffect.isPlaying)
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

    public IEnumerator WaitForSound(AudioSource Sound)
    {
        yield return new WaitUntil(() => voiceOver.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        Destroy(voiceOver);
        startBtn.SetActive(true); //Do something
    }

    public void ObjectTracked(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            Debug.Log(currentStep);
            trackedObjectStatus[objectToTrack] = true;
            Debug.Log(objectToTrack.name + "Tracked");
            if (objectToTrack.name == "AngKuKueh_Model" && currentStep == 1 && audioIsPlaying == false)
            {
                playVoiceOver();

            }

            if (objectToTrack.name == "table_model" && currentStep == 2)
            {
                stepsText.text = "Drag and drop flour, salt and sugar into the yellow bowl";                
            }

            if (objectToTrack.name == "table_model" && currentStep == 5)
            {
                stepsText.text = "Drag and drop peanut fillings to blender";
            }

            if (objectToTrack.name == "table_model" && currentStep == 6)
            {
                stepsText.text = "Drag dough and peanut fillings to the Ang Ku Kueh mold";
            }

            if (objectToTrack.name == "table_model" && currentStep == 7)
            {
                stepsText.text = "Drag and drop Ang Ku Kueh to steamer before placing the lid on";
            }

            if (objectToTrack.name == "table_model" && currentStep == 8)
            {
                stepsText.text = "Congrats";
            }
        }
        else
        {
            return;
        }
    }
    public void playVoiceOver()
    {
        voiceOver.Play();
        audioIsPlaying= true;
        StartCoroutine(WaitForSound(voiceOver));
    }


    public void HideStepOneOrTwo()
    {
        if (step2.gameObject.activeSelf)
        {
            step2.SetActive(false);
            mixedLiquid.SetActive(true);
            aftKneadingEffect.Play();
            stepsText.text = "your dough is getting there!";
            TicksUI();
            nextBtn.SetActive(true);
        }
        else if (step1.gameObject.activeSelf)
        {
            step1.SetActive(false);
            mixedDryIngreModel.SetActive(true);
            aftKneadingEffect.Play();
            mixedLiquid.SetActive(false);
            stepsText.text = "So far so good!";
            TicksUI();
            nextBtn.SetActive(true);
        }

    }

    public void TicksUI() //tick UI appear after complete steps
    {
        stepsTickUI[q].SetActive(true);
        q++;
    }

    public void NextBtnFunctions()
    {
        if (currentStep == 1)
        {
            angKuKueh.SetActive(false);
            startBtn.SetActive(false);
            stepsUI.SetActive(true);
            currentStep++;
            bgMusic.Play();

        }
        else if (currentStep == 2)
        {
            step2.SetActive(true);
            mixedDryIngreModel.SetActive(false);
            currentStep++;
            stepsText.text = "Drag and drop the beetroot juice into the dry ingredients";

        }
        else if (currentStep == 3)
        {
            step3.SetActive(true);
            stepsText.text = "Drag from bottom to top continuously on the arrow to knead the dough!";
            mixingSlider.GetComponent<Scrollbar>().size = 0;
            mixingSlider.gameObject.SetActive(true);
            mixedLiquid.SetActive(true);
            kneadCanva.SetActive(true);
            currentStep++;

        }
        else if (currentStep == 4)
        {
            stepsText.text = "Scan the Blender on the postcard";
            step3.SetActive(false);
            tableBowl.SetActive(false);
            kneadCanva.SetActive(false);
            step4.SetActive(true);
            currentStep++;

        }
        else if (currentStep == 5)
        {
            stepsText.text = "Scan the Ang Ku Kueh mold on the postcard ";
            step4.SetActive(false);
            step5.SetActive(true);
            currentStep++;
        }
        else if (currentStep == 6)
        {
            stepsText.text = "Scan the Steamer on the postcard";
            step5.SetActive(false);
            step6.SetActive(true);
            currentStep++;
        }
        else if (currentStep == 7)
        {
            step6.SetActive(false);
            angKuKuehFinal.SetActive(true);
        }
    }
}
