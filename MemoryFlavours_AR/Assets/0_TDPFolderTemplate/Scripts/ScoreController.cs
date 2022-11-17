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
    public GameObject gameOverMessage;

    public int score;
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
            if(score < 5)
            {
                if (gameOver == false)
                {
                    Debug.Log("item collided");
                    Destroy(target.gameObject);
                    score++;
                }
                
            }
            else
            {

                gameOver = true;
                Debug.Log("GameOver is "+ gameOver.ToString());
                GameOver();
            }


        }
        
    }
    public void GameOver()
    {
        gameOverMessage.SetActive(true);
    }

}
