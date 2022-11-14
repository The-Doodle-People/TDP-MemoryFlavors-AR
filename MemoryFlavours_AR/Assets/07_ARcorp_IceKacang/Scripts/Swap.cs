using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{

    public GameObject[] stages;
    private int numModel;
    void Start()
    {
        numModel = 0;
    }
    public void ChangeStageModel()
    {
        if (numModel == 0)
        {
            stages[0].SetActive(false);
            stages[1].SetActive(true);
            stages[2].SetActive(false);
        }
        else if (numModel == 1)
        {
            stages[1].SetActive(false);
            stages[2].SetActive(true);
            stages[0].SetActive(false);
        }
        else if (numModel == 2)
        {
            stages[2].SetActive(false);
            stages[0].SetActive(true);
            stages[1].SetActive(false);
        }
        if (numModel != 2)
        {
            numModel += 1;
        }

        else if (numModel == 2)
        {
            numModel = 0;
        }
    }
}
