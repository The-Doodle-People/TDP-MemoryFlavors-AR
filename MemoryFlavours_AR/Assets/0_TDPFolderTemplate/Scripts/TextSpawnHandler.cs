using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TextSpawnHandler : MonoBehaviour
{
    public TextMeshProUGUI memoryText;
    public FirebaseScript firebase;
    public string memoryTextUI;
    public TextMeshProUGUI usernameText;
    public string usernameTextUI;
    public void SpawnText(List<MemoriesPadlet> memoryList)
    {

        //Debug.Log("textspawn");
        //Debug.Log(firebase.memoriesNum);
        int randomNum = UnityEngine.Random.Range(0, firebase.memoriesNum);
        Debug.Log(memoryList[randomNum]);

        memoryTextUI = memoryList[randomNum].memory;
        usernameTextUI = memoryList[randomNum].username;
        Debug.Log(memoryTextUI);
        memoryText.text = memoryTextUI;
        usernameText.text = "By: " + usernameTextUI;
    }

    
}
