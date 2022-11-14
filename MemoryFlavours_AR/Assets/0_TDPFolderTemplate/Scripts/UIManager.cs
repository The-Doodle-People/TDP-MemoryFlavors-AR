using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public GameObject startBtn;
    public TMP_Text stepsText;
    public GameObject step2;
    public GameObject step1;
    public GameObject flour;
    
    public GameObject mixedDryIngreModel;
    public GameObject nextBtn;
    
    public GameObject mixedDryIngreWWell;
    public GameObject mixedLiquid;
    int currentStep = 2;
   
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
                flour.AddComponent<Lean.Touch.LeanDragTranslate>();
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
            stepsText.text = "knead the dough";
          
        }
    }


}
