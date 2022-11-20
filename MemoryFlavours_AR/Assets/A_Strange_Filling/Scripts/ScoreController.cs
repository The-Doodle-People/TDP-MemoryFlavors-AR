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
    public SFXManager sfx;
    public Spawner spawner;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if(score < 30)
            {
                if (gameOver == false)
                {
                    Debug.Log("item collided");
                    Destroy(target.gameObject);
                    score++;
                    sfx.CollectAudioOn();
                }
            }
            else
            {

                gameOver = true;
                //Debug.Log("GameOver is "+ gameOver.ToString());
                Debug.Log("Game ended " + gameOver);
                GameOver();
            }
        }  
    }
    public void GameOver()
    {
        gameOverMessage.SetActive(true);
        spawner.StopCoroutine("SpawnRandomGameObject");
    }

}
