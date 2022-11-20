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

    /// <summary>
    /// This code allows us to to be able to scan our image plane 
    /// For this project our image plane is the postcard we designed
    /// </summary>
    /// <param name="objectToTrack"></param>

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
                descriptionText.text = objectToTrack.name + "Devil curry also known as curry Debal in Kristang is a very spicy curry flavoured with candlenuts, galangal, mustard seed and vinegar from the Eurasian Kristang (Cristão) culinary tradition in Malacca, " +
                    "Malaysia, Indonesia and the Indo-Dutch diaspora (where it is known as Ayam ore Daging Setan). It was historically served one or two days after Christmas and on other special occasions. " +
                    "Kristang cuisine blends the cuisines of Southeast Asia with a western-style cuisine inherited from Portuguese colonial rulers";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "NasiPandang")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = " Nasi Padang is literally translated as ‘rice’ from Padang city in Indonesia. It’s a style of eating where the diner is presented with a number of dishes to choose from. " +
                    "In Singapore, the selection often showcases the diversity of dishes from all over Indonesia and Malaysia.But don’t be fooled for, despite the sheer number of dishes on display, ordering is a straightforward affair." +
                    "Choose between having it two ways: as mixed rice(nasi campur) where all the different dishes are served on your own plate or served family - style(hidang) where each selection is placed on individual plates";
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
                descriptionText.text = " Hawker centers sprang up in urban areas following the rapid urbanization in the 1950s and 1960s. In many cases, they were built partly to address the problem of unhygienic food preparation by unlicensed street hawkers. " +
                    "More recently, they have become less ubiquitous due to growing affluence in the urban populations of Malaysia and Singapore. Particularly in Singapore, they are increasingly being replaced by food courts, which are indoor, air-conditioned versions of hawker centers located in shopping malls and other commercial venues.";
                toggleNarrate.interactable = true;
                ToPlayAmbienceSound(objectToTrack.name);
            }
            else if (objectToTrack.name == "PushCart")
            {
                StaticController.objectName = objectToTrack.name;
                //descriptionBtn.gameObject.SetActive(true);
                descriptionText.text = " Traveling hawkers or itinerant hawkers were a common sight in Singapore from the 19th century to the mid-20th century. " +
                    "They were frequently found along busy streets and intersections, peddling food, drinks, vegetables, poultry, and sundries. Street hawking was a popular occupation for many new immigrants to Singapore as it gave the unemployed and the unskilled a way to make a living with little cost. " +
                    "Though there were many issues associated with street hawking, such as traffic obstruction and hygiene, street hawkers played an important role in providing the working classes with easy access to affordable meals.";
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
    /// <summary>
    /// This function disables the toggle button for ambient sound for AR objects that doesnt include the ambient sound feature.
    /// </summary>
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
    /// <summary>
    /// This function allows users to toggle on and off our AR feature called 'ambient sound' 
    /// where we record surroudning sound at hawker centers with our mobile devices
    /// </summary>
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

    /// <summary>
    /// Onclick it takes a screenshot of the AR scene and enable social media sharing 
    /// (Due to android version issue we are unable to get social sharing to work but screenshot features is working) 
    /// </summary>
    public async void SharingButton()
    {
        VSSHARE.DOTakeScreenShot();
        await Task.Delay(1000);
        VSSHARE.DOOpenScreenshotButton();
        await Task.Delay(10000);
        VSSHARE.DOHideScreenshotIcon();
    }
   
    /// <summary>
    /// This part of the code handles our narration of individual AR objects using a text to speech audio clip
    /// </summary>
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

    /// <summary>
    /// Loads the pop quiz
    /// </summary>
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
    /// <summary>
    /// This function restarts the whole screen clearing all screeshots and bugs seen on screen.
    /// </summary>
    public void RefreshCurrentScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Navigaton back to home scene
    /// </summary>
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

}

