using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionText;
    //To find the total number of question is there in the quiz.
    public int totalQuestion = 0;
    //To find the number of right answer you got.
    public int rightAnswer = 0;
    public GameObject EndTemplate;
    public TextMeshProUGUI CorrectAns;
    public TextMeshProUGUI TotalAns;

    public void Start()
    {
        //randomize questions
        generateQuestion();
    }
    public void Update()
    {
        if (QnA.Count == 0)
        {
            quizDone();
            Debug.Log("FINIHSED QUIZ");
        }
    }

    public void correct()
    {
        //remove previous question and generate new one
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionText.text = QnA[currentQuestion].Question;
        SetAnswers();
    }
    public void TickAnswer()
    {
        rightAnswer++;
        Debug.Log(rightAnswer + "rightAnswer");

        totalQuestion++;
        Debug.Log("totalQuestion" + totalQuestion);
    }
    public void XAnswer()
    {
        totalQuestion++;
        Debug.Log("totalQuestion" + totalQuestion);
    }
    public void quizDone()
    {
        Debug.Log("All Question has been answered");
        //Debug.Log(rightAnswers + "/" + answeredAnswers);
        EndTemplate.SetActive(true);
        CorrectAns.text = rightAnswer.ToString();
        TotalAns.text = totalQuestion.ToString();
    }

}
