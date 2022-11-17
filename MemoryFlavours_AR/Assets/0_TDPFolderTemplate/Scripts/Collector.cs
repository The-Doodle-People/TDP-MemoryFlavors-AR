using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collector : MonoBehaviour
{
    public ScoreController game;

    public GameObject gameOverMessage;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "DimSum")
        {
            game.gameOver = true;
            Debug.Log("GameOver is" + game.gameOver.ToString());
            Destroy(target.gameObject);
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverMessage.SetActive(true);
    }
    
}
