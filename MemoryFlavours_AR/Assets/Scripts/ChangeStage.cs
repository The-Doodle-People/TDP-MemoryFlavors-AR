using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    // To drag and drop the different stage objects
    public GameObject[] stages;

    // To indicate the order of objects
    private int numObject;
    void Start()
    {
        numObject = 0;
    }

    // To change the stages of objects
    public void ChangeStageObjects()
    {
        if (numObject == 0)
        {
            stages[0].SetActive(false);
            stages[1].SetActive(true);
            stages[2].SetActive(false);
            stages[3].SetActive(false);
            stages[4].SetActive(false);
        }
        else if (numObject == 1)
        {
            stages[1].SetActive(false);
            stages[2].SetActive(true);
            stages[3].SetActive(false);
            stages[4].SetActive(false);
            stages[0].SetActive(false);
        }
        else if (numObject == 2)
        {
            stages[2].SetActive(false);
            stages[0].SetActive(true);
            stages[1].SetActive(false);
            stages[3].SetActive(false);
            stages[4].SetActive(false);
        }
        else if (numObject == 3)
        {
            stages[0].SetActive(false);
            stages[3].SetActive(true);
            stages[4].SetActive(false);
            stages[1].SetActive(false);
            stages[2].SetActive(false);
        }
        else if (numObject == 4)
        {
            stages[3].SetActive(false);
            stages[4].SetActive(true);
            stages[0].SetActive(false);
            stages[1].SetActive(false);
            stages[2].SetActive(false);
        }
        if (numObject != 5)
        {
            numObject += 1;
        }

        else if (numObject == 5)
        {
            numObject = 0;
        }
    }
}
