using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesController : MonoBehaviour
{   
    /// <summary>
    /// variables related to the info and quiz appearing
    /// </summary>
    public GameObject info1;
    public GameObject quizButton;
    
    /// <summary>
    /// When called for quiz button and info is shown
    /// </summary>
    public void Interact()
    {
        quizButton.SetActive(true);
        info1.SetActive(true);
    }
   
}
