using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlsAnim : MonoBehaviour
{
    /// <summary>
    /// Stores Chinatown model
    /// </summary>
    public GameObject chinaTown;

    /// <summary>
    /// Stores mooncake model
    /// </summary>
    public GameObject mooncake;

    /// <summary>
    /// Stores kueh model
    /// </summary>
    public GameObject kueh;

    /// <summary>
    /// Stores little india model
    /// </summary>
    public GameObject littleIndia;

    /// <summary>
    /// Stores prata model
    /// </summary>
    public GameObject prata;

    /// <summary>
    /// Stores mosque model
    /// </summary>
    public GameObject mosque;

    /// <summary>
    /// Stores start animation button to set active
    /// </summary>
    public GameObject startButton;

    /// <summary>
    /// Stores stop animation button to set active
    /// </summary>
    public GameObject stopButton;

    /// <summary>
    /// Stores text image of chinatown
    /// </summary>
    public GameObject imageChinatown;

    /// <summary>
    /// Stores text image of mooncake
    /// </summary>
    public GameObject imageMooncake;

    /// <summary>
    /// Stores text image of little india
    /// </summary>
    public GameObject imageLittleIndia;

    /// <summary>
    /// Stores text image of prata
    /// </summary>
    public GameObject imagePrata;

    /// <summary>
    /// Stores text image of mosque
    /// </summary>
    public GameObject imageMosque;

    /// <summary>
    /// Stores text image of kueh tutu
    /// </summary>
    public GameObject imageKueh;

    public AudioSource audioSource;

    public AudioClip[] changeClip = new AudioClip[0];

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = changeClip[3];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // called when target one image appears
    public void ImageAppearsTargetOne()
    {
        startButton.SetActive(true);
        stopButton.SetActive(true);
        imageChinatown.SetActive(true);
        chinaTown.SetActive(true);
        startButton.SetActive(true);
        stopButton.SetActive(false);
        audioSource.clip = changeClip[0];
        audioSource.Play();
    }

    // called when target two image appears
    public void ImageAppearsTargetTwo()
    {
        startButton.SetActive(true);
        stopButton.SetActive(true);
        imageMosque.SetActive(true);
        mosque.SetActive(true);
        startButton.SetActive(true);
        stopButton.SetActive(false);
        audioSource.clip = changeClip[1];
        audioSource.Play();
    }

    // called when target three image appears
    public void ImageAppearsTargetThree()
    {
        startButton.SetActive(true);
        stopButton.SetActive(true);
        imageLittleIndia.SetActive(true);
        littleIndia.SetActive(true);
        startButton.SetActive(true);
        stopButton.SetActive(false);
        audioSource.clip = changeClip[2];
        audioSource.Play();
    }
    //called when any image target disappears from camera view
    public void ImageDisappears()
    {
        chinaTown.GetComponent<Animator>().SetBool("StartAnim", false);
        mooncake.GetComponent<Animator>().SetBool("StartAnim", false);
        kueh.GetComponent<Animator>().SetBool("StartAnim", false);
        mosque.GetComponent<Animator>().SetBool("StartAnim", false);
        prata.GetComponent<Animator>().SetBool("StartAnim", false);
        littleIndia.GetComponent<Animator>().SetBool("StartAnim", false);
        chinaTown.SetActive(false);
        mooncake.SetActive(false);
        kueh.SetActive(false);
        mosque.SetActive(false);
        littleIndia.SetActive(false);
        prata.SetActive(false);
        startButton.SetActive(false);
        stopButton.SetActive(false);
        imageChinatown.SetActive(false);
        imageMooncake.SetActive(false);
        imageKueh.SetActive(false);
        imageMosque.SetActive(false);
        imagePrata.SetActive(false);
        imageLittleIndia.SetActive(false);
        audioSource.clip = changeClip[3];
        audioSource.Play();
    }

    //Animation starts if active in hierarchy and button is pressed
    public void AnimationForTargetOne()
    {
        if (mooncake.activeInHierarchy)
        {
            mooncake.GetComponent<Animator>().SetBool("StartAnim", true);
        }

        if (chinaTown.activeInHierarchy)
        {
            chinaTown.GetComponent<Animator>().SetBool("StartAnim", true);
        }

        if (kueh.activeInHierarchy)
        {
            kueh.GetComponent<Animator>().SetBool("StartAnim", true);
        }

        if (mosque.activeInHierarchy)
        {
            mosque.GetComponent<Animator>().SetBool("StartAnim", true);
        }

        if (prata.activeInHierarchy)
        {
            prata.GetComponent<Animator>().SetBool("StartAnim", true);
        }

        if (littleIndia.activeInHierarchy)
        {
            littleIndia.GetComponent<Animator>().SetBool("StartAnim", true);
        }
    }
    //Animation Stops when button is pressed,checks for object active in hierarchy
    public void StopAnimationForTargetOne()
    {
        if (mooncake.activeInHierarchy)
        {
            mooncake.GetComponent<Animator>().SetBool("StartAnim", false);
        }

        if (chinaTown.activeInHierarchy)
        {
            chinaTown.GetComponent<Animator>().SetBool("StartAnim", false);
        }

        if (kueh.activeInHierarchy)
        {
            kueh.GetComponent<Animator>().SetBool("StartAnim", false);
        }

        if (mosque.activeInHierarchy)
        {
            mosque.GetComponent<Animator>().SetBool("StartAnim", false);
        }

        if (prata.activeInHierarchy)
        {
            prata.GetComponent<Animator>().SetBool("StartAnim", false);
        }

        if (littleIndia.activeInHierarchy)
        {
            littleIndia.GetComponent<Animator>().SetBool("StartAnim", false);
        }
    }


}
