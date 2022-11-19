using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector] public QuizGenerator quizGenerator;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI timerTxt;
    public int score = 0;
    public int turn = 0;

    public float maxCountdown = 90f;

    private float countdown;

    public bool timerStart;
    
    [Header("Contains Final Scores")] 
    public TextMeshProUGUI finalScoreTxt;

    public GameObject winScreen;

    public TextMeshProUGUI timeTaken;
    
    private void Start()
    {
        quizGenerator = FindObjectOfType<QuizGenerator>();
        countdown = maxCountdown;
    }

    private void Update()
    {
        if (turn >= 5 || countdown < 0)
        {
            //calculate score + end game
            EndGame();
            timerTxt.text = "0.0";
            timerStart = false;
        }

        if (timerStart && countdown > 0)
        {
            countdown -= Time.deltaTime;
            timerTxt.text = countdown.ToString("0.0");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var targetTag = other.tag;
        // check if valid obj
        if (other.transform.parent) return;
        turn++;
        // check if ans correct
        if (!quizGenerator.selectedIngredients.Contains(targetTag)) return;

        score++;
        scoreTxt.text = "Score: " + score + " / 5";
        
        // prevent score dup
        var chosen = new List<string>();
        foreach (string ingredients in quizGenerator.selectedIngredients)
        {
            if (ingredients == targetTag)
            {
                // add line to add ingredients to list to check for duplicates
                continue;
            }
            chosen.Add(ingredients);
        }

        quizGenerator.selectedIngredients = chosen;
    }

    public void EndGame()
    {
        winScreen.SetActive(true);
        finalScoreTxt.text = "Score: " + score + " / 5";
        timeTaken.text = "Time Taken: " + (maxCountdown - countdown).ToString("0.0");
    }
}
