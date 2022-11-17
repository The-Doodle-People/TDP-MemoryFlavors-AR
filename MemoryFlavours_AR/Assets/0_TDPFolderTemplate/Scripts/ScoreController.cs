using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public bool gameOver;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;   
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        
        if (target.tag == "DimSum")
        {
            Debug.Log("item collided");
            Destroy(target.gameObject);

            if (gameOver == false)
            {
                score++;
            }
            
            
        }
    }

}
