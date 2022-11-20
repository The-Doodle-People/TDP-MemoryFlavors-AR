using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public void Answer()
    {
        if(isCorrect)
        {
            //if answer is correct, check as correct answer
            quizManager.TickAnswer();
            Debug.Log("Coreect Answer");
            quizManager.correct();
        }
        else
        {
            //if answer is incorrect, check as incorrect answer
            quizManager.XAnswer();
            Debug.Log("Wrong Answer");
            quizManager.correct();
        }
    }
}
