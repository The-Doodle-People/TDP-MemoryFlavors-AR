using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI quizQuestion;

    public GameObject audioBtn;
    public GameObject quizBtn;
    public GameObject shareBtn;
    public GameObject clearBtn;
    public GameObject helpBtn;
    public GameObject whiteNoiseBtn;

    public GameObject mainCanvas;
    public GameObject quizCanvas;

    public bool checkQuiz = false;

    /// <summary>
    /// This dictionary holds the tracked status of AR objects
    /// </summary>
    Dictionary<GameObject, bool> objectsTracked = new Dictionary<GameObject, bool>();

    void Awake()
    {
        mainCanvas.SetActive(true);
    }

    public void ObjectTracked(GameObject objectToTrack)
    {
        
        if (objectToTrack != null)
        {
            objectsTracked[objectToTrack] = true;
            //displayText.text = objectToTrack.name + " Track Status: true";
            Debug.Log(objectToTrack.name);
            if (objectToTrack.name == "DevilCurry")
            {
                displayText.text = objectToTrack.name + " is Devil Curry Tracked";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "NasiPandang")
            {
                displayText.text = objectToTrack.name + " is Nasi Pandang Tracked";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "HokkienMee")
            {
                displayText.text = objectToTrack.name + " is Hokkien Mee Tracked";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "Tosai")
            {
                displayText.text = objectToTrack.name + " is Tosai Tracked";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "Hawker")
            {
                displayText.text = objectToTrack.name + " is Hawker Tracked";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "PushCart")
            {
                displayText.text = objectToTrack.name + " is Push Cart Tracked";
                OpenQuiz(objectToTrack.name);
            }
        }
        
        else
        {
            return;
        }
    }

    public void ObjectLost(GameObject objectToTrack)
    {
        if (objectToTrack != null)
        {
            objectsTracked[objectToTrack] = false;
            //displayText.text = objectToTrack.name + "Track Status: false";
        }
        else
        {
            return;
        }
    }

    public void CheckQuiz()
    {
        checkQuiz = true;
        Debug.Log(checkQuiz);
    }

    public void OpenQuiz(string scannedItem)
    {
        if (scannedItem != null && checkQuiz == true)
        {
            quizCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            //displayText.text = objectToTrack.name + " Track Status: true";
            Debug.Log("Open Quiz Scanned item " + scannedItem);
            if (scannedItem == "DevilCurry")
            {
                quizQuestion.text = scannedItem + " is Devil Curry Quiz";
            }
            else if (scannedItem == "NasiPandang")
            {
                quizQuestion.text = scannedItem + " is Nasi Pandang Quiz";
            }
            else if (scannedItem == "HokkienMee")
            {
                quizQuestion.text = scannedItem + " is Hokkien Mee Quiz";
            }
            else if (scannedItem == "Tosai")
            {
                quizQuestion.text = scannedItem + " is Tosai Quiz";
            }
            else if (scannedItem == "Hawker")
            {
                quizQuestion.text = scannedItem + " is Hawker Quiz";
            }
            else if (scannedItem == "PushCart")
            {
                quizQuestion.text = scannedItem + " is Push Cart Quiz";
            }
        }

        else
        {
            return;
        }
    }

}

