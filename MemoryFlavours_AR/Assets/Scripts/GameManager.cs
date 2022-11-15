using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject [] dialogues;
    int dialogueNum = 0;

    public void OnNext()
    {
        dialogues[dialogueNum].SetActive(true);
        dialogueNum++;
        dialogues[dialogueNum - 1].SetActive(false);
    }
}
