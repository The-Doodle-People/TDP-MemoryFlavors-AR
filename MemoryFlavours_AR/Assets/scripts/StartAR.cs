/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used to start AR
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;


public class StartAR : MonoBehaviour
{
    /// <summary>
    /// An AnchorBehaviour array to hold GroundPlaneStages
    /// </summary>
    public AnchorBehaviour[] groundStages = new AnchorBehaviour[0];

    /// <summary>
    /// The PlaneFinder object in the scene
    /// </summary>
    public ContentPositioningBehaviour planeFinder;

    /// <summary>
    /// An AnchorBehaviour array to hold MidAirStages
    /// </summary>
    public AnchorBehaviour[] airStages = new AnchorBehaviour[0];

    /// <summary>
    /// The MidAirPositioner object in the scene
    /// </summary>
    public ContentPositioningBehaviour airPositioner;

    /// <summary>
    /// The index of the current active ground stage
    /// </summary>
    private int groundStageIndex = 0;

    /// <summary>
    /// The index of the current active air stage
    /// </summary>
    private int airStageIndex = 0;

    /// <summary>
    /// Tracks whether ground stages are active
    /// </summary>
    private bool trackingGroundStages;

    /// <summary>
    /// The Text component in the cycle stage button
    /// </summary>
    public TextMeshProUGUI cycleButtonText;

    /// <summary>
    /// The text component in the toggle stage type button
    /// </summary>
    public TextMeshProUGUI toggleButtonText;

    private void Start()
    {
        ChangeButtonTexts();
        EnableFinder();
        SetStage();
    }

    /// <summary>
    /// Flips the current stage type.
    /// </summary>
    public void ToggleStageType()
    {
        // First flip the bool value
        trackingGroundStages = !trackingGroundStages;

        // Change the button texts after changing the bool value.
        ChangeButtonTexts();

        // Enable the correct finder after changing the bool value.
        EnableFinder();
    }

    /// <summary>
    /// Sets the PlaneFinder and MidAirPositioner active/inactive
    /// </summary>
    private void EnableFinder()
    {
        // Set the planeFinder to be active IF trackingGroundStages is TRUE.
        planeFinder.gameObject.SetActive(trackingGroundStages);

        // Set the airPositioner to be active IF trackingGroundStages is FALSE.
        airPositioner.gameObject.SetActive(!trackingGroundStages);

        /* Long way to write the above two lines
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

    /// <summary>
    /// Changes the texts in the button to match the current tracking stage type
    /// </summary>
    private void ChangeButtonTexts()
    {
        // Changes the cycleButton text
        cycleButtonText.text = trackingGroundStages ? "CYCLE GROUND STAGES" : "CYCLE AIR STAGES";

        // Changes the toggleButton text
        toggleButtonText.text = trackingGroundStages ? "TOGGLE AIR STAGES" : "TOGGLE GROUND STAGES";

        // For ternary conditional operators, it follows this format:
        // *bool* ? *value if true* : *value if false*


        /* Long way to write the two lines above
        if(trackingGroundStages)
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

    /// <summary>
    /// Set the active stage of the ContentPositioningBehaviour
    /// </summary>
    private void SetStage()
    {
        // If trackingGroundStages is true, set the AnchorStage of the groundPlane
        if (trackingGroundStages)
        {
            planeFinder.AnchorStage = groundStages[groundStageIndex];
        }
        // If not, set the AnchorStage of the airPositioner
        else
        {
            airPositioner.AnchorStage = airStages[airStageIndex];
        }
    }

    /// <summary>
    /// Cycles through the active stages
    /// </summary>
    public void CycleStages()
    {
        // If trackingGroundStages is true, cycle through the ground stages
        if (trackingGroundStages)
        {
            // Increase the index
            ++groundStageIndex;
            // Check if the index exceeds the bounds of the array
            if (groundStageIndex >= groundStages.Length)
            {
                groundStageIndex = 0;
            }
        }
        // If not, cycle through the air stages
        else
        {
            // Increase the index
            ++airStageIndex;
            // Check if the index exceeds the bounds of the array
            if (airStageIndex >= airStages.Length)
            {
                airStageIndex = 0;
            }
        }

        // Update the active stage
        SetStage();
    }
}
