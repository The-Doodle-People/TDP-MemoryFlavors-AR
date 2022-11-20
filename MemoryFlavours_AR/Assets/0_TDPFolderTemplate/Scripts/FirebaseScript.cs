/*
* Author: Wong Qing Wei, Jasmine Giam
* Date: 20/11/2022
* Description: Firebase script to store inputs to database
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using Firebase.Extensions;

public class FirebaseScript : MonoBehaviour
{
    /// <summary>
    /// Stores the database references
    /// </summary>
    DatabaseReference mDatabaseRef;

    /// <summary>
    /// Stores all inputfields
    /// </summary>
    public TMP_InputField answerInput;
    public TMP_InputField usernameInput;

    /// <summary>
    /// Stores the text spawn handler
    /// </summary>
    public TextSpawnHandler textSpawnHandler;

    //stores the game objects
    /// <summary>
    /// game object that stores ui for player input
    /// </summary>
    public GameObject sharingPadlet;

    /// <summary>
    /// text that instructs the player what to do when wanting viewing other peoples responses
    /// </summary>
    public GameObject sharingMemoryUI;
    /// <summary>
    /// error msg when user inputs an invalid input when answering the qn
    /// </summary>
    public GameObject errorMsg;

    /// <summary>
    /// sprite that is the background of player response
    /// </summary>
    public GameObject titleSprite;

    /// <summary>
    /// mid air positioner & mid air stage/ portion to view the memories
    /// </summary>
    public GameObject viewMemory;

    /// <summary>
    /// A int object to count how many entries are in the databse
    /// </summary>
    public int memoriesNum;

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("MemoriesPadlet"); // get root reference location of the database
    }

    /// <summary>
    /// getting and checking user input
    /// </summary>
    public void getUserInput()
    {
        if((answerInput.text != null && answerInput.text !="" )&& (usernameInput != null && usernameInput.text != ""))
        {
            string newInput = answerInput.text.Trim();
            string newUsername = usernameInput.text.Trim();
            Debug.Log(newInput.Length);
            if (newInput.Length <= 255 && newUsername.Length <=16) //checking if the player input is more than a certain num of char
            {
                GameManager.instance.enterMemories = true; // letting code know player is viewing response
                Memory(newInput, newUsername);
              
                sharingPadlet.SetActive(false); 
                titleSprite.SetActive(false);
                sharingMemoryUI.SetActive(true);
                errorMsg.SetActive(false);
                viewMemory.SetActive(true);
                GetMemory();

            } else
            {
                errorMsg.SetActive(true);
            }
          
           
        }
        else
        {
            Debug.Log("please input smt");
            
        }
    }

    //Get the inputfields data to set a random ID node to it and push to firebase
    private void Memory(string memory, string username)
    {
        MemoriesPadlet mp = new MemoriesPadlet(memory, username);
        var playerPath = mDatabaseRef.Push(); //generate random key each time
        mDatabaseRef.Push().SetRawJsonValueAsync(JsonUtility.ToJson(mp)); // push to firebase
    }

    /// <summary>
    /// getting the db entries
    /// </summary>
    public void GetMemory()
    {
       
        List<MemoriesPadlet> memoryList = new List<MemoriesPadlet>(); //list to store entries

        mDatabaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("sorry there was an error getting entries, : Error:" + task.Exception);

            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                if (ds.Exists)
                {
                    foreach (DataSnapshot d in ds.Children)
                    {

                        //create temp objects based on the results
                        MemoriesPadlet mP = JsonUtility.FromJson<MemoriesPadlet>(d.GetRawJsonValue());
                       

                        memoryList.Add(mP);
                        
                    }

                    memoriesNum = memoryList.Count;
                    textSpawnHandler.SpawnText(memoryList);

                    //foreach (string memory in memoryList)
                    //{

                    //    Debug.Log("Memory: " + memory);
                        
                    //}
                    

                }
                else
                {
                    Debug.Log("sorry data does not exist");
                }
            }

        });
        //return leaderboardList;
    }

}