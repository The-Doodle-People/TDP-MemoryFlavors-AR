using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using AppAdvisory.SharingSystem;
using System.Threading.Tasks;
using Vuforia;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public bool isTracked = false;
    public GameObject mainCanvas;

    public GameObject descriptionBtn;
    public GameObject descriptionBackground;
    public GameObject descriptionTextArea;

    /// <summary>
    /// Sound: SFX - Ambience sound for the app
    /// </summary>
    public AudioSource hawkerAmbienceSound;
    public AudioSource thosaiSound;
    public AudioSource hokkienMeeSound;
    public Button toggleWhiteNoise;
    public bool playAmbience = false; // Toggle off and on button for the ambience sound when object is detected

    /// <summary>
    /// Sound: Narration - It narrates unique conversation based on what image that been scanned
    /// </summary>
    string nameOfObject;
    public AudioSource devilCurryNar;
    public AudioSource hawkerCenterNar;
    public AudioSource hokkienMeeNar;
    public AudioSource nasiPadangNar;
    public AudioSource pushCartNar;
    public AudioSource thosaiNar;
    public Button toggleNarrate;
    public bool playNarrate = false;


    /// <summary>
    /// This dictionary holds the tracked status of AR objects
    /// </summary>
    Dictionary<GameObject, bool> objectsTracked = new Dictionary<GameObject, bool>();

    void Awake()
    {
        playAmbience = false;
        playNarrate = false;
        toggleWhiteNoise.interactable = false;
        toggleNarrate.interactable = false;
        mainCanvas.SetActive(true);
        descriptionText.text = "";
        descriptionBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Play audio update: " + playAmbience);
        
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
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "<insert Description here>";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "NasiPandang")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " <insert Description here>";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "HokkienMee")
            {
                StaticController.objectName = objectToTrack.name;
               //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "In Singapore, Hokkien mee (???) refers to a dish of" +
                    " egg noodles and rice noodles stir-fried with egg, slices of pork, prawns, and squid. The key" +
                    " to the dish is copious quantities of an aromatic broth made from prawns and pork bones, slowly" +
                    " simmered for many hours. Sambal chili and calamansi limes are served on the side for the diner" +
                    " to blend in, giving it an extra zing and tanginess. Traditionally, small cubes of fried lard are added," +
                    " and some stalls also serve the open on an opeh leaf (soft areca palm bark), which enhances the fragrance of the dish.";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "Thosai")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + "Thosai (also spelled dosa) is a savory pancake " +
                    "served with a slew of spicy dipping sauces. Like so much of the Indian food popular at" +
                    " Singapore hawker centers, thosai are cheap, tasty and 100% vegetarian.";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "Hawker")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " is Hawker Tracked";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "PushCart")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = objectToTrack.name + " is Push Cart Tracked";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
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

    public void ToPlayAmbienceSound(string trackName)
    {
        if (trackName != null)
        {
            if (trackName == "DevilCurry")
            {
                toggleWhiteNoise.interactable = false;
            }
            else if (trackName == "NasiPandang")
            {
                toggleWhiteNoise.interactable = false;
            }
            else if (trackName == "HokkienMee")
            {
                toggleWhiteNoise.interactable = true;
                //hokkienMeeSound.Play();
            }
            else if (trackName == "Thosai")
            {
                toggleWhiteNoise.interactable = true;
               //thosaiSound.Play();
            }
            else if (trackName == "Hawker")
            {
                toggleWhiteNoise.interactable = true;
                //hawkerAmbienceSound.Play();
            }
            else if (trackName == "PushCart")
            {
                toggleWhiteNoise.interactable = false;
            }
        }
    }

    public void ToggleAmbience()
    {
        nameOfObject = StaticController.objectName;
        Debug.Log(" Play Audio: " + playAmbience);
        if(playAmbience == false)
        {
            if (nameOfObject == "HokkienMee")
            {
                toggleWhiteNoise.interactable = true;
                hokkienMeeSound.Play();
            }
            else if (nameOfObject == "Thosai")
            {
                toggleWhiteNoise.interactable = true;
                thosaiSound.Play();
            }
            else if (nameOfObject == "Hawker")
            {
                toggleWhiteNoise.interactable = true;
                hawkerAmbienceSound.Play();
            }

            playAmbience = true;
            //ambienceSoundGrp.SetActive(true);
        }
        else if (playAmbience == true)
        {
            if (nameOfObject == "HokkienMee")
            {
                toggleWhiteNoise.interactable = true;
                hokkienMeeSound.Stop();
            }
            else if (nameOfObject == "Thosai")
            {
                toggleWhiteNoise.interactable = true;
                thosaiSound.Stop();
            }
            else if (nameOfObject == "Hawker")
            {
                toggleWhiteNoise.interactable = true;
                hawkerAmbienceSound.Stop();
            }
            playAmbience = false;
            //ambienceSoundGrp.SetActive(false);
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
   

    public void PlayNarration()
    {
        nameOfObject = StaticController.objectName;
        Debug.Log("narration: " + nameOfObject);
        if (playNarrate == false)
        {
            if (nameOfObject == "DevilCurry")
            {
                devilCurryNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);
            }
            else if (nameOfObject == "NasiPandang")
            {
                nasiPadangNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);
            }
            else if (nameOfObject == "HokkienMee")
            {
                hokkienMeeNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);

            }
            else if (nameOfObject == "Thosai")
            {
                thosaiNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);
            }
            else if (nameOfObject == "Hawker")
            {
                hawkerCenterNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);

            }
            else if (nameOfObject == "PushCart")
            {
                pushCartNar.Play();
                descriptionBackground.gameObject.SetActive(true);
                descriptionTextArea.gameObject.SetActive(true);
            }
            playNarrate = true;
        }
        else if (playNarrate == true)
        {
            if (nameOfObject == "DevilCurry")
            {
                devilCurryNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);
            }
            else if (nameOfObject == "NasiPandang")
            {
                nasiPadangNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);
            }
            else if (nameOfObject == "HokkienMee")
            {
                hokkienMeeNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);

            }
            else if (nameOfObject == "Thosai")
            {
                thosaiNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);
            }
            else if (nameOfObject == "Hawker")
            {
                hawkerCenterNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);

            }
            else if (nameOfObject == "PushCart")
            {
                pushCartNar.Stop();
                descriptionBackground.gameObject.SetActive(false);
                descriptionTextArea.gameObject.SetActive(false);
            }
            playNarrate = false;
        }
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

