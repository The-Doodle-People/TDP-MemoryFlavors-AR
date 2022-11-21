/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for dialogue
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// class for dialogue 
/// </summary>
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

    /// <summary>
    /// link groundplane cup 
    /// </summary>
    public GameObject groundPlaneCup;

    /// <summary>
    /// link tracker UI
    /// </summary>
    public GameObject Tracker;

    /// <summary>
    /// link 2d drawing aunty
    /// </summary>
    public GameObject Aunty;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    /// <summary>
    /// function to start dialogue
    /// </summary>
    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// function to type characters one by one
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeLine()
    {
        //type out each character one by one 
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// function to move on to the next link in dialogue
    /// </summary>
    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else //when dialogue ends
        {
            gameObject.SetActive(false);
            groundPlaneCup.SetActive(true);
            Tracker.SetActive(true); //set UI tracker to true, to spawn objects
            Aunty.SetActive(false); //set auntie drawing to false
        }
    }

    /*public void CupAppear()
    {
        if(index > lines.Length)
        {
            
        }
    }*/
}
