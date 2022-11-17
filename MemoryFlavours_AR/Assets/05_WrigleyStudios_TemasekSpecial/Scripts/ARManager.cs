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
                displayText.text = objectToTrack.name + "<insert Description here>";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "NasiPandang")
            {
                displayText.text = objectToTrack.name + " <insert Description here>";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "HokkienMee")
            {
                displayText.text = objectToTrack.name + "In Singapore, Hokkien mee (???) refers to a dish of" +
                    " egg noodles and rice noodles stir-fried with egg, slices of pork, prawns, and squid. The key" +
                    " to the dish is copious quantities of an aromatic broth made from prawns and pork bones, slowly" +
                    " simmered for many hours. Sambal chili and calamansi limes are served on the side for the diner" +
                    " to blend in, giving it an extra zing and tanginess. Traditionally, small cubes of fried lard are added," +
                    " and some stalls also serve the open on an opeh leaf (soft areca palm bark), which enhances the fragrance of the dish.";
                OpenQuiz(objectToTrack.name);
            }
            else if (objectToTrack.name == "Thosai")
            {
                displayText.text = objectToTrack.name + "Thosai (also spelled dosa) is a savory pancake " +
                    "served with a slew of spicy dipping sauces. Like so much of the Indian food popular at" +
                    " Singapore hawker centers, thosai are cheap, tasty and 100% vegetarian.";
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
        
    }

}

