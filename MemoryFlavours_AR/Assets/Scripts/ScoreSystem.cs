using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public int playerScore;
    public AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ingredients triggered");
        collectSound.Play();
        if (other.tag == "Ingredients")
        {
            Debug.Log("Ingredients detected");
            playerScore += 1;
            scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
            Debug.Log("Ingredients collected");
        }    
    }
}
