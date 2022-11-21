/*
 * Author: Wrigley Studios
 * Date: 20/11/22
 * Description: The Quiz Manager handle the quiz scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

public class QuizManager : MonoBehaviour
{
    /// <summary>
    /// Text display of the question and answer
    /// </summary>
    public TextMeshProUGUI quizQuestionText;
    public TextMeshProUGUI tryAgainText;
    public TextMeshProUGUI answerOneText;
    public TextMeshProUGUI answerTwoText;
    public TextMeshProUGUI answerThreeText;

    /// <summary>
    /// Toggle button to turn on and off the AR Camera
    /// </summary>
    public GameObject toggleImageTarget;

    /// <summary>
    /// Enable user to go back to AR scene
    /// </summary>
    public GameObject CompleteBtn;
    string objectName;
    int answer = 0;
    bool isCorrect = false;

    /// <summary>
    /// Initialzie Function and update
    /// </summary>
    private void Update()
    {
        Question();
        MCQ();
        CompleteButtonCheck();
        Debug.Log("Answer: " + answer);
        Debug.Log("The Food: " + objectName);
        Debug.Log("Answer is Correct: " + isCorrect);
    }

    /// <summary>
    /// Initialize the variable, retrieve objectName from Static Controller
    /// </summary>
    private void Start()
    {
        objectName = StaticController.objectName;
        isCorrect = false;
        tryAgainText.text = "";
    }

    /// <summary>
    /// Question Function, when objectName received the variable from Static Controller,
    /// It will vet through which variable to be able to execute the code.
    /// </summary>
    private void Question()
    {
        // If objectName is equal to the string, following line will be executed
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

    /// <summary>
    /// MCQ Function, when objectName received the variable from Static Controller,
    /// It will vet through which variable to be able to execute the code.
    /// User have to select the correct answer, if wrong is red and correct is green
    /// </summary>
    private void MCQ()
    {
        // If objectName is equal to the string, following line will be executed
        if (objectName == "DevilCurry")
        {
            // If answer based on the user input, if is 1, the if statement of 1 will be executed and vice versa
            if (answer == 1)
            {
                answerOneText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                isCorrect = true;
                answerTwoText.color = Color.green;
                tryAgainText.text = "Correct";
                answer = 0;
            }
            else if (answer == 3)
            {
                answerThreeText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
        }
        else if (objectName == "NasiPandang")
        {
            if (answer == 1)
            {
                answerOneText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                answerTwoText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                isCorrect = true;
                answerThreeText.color = Color.green;
                tryAgainText.text = "Correct";
                answer = 0;
            }

        }
        else if (objectName == "HokkienMee")
        {
            if (answer == 1)
            {
                answerOneText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                isCorrect = true;
                answerTwoText.color = Color.green;
                tryAgainText.text = "Correct";
                answer = 0;
            }
            else if (answer == 3)
            {
                answerThreeText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }

        }
        else if (objectName == "Thosai")
        {
            if (answer == 1)
            {
                isCorrect = true;
                answerOneText.color = Color.green;
                tryAgainText.text = "Correct!";
                answer = 0;
            }
            else if (answer == 2)
            {
                answerTwoText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                answerThreeText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }

        }
        else if (objectName == "Hawker")
        {
            if (answer == 1)
            {
                answerOneText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 2)
            {
                isCorrect = true;
                answerTwoText.color = Color.green;
                tryAgainText.text = "Correct";
                answer = 0;
            }
            else if (answer == 3)
            {
                answerThreeText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }

        }
        else if (objectName == "PushCart")
        {
            if (answer == 1)
            {
                isCorrect = true;
                answerOneText.color = Color.green;
                tryAgainText.text = "Correct";
                answer = 0;
            }
            else if (answer == 2)
            {
                answerTwoText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }
            else if (answer == 3)
            {
                answerThreeText.color = Color.red;
                tryAgainText.text = "Wrong";
                answer = 0;
            }

        }
    }

    /// <summary>
    /// Function to change answer to one
    /// </summary>
    public void AnswerOne()
    {
        answer = 1;
    }

    /// <summary>
    /// Function to change answer to two
    /// </summary>
    public void AnswerTwo()
    {
        answer = 2;
    }

    /// <summary>
    /// Function to change answer to three
    /// </summary>
    public void AnswerThree()
    {
        answer = 3;
    }

    /// <summary>
    /// If the answer is correct, the return button will appear
    /// </summary>
    public void CompleteButtonCheck()
    {
        if(isCorrect == true)
        {
            CompleteBtn.gameObject.SetActive(true);
        }
        else
        {
            CompleteBtn.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggle Camera, the user can toggle on and off the camera if the background is too distracting
    /// when answer the question.
    /// </summary>
    public void ToggleCamera()
    {
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
            toggleImageTarget.SetActive(false);

        }
        else
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
            toggleImageTarget.SetActive(true);
        }
    }
    /// <summary>
    /// Go back to main menu
    /// </summary>
    public void GoBackToMain()
    {
        SceneManager.LoadScene(1);
    }
}
