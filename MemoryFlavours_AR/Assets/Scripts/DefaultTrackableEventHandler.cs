using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class DefaultTrackableEventHandler : MonoBehaviour
{
    public GameObject videoPlayBack;

    // Start is called before the first frame update
    void Start()
    {
        //videoPlayBack = GameObject.Find("videoplayback").GetComponent<VideoPlayer>().Play();
        //videoPlayBack.GetComponent<DefaultTrackableEventHandler>().Invoke(videoPlayBack, float);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}