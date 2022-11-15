using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public GameObject startBtn;
    public TMP_Text stepsText;
    public GameObject step2;
    public GameObject step1;
   
    public GameObject mixedDryIngreModel;
    public GameObject nextBtn;
    
    public GameObject mixedDryIngreWWell;
    public GameObject mixedLiquid;
    int currentStep = 2;

    //step 3
    public Slider kneadSlider;
    public Slider mixingSlider;
    public GameObject kneadCanva;
    public GameObject kneadedDough;
    public GameObject guideArrow;
    int targetKnead = 20;
    int currentKnead;
    bool targetTF;

    private void Update()
    {
        if(kneadCanva.activeSelf)
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
                    mixingSlider.value = 0f;
                    mixedLiquid.SetActive(false);
                    guideArrow.SetActive(false);
                    kneadedDough.SetActive(true);
                    nextBtn.SetActive(true);
                }
            };
        }
    }

    public void ValueChangeCheck() // a callback function to check if slider value has change, if yes means user is using the slider
    {
        mixedLiquid.transform.Rotate(0, 90 * Time.deltaTime, 0, Space.World);
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
            }

            if (objectToTrack.name == "table_model")
            {
                stepsText.text = "drag and drop flour, salt and sugar into yellow mixing bowl";
               
            }


        }
        else
        {
            return;
        }
    }
    
    
    

    public void HideStepOneOrTwo()
    {
        if (step2.gameObject.activeSelf)
        {
            step2.SetActive(false);
            mixedLiquid.gameObject.SetActive(true);
            nextBtn.SetActive(true);

        }
        else
        {
            step1.SetActive(false);
            mixedDryIngreModel.SetActive(true);
            nextBtn.SetActive(true);
           
        }
    }

    public void NextBtnFunctions()
    {
        if(currentStep== 2)
        {
            step2.SetActive(true);
            mixedDryIngreModel.SetActive(false);
            currentStep++;
            stepsText.text = "drag and drop the beetrot juice into the dry ingredients";
           

        } else if(currentStep== 3)
        {
            stepsText.text = "drag up and down continuously to knead the dough!";
            mixingSlider.GetComponent<Slider>().value = 0;
            mixingSlider.gameObject.SetActive(true);            
            kneadCanva.SetActive(true);
        }
    }
}
