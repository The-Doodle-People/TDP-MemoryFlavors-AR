using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfIceCreamWafer;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfIceCreamWafer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        scoreText.text = "Score: " + numberOfIceCreamWafer;
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
