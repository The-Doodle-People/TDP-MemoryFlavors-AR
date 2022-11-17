using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{

    public TextMeshProUGUI quizQuestionText;
    public TextMeshProUGUI tryAgainText;
    public TextMeshProUGUI answerOneText;
    public TextMeshProUGUI answerTwoText;
    public TextMeshProUGUI answerThreeText;
    string objectName;
    int answer = 0;


    private void Update()
    {
        

        Question();
        MCQ();
        Debug.Log("Answer: " + answer);
        Debug.Log("The Food: " + objectName);
    }

    private void Start()
    {
        objectName = StaticController.objectName;
    }

    private void Question()
    {
        if (objectName == "DevilCurry")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Devil Curry";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";
        }
        else if (objectName == "NasiPandang")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Nasi Padang";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";

        }
        else if (objectName == "HokkienMee")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Hokkien Mee";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";

        }
        else if (objectName == "Thosai")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Thosai";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";
        }
        else if (objectName == "Hawker")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Hawker";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";
        }
        else if (objectName == "PushCart")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Question about Hawker";
            answerOneText.text = "1. <insert answer here>";
            answerTwoText.text = "2. <insert answer here>";
            answerThreeText.text = "3. <insert answer here>";
        }
    }

    private void MCQ()
    {
        if (objectName == "DevilCurry")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
               
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
        }
        else if (objectName == "NasiPandang")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
               
                tryAgainText.text = "wrong";
                answer = 0;
            }

        }
        else if (objectName == "HokkienMee")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }

        }
        else if (objectName == "Thosai")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
               
                tryAgainText.text = "wrong";
                answer = 0;
            }

        }
        else if (objectName == "Hawker")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }

        }
        else if (objectName == "PushCart")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }

        }
    }

    public void AnswerOne()
    {
        answer = 1;
    }
    public void AnswerTwo()
    {
        answer = 2;
    }
    public void AnswerThree()
    {
        answer = 3;
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
