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
            quizQuestionText.text = "What is Devil Curry also known as?";
            answerOneText.text = "1. Monster Curry";
            answerTwoText.text = "2. Curry Debal";
            answerThreeText.text = "3. Spicy Curry";
        }
        else if (objectName == "NasiPandang")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Where is Nasi Padang from?";
            answerOneText.text = "1. Singapore";
            answerTwoText.text = "2. Malaysia";
            answerThreeText.text = "3. Indonesia";

        }
        else if (objectName == "HokkienMee")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "What is added to enhance the fragrance of hokkien mee?";
            answerOneText.text = "1. Prawn";
            answerTwoText.text = "2. Fried Lard";
            answerThreeText.text = "3. Rice Noodles";

        }
        else if (objectName == "Thosai")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Is Thosai vegetarian?";
            answerOneText.text = "1. Yes";
            answerTwoText.text = "2. No";
            answerThreeText.text = "3. Mix of both";
        }
        else if (objectName == "Hawker")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "When did Hawker centers become popular?";
            answerOneText.text = "1. 2000s";
            answerTwoText.text = "2. 1950s";
            answerThreeText.text = "3. 1700s ";
        }
        else if (objectName == "PushCart")
        {
            StaticController.objectName = "";
            quizQuestionText.text = "Why is there street hawking?";
            answerOneText.text = "1. No money";
            answerTwoText.text = "2. Free and easy";
            answerThreeText.text = "3. Nothing do";
        }
    }

    private void MCQ()
    {
        if (objectName == "DevilCurry")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
               
                tryAgainText.text = "correct";
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
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
               
                tryAgainText.text = "correct";
                answer = 0;
            }

        }
        else if (objectName == "HokkienMee")
        {
            if (answer == 1)
            {
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "correct";
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
                
                tryAgainText.text = "wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                
                tryAgainText.text = "correct";
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
