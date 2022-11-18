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
        }

        if (timerStart && countdown > 0)
        {
            countdown -= Time.deltaTime;
            timerTxt.text = countdown.ToString("0.0");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        turn++;
        var targetTag = other.tag;
        if (!quizGenerator.selectedIngredients.Contains(targetTag)) return;
        if (other.transform.parent) return;

        score++;
        scoreTxt.text = "Score: " + score + " / 5";
    }

    public void EndGame()
    {
        winScreen.SetActive(true);
        finalScoreTxt.text = "Score: " + score + " / 5";
        timeTaken.text = "Time Taken: " + (maxCountdown - countdown).ToString("0.0");
    }
}
