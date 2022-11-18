using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    /// <summary>
    /// to store message to display
    /// </summary>
    public string[] lines;

    /// <summary>
    /// the speed for the word characters to show up
    /// </summary>
    public float textSpeed;

    /// <summary>
    /// to substract where we are at in the dialogue
    /// </summary>
    private int index;

    public GameObject groundPlaneCup;

    public GameObject Tracker;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //type out each character one by one 
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            groundPlaneCup.SetActive(true);
            Tracker.SetActive(true);
        }
    }

    /*public void CupAppear()
    {
        if(index > lines.Length)
        {
            
        }
    }*/
}
