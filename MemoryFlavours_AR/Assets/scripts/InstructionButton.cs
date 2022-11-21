/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for instructions button to toggle on and off
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

/// <summary>
/// class for instructions button to toggle on and off
/// </summary>
public class InstructionButton : MonoBehaviour
{
    /// <summary>
    /// link the instructions button
    /// </summary>
    public Button instructionButton;
    /// <summary>
    /// set current option as option 1
    /// </summary>
    private string currentOption = "option 1";
    /// <summary>
    /// set gameobject to link first groundplane
    /// </summary>
    //public GameObject groundPlane;
    /// <summary>
    /// set gameobject to link second groundplane
    /// </summary>
    public GameObject Instruction;
    // Start is called before the first frame update
    void Start()
    {
        // set the initial text of the button
        setButtonText(currentOption);

        // add an event listener to look out for button clicks
        instructionButton.onClick.AddListener(myButtonClick);
    }

    /// <summary>
    /// function for each time button click, changes stages of the string
    /// </summary>
    public void myButtonClick()
    {
        switch (currentOption)
        {
            case "option 1":
                //set groundplanel1 as true for option 1
                Instruction.SetActive(true);

                // change the current option string to option 2 for the next click
                currentOption = "option 2";

                // change the text on the button to be the next option
                setButtonText(currentOption);

                break;

            case "option 2":
                //set midair as true for option 2
                Instruction.SetActive(false);
                // change the current option string to option 3 for the next click
                currentOption = "option 1";
                setButtonText(currentOption);
                break;

        }

    }

    /// <summary>
    /// function to set button text 
    /// </summary>
    /// <param name="buttonText"></param>
    void setButtonText(string buttonText)
    {
        instructionButton.transform.GetChild(0).GetComponent<Text>().text = buttonText;
    }

}


