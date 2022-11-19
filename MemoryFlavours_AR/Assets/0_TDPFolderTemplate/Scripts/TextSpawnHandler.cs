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

    private void Start()
    {
        
        memoryText.text ="hi jasmine";
    }
    public void SpawnText(List <string> memoryList)
    {
      

        Debug.Log(firebase.memoriesNum);
        int randomNum = UnityEngine.Random.Range(0,firebase.memoriesNum);
        Debug.Log(memoryList[randomNum]);
       
        memoryText.text = memoryList[randomNum];
     
    }
}
