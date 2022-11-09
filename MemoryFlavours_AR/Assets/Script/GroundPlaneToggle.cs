using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class GroundPlaneToggle : MonoBehaviour
{
    public AnchorBehaviour[] groundStages = new AnchorBehaviour[0];
    public ContentPositioningBehaviour planeFinder;

    public AnchorBehaviour[] airStages = new AnchorBehaviour[0];
    public ContentPositioningBehaviour airPositioner;

    private int groundStageIndex = 0;
    private int airStageIndex = 0;

    private bool trackingGroundStages;

    public TextMeshProUGUI cycleButtonText;
    public TextMeshProUGUI toggleButtonText;

    private void Start()
    {
        ChangeButtonTexts();
        EnableFinder();
        SetStage();
       
    }

    public void ToggleStageType()
    {
        trackingGroundStages = !trackingGroundStages;

        ChangeButtonTexts();
        EnableFinder();
    }

    private void EnableFinder()
    {
        planeFinder.gameObject.SetActive(trackingGroundStages);
        airPositioner.gameObject.SetActive(!trackingGroundStages);

        /*
        if (trackingGroundStages)
        {
            planeFinder.gameObject.SetActive(true);
            airPositioner.gameObject.SetActive(false);
        }
        else
        {
            planeFinder.gameObject.SetActive(false);
            airPositioner.gameObject.SetActive(true);
        }
        */
    }

    private void ChangeButtonTexts()
    {
        cycleButtonText.text = trackingGroundStages ? "CYCLE GROUND STAGES" : "CYCLE AIR STAGES";
        toggleButtonText.text = trackingGroundStages ? "TOGGLE AIR STAGES" : "TOGGLE GROUND STAGES";

        /*
        if (trackingGroundStages)
        {
            cycleButtonText.text = "CYCLE GROUND STAGES";
            toggleButtonText.text = "TOGGLE AIR STAGES";
        }
        else
        {
            cycleButtonText.text = "CYCLE AIR STAGES";
            toggleButtonText.text = "TOGGLE GROUND STAGES";
        }
        */
    }

    private void SetStage()
    {
        if (trackingGroundStages)
        {
            planeFinder.AnchorStage = groundStages[groundStageIndex];
        }
        else
        {
            airPositioner.AnchorStage = airStages[airStageIndex];
        }
    }

    public void CycleStages()
    {
        if (trackingGroundStages)
        {
            ++groundStageIndex;
            if (groundStageIndex >= groundStages.Length)
            {
                groundStageIndex = 0;
            }
        }
        else
        {
            ++airStageIndex;
            if (airStageIndex >= airStages.Length)
            {
                airStageIndex = 0;
            }
        }
        

        SetStage();
    }
}
