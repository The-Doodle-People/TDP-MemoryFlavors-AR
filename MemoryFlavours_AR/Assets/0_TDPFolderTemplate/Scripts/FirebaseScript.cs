using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using System;

public class FirebaseScript : MonoBehaviour
{
    DatabaseReference mDatabaseRef;

    public TextMeshProUGUI answerInput;

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("MemoriesPadlet"); // get root reference location of the database
    }

    public void getUserInput()
    {
        if(!String.IsNullOrEmpty(answerInput.text))
        {
            string newInput = answerInput.text.Trim();
            Memory(newInput);
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



}