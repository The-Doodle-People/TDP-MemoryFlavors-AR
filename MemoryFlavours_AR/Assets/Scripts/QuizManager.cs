using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{   
    /// <summary>
    /// calling list from QuestionAndAnswers script
    /// </summary>
    public List<QuestionsAndAnswers> QnA;

    /// <summary>
    /// the answer options
    /// </summary>
    public GameObject[] options;

    /// <summary>
    /// the current qn the user is on
    /// </summary>
    public int currentQuestions;

    /// <summary>
    /// reference to questions text
    /// </summary>
    public TMP_Text QuestionText;

    private void Start()
    {
        generateQuestions();
    }

    /// <summary>
    /// remove previous qn and put the 
    /// </summary>
    public void correct()
    {
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }
    /// <summary>
    /// Check if answer is correct 
    /// </summary>
    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestions].Answers[i];
           
            if(QnA[currentQuestions].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    /// <summary>
    /// randomly generate the questions based on the questions available
    /// </summary>
    void generateQuestions()
    {   
        /// Random number from 0 to the total number to questions 
        currentQuestions = Random.Range(0, QnA.Count);

        QuestionText.text = QnA[currentQuestions].Question ;
        SetAnswers();
    }
}
