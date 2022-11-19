using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using AppAdvisory.SharingSystem;
using System.Threading.Tasks;
using Vuforia;
public class ARManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public bool isTracked = false;
    public GameObject mainCanvas;

    public GameObject descriptionBtn;
    public GameObject descriptionBackground;
    public GameObject descriptionTextArea;



    /// <summary>
    /// This dictionary holds the tracked status of AR objects
    /// </summary>
    Dictionary<GameObject, bool> objectsTracked = new Dictionary<GameObject, bool>();

    void Awake()
    {
        mainCanvas.SetActive(true);
        descriptionText.text = "";
        descriptionBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void ObjectTracked(GameObject objectToTrack)
    {
        
        if (objectToTrack != null)
        {
            isTracked = true;
            Debug.Log(isTracked);
            objectsTracked[objectToTrack] = true;
            //displayText.text = objectToTrack.name + " Track Status: true";
            Debug.Log(objectToTrack.name);
            if (objectToTrack.name == "DevilCurry")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "<insert Description here>";

            }
            else if (objectToTrack.name == "NasiPandang")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " <insert Description here>";

            }
            else if (objectToTrack.name == "HokkienMee")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "In Singapore, Hokkien mee (???) refers to a dish of" +
                    " egg noodles and rice noodles stir-fried with egg, slices of pork, prawns, and squid. The key" +
                    " to the dish is copious quantities of an aromatic broth made from prawns and pork bones, slowly" +
                    " simmered for many hours. Sambal chili and calamansi limes are served on the side for the diner" +
                    " to blend in, giving it an extra zing and tanginess. Traditionally, small cubes of fried lard are added," +
                    " and some stalls also serve the open on an opeh leaf (soft areca palm bark), which enhances the fragrance of the dish.";
                
            }
            else if (objectToTrack.name == "Thosai")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "Thosai (also spelled dosa) is a savory pancake " +
                    "served with a slew of spicy dipping sauces. Like so much of the Indian food popular at" +
                    " Singapore hawker centers, thosai are cheap, tasty and 100% vegetarian.";
                
            }
            else if (objectToTrack.name == "Hawker")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " is Hawker Tracked";
                
            }
            else if (objectToTrack.name == "PushCart")
            {
                StaticController.objectName = objectToTrack.name;
                descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " is Push Cart Tracked";
                
            }
            
        }
        
        else
        {
            descriptionBtn.gameObject.SetActive(false);
            descriptionBackground.gameObject.SetActive(false);
            descriptionTextArea.gameObject.SetActive(false);
            isTracked = false;
            return;
        }
    }

    public void ObjectLost(GameObject objectToTrack)
    {
        Debug.Log("Object lost");
        if (objectToTrack != null)
        {
        }
        else
        {
            isTracked = false;
            objectsTracked[objectToTrack] = false;
            descriptionText.text = "";
            descriptionBtn.gameObject.SetActive(false);
            descriptionBackground.gameObject.SetActive(false);
            descriptionTextArea.gameObject.SetActive(false);
            return;
        }
    }

    public async void SharingButton()
    {
        VSSHARE.DOTakeScreenShot();
        await Task.Delay(1000);
        VSSHARE.DOOpenScreenshotButton();
        await Task.Delay(10000);
        VSSHARE.DOHideScreenshotIcon();
    }
    
    public void ToggleDescription()
    {
        descriptionBackground.gameObject.SetActive(true);
        descriptionTextArea.gameObject.SetActive(true);
    }

    public void LoadQuizScene()
    {
        Debug.Log(isTracked);
        if (isTracked == true)
        {
            
            SceneManager.LoadScene(2);
            isTracked = false;
        }
        else
        {
            return;
        }
    }

    public void RefreshCurrentScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

}

