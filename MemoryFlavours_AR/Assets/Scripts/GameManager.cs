using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject [] dialogues;
    public GameObject NextButton;
    public GameObject PlayAR_Button;
    public GameObject ReplayButton;
    public GameObject UserDialogue;
    public GameObject UserDialogue1;
    public GameObject UserDialogue2;
    public GameObject UserDialogue3;
    public GameObject UserDialogue4;
    public GameObject UserDialogue5;
    public int dialogueNum = 0;

    private int photoIndex;

    public void OnNext()
    {
        dialogues[dialogueNum].gameObject.SetActive(false);
        dialogues[dialogueNum + 1].gameObject.SetActive(true);
        dialogueNum++;

        if (dialogueNum == 2)
        {
            NextButton.gameObject.SetActive(false);
            UserDialogue.SetActive(true);
            UserDialogue1.SetActive(true)
;        }

        if (dialogueNum == 4)
        {
            NextButton.gameObject.SetActive(false);
            UserDialogue2.SetActive(true);
            UserDialogue3.SetActive(true);
        }

        if (dialogueNum == 6)
        {
            NextButton.gameObject.SetActive(false);
            UserDialogue4.SetActive(true);
        }

        if (dialogueNum == 8)
        {
            NextButton.gameObject.SetActive(false);
            UserDialogue5.SetActive(true);
        }
    }

    public void OnUserDialogue()
    {
        dialogues[2].gameObject.SetActive(false);
        UserDialogue.SetActive(false);
        UserDialogue1.SetActive(false);
        NextButton.gameObject.SetActive(true);
    }

    public void OnUserDialogueTwo()
    {
        dialogues[4].gameObject.SetActive(false);
        UserDialogue2.SetActive(false);
        UserDialogue3.SetActive(false);
        NextButton.gameObject.SetActive(true);
    }

    public void OnUserDialogueThree()
    {
        dialogues[6].gameObject.SetActive(false);
        UserDialogue4.SetActive(false);
        NextButton.gameObject.SetActive(true);
    }

    public void OnUserDialogueFour()
    {
        dialogues[8].gameObject.SetActive(false);
        UserDialogue5.SetActive(false);
        PlayAR_Button.SetActive(true);
        ReplayButton.SetActive(true);
        //NextButton.gameObject.SetActive(true);
    }

    public void GoToARFeature()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToConversation()
    {
        SceneManager.LoadScene(1);
    }

    public void ReplayStory()
    {
        SceneManager.LoadScene(0);
    }

    public void TakePhoto()
    {

        ScreenCapture.CaptureScreenshot($"AR{photoIndex}.png");
        //savedPhoto.SetActive(true);
        photoIndex++;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
