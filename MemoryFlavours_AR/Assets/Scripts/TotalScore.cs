/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script to calculate total scores of objects collected
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int totalScore;
    public GameObject videoSource; // to attach video player object
    public GameObject firstDescription; // to attach text of 'first description' 
    public GameObject secondDescription; // to attach text of 'second description' 

    public void Update()
    {
        // update the score of objects collected
        scoreText.text = "INGREDIENTS: " + totalScore;

        // to deactivate firstDescription and activate secondDescription and video player object
        if(totalScore > 9)
        {
            firstDescription.SetActive(false);
            secondDescription.SetActive(true);
            videoSource.SetActive(true);
        }
    }
}
