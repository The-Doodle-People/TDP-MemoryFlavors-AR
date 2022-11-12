using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PointSystem : MonoBehaviour
{
    /// <summary>
    /// Store button to switch to air
    /// </summary>
    public GameObject groundButton;

    /// <summary>
    /// Store button to switch to ground
    /// </summary>
    public GameObject airButton;

    /// <summary>
    /// Store ground stages
    /// </summary>
    public AnchorBehaviour[] groundStages = new AnchorBehaviour[0];

    /// <summary>
    /// Store mid air stages
    /// </summary>
    public AnchorBehaviour[] airStages = new AnchorBehaviour[0];

    /// <summary>
    /// Stores air positioner finder
    /// </summary>
    public ContentPositioningBehaviour airPositioner;

    /// <summary>
    /// Stores plane finder
    /// </summary>
    public ContentPositioningBehaviour planeFinder;

    /// <summary>
    /// Set active air positioner
    /// </summary>
    public GameObject activePositioner;

    /// <summary>
    /// Set active plane
    /// </summary>
    public GameObject activePlane;

    /// <summary>
    /// checks if active is called once
    /// </summary>
    private bool checkStage;

    /// <summary>
    /// checks if ground or air
    /// </summary>
    private bool checkPlane;

    /// <summary>
    /// Count and check questions
    /// </summary>
    private int stageNum = 1;

    /// <summary>
    /// Count number of points earned
    /// </summary>
    private int countPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        checkStage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkStage)
        {
            if (stageNum <= 4)
            {
                groundButton.SetActive(false);
                airButton.SetActive(false);
                activePlane.SetActive(false);
                activePositioner.SetActive(false);
                checkStage = false;
                Debug.Log(stageNum);


            }
        }
    }

    public void PrataIngredients()
    {
        if (stageNum <= 4)
        {

            countPoints += 1;
            stageNum += 1;
            Debug.Log(stageNum);
        }

        if (stageNum > 4)
        {
            activePlane.SetActive(true);
            groundButton.SetActive(true);
            checkPlane = true;
        }
        
    }

    public void KuehIngredients()
    {
        if (stageNum <= 4)
        {
            countPoints += 2;
            stageNum += 1;
            Debug.Log(stageNum);
        }

        if (stageNum > 4)
        {
            activePlane.SetActive(true);
            groundButton.SetActive(true);
            checkPlane = true;
        }
    }

    public void MooncakeIngredients()
    {
        if (stageNum <= 4)
        {
            countPoints += 3;
            stageNum += 1;
        }

        if (stageNum > 4)
        {
            activePlane.SetActive(true);
            groundButton.SetActive(true);
            checkPlane = true;
        }
    }

    public void ReplayGame()
    {
        stageNum = 1;
        countPoints = 0;
        checkStage = true;
    }

    public void ChangeStage()
    {
        if (checkPlane)
        {
            activePositioner.SetActive(false);
            activePlane.SetActive(true);
            if (countPoints >= 4 && countPoints <= 6)
            {
                planeFinder.AnchorStage = groundStages[0];

            }

            if (countPoints >= 7 && countPoints <= 9)
            {
                planeFinder.AnchorStage = groundStages[1];

            }

            if (countPoints >= 10 && countPoints <= 12)
            {
                planeFinder.AnchorStage = groundStages[2];

            }
        }

        else
        {
            activePositioner.SetActive(true);
            activePlane.SetActive(false);
            if (countPoints >= 4 && countPoints <= 6)
            {
                airPositioner.AnchorStage = airStages[0];

            }

            if (countPoints >= 7 && countPoints <= 9)
            {
                airPositioner.AnchorStage = airStages[1];

            }

            if (countPoints >= 10 && countPoints <= 12)
            {
                airPositioner.AnchorStage = airStages[2];

            }
        }
    }

    public void SwitchPlane()
    {
        checkPlane = !checkPlane;
        ChangeStage();
    }
}
