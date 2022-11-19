using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    [Header("For ethnicities allocation")] private GameObject gender;

    public Material[] ethnicities;

    [Header("For Game")] public int quizId;

    public GameObject prompt;

    public TextMeshProUGUI speech;

    void Start()
    {
        int min = 0;
        int max = 2;

        // set gender
        gender = transform.GetChild(UnityEngine.Random.Range(min, max)).gameObject;
        gender.SetActive(true);

        max = ethnicities.Length;
        gender.GetComponent<MeshRenderer>().material = ethnicities[UnityEngine.Random.Range(min, max)];
    }

    public void ChangeChat()
    {
        /*
         * "IceGemBiscuits", "BiscuitPiring", "Murukku", "ChocolateEggs"
         */
        prompt.SetActive(false);
        speech.transform.parent.gameObject.SetActive(true);
        var type = quizId switch
        {
            0 => "Ice Gem Biscuits",
            1 => "Biscuit Piring",
            2 => "Murukku",
            3 => "Chocolate Eggs",
            _ => ""
        };

        speech.text = "I love " + type + ". Can you make me some?";
    }
}