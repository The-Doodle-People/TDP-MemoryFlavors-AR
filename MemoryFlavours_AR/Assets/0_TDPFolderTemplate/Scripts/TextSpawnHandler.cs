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
   
    public void SpawnText(List <string> memoryList)
    {
      

        Debug.Log(firebase.memoriesNum);
        int randomNum = UnityEngine.Random.Range(0,firebase.memoriesNum);
        Debug.Log(memoryList[randomNum]);
       
        memoryTextUI = memoryList[randomNum];
        
     
    }

    public void ShowText()
    {
        memoryText.text = memoryTextUI;
    }
}