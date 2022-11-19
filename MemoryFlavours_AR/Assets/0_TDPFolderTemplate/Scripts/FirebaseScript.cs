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
    DatabaseReference mDatabaseRef;

    public TMP_InputField answerInput;
    public TextMeshProUGUI reminder;
    public int memoriesNum;
    public TextSpawnHandler textSpawnHandler;

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("MemoriesPadlet"); // get root reference location of the database
    }

    public void getUserInput()
    {
        if(answerInput.text != null && answerInput.text !="")
        {
            string newInput = answerInput.text.Trim();
            Memory(newInput);
            Debug.Log("eter");
        }
        else
        {
            Debug.Log("please input smt");
            
        }
    }

    private void Memory(string memory)
    {
        MemoriesPadlet mp = new MemoriesPadlet(memory);
        var playerPath = mDatabaseRef.Push(); //generate random key each time
        mDatabaseRef.Push().SetRawJsonValueAsync(JsonUtility.ToJson(mp)); // push to firebase
    }

    public void GetMemory()
    {
        List<string> memoryList = new List<string>();

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
                       

                        memoryList.Add(mP.memory);
                        
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