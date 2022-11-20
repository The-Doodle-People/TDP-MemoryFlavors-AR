/*
* Author:Jasmine Giam
* Date: 20/11/2022
* Description: Script to update text as it spawns
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TextSpawnHandler : MonoBehaviour
{
    /// <summary>
    /// text ui that will shoe users response
    /// </summary>
    public TextMeshProUGUI memoryText;

    /// <summary>
    /// refering to firebase script
    /// </summary>
    public FirebaseScript firebase;

    /// <summary>
    /// string to store new entry
    /// </summary>
    public string memoryTextUI;

    /// <summary>
    /// ui that shows user name
    /// </summary>
    public TextMeshProUGUI usernameText;
    /// <summary>
    /// string that stores new user name
    /// </summary>
    public string usernameTextUI;

    /// <summary>
    /// updating text as player spawns note
    /// </summary>
    /// <param name="memoryList"></param>
    public void SpawnText(List<MemoriesPadlet> memoryList)
    {

        //Debug.Log("textspawn");
        //Debug.Log(firebase.memoriesNum);
        int randomNum = UnityEngine.Random.Range(0, firebase.memoriesNum); //getting a random number to call a random entry in the list
        Debug.Log(memoryList[randomNum]);

        memoryTextUI = memoryList[randomNum].memory;
        usernameTextUI = memoryList[randomNum].username;
        Debug.Log(memoryTextUI);
        memoryText.text = memoryTextUI;
        usernameText.text = "By: " + usernameTextUI;
    }

    
}
