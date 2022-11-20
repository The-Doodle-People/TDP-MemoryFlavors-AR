using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int totalScore;

    public void Update()
    {
        scoreText.GetComponent<Text>().text = "INGREDIENTS: " + totalScore;
    }
}
