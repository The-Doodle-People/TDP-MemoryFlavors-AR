using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public Button infoButton;
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
    public GameObject Information;
    
    // Start is called before the first frame update
    void Start()
    {
        // set the initial text of the button
        setButtonText(currentOption);

        // add an event listener to look out for button clicks
        infoButton.onClick.AddListener(myButtonClick);
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
                Information.SetActive(true);

                // change the current option string to option 2 for the next click
                currentOption = "option 2";

                // change the text on the button to be the next option
                setButtonText(currentOption);

                break;

            case "option 2":
                //set midair as true for option 2
                Information.SetActive(false);
                // change the current option string to option 3 for the next click
                currentOption = "option 1";
                setButtonText(currentOption);
                break;

        }

    }

    void setButtonText(string buttonText)
    {
        infoButton.transform.GetChild(0).GetComponent<Text>().text = buttonText;
    }

}

