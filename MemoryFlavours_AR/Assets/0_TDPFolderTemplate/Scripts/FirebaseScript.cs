using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FirebaseScript : MonoBehaviour
{
    DatabaseReference mDatabaseRef;

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference; // get root reference location of the database
        Testing("01", "I Love Ang Ku Kueh");
    }

    private void Testing(string id, string memo)
    {
        MemoriesPadlet list = new MemoriesPadlet(memo);
        string json = JsonUtility.ToJson(list); // to convert to Json string

        mDatabaseRef.Child(id).Child(memo).SetRawJsonValueAsync(json); // send converted json data to Firebase DB
    }
}