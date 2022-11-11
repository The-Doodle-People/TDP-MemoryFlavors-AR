using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlsAnim : MonoBehaviour
{
    public GameObject chinaTown;
    public GameObject mooncake;
    public GameObject startButton;
    public GameObject stopButton;
    public GameObject ImageChinatown;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageAppearsTargetOne()
    {
        startButton.SetActive(true);
        stopButton.SetActive(true);
        ImageChinatown.SetActive(true);
        startButton.SetActive(true);
        stopButton.SetActive(false);
    }
    public void ImageDisappears()
    {
        chinaTown.GetComponent<Animator>().SetBool("StartAnim", false);
        mooncake.GetComponent<Animator>().SetBool("StartAnim", false);
        chinaTown.SetActive(true);
        mooncake.SetActive(false);
        startButton.SetActive(false);
        stopButton.SetActive(false);
    }
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
    }

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
    }


}
