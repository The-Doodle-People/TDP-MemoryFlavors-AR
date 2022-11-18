using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizLogic : MonoBehaviour
{
    /*
 *  1. Mee siam
    2. 17Nov 1928
    3. Makan Mana
    4. 3
 */
    // Objects

    [Header("Quiz UI")]
    public GameObject UIOne;
    public GameObject UITwo;
    public GameObject UIThree;
    public GameObject UIFour;

    [Header("Total Score UI")]
    public GameObject UITotal;

    //Anwsers
    [Header("Anwser Set One")]
    public Button AnwOne;
    public Button AnwTwo;

    [Header("Anwser Set Two")]
    public Button AnwThree;
    public Button AnwFour;

    [Header("Anwser Set Three")]
    public Button AnwFive;
    public Button AnwSix;

    [Header("Anwser Set Four")]
    public Button AnwSeven;
    public Button AnwEight;


    // Text 
    [Header("Correct Anwser")]
    public TMP_Text CorretAnwser;

    [Header("Score Percental")]
    public TMP_Text Percental;


    private float Correct = 25f;
    private float Total = 0f;

    private bool isCorrect;
    [SerializeField]
    private int CorrectCounter;


    private void Awake()
    {
        
        Button One = AnwOne.GetComponent<Button>();
        Button Two = AnwTwo.GetComponent<Button>();
        Button Three = AnwThree.GetComponent<Button>();
        Button Four = AnwFour.GetComponent<Button>();
        Button Five = AnwFive.GetComponent<Button>();
        Button Six = AnwSix.GetComponent<Button>();
        Button Seven = AnwSeven.GetComponent<Button>();
        Button Eight = AnwEight.GetComponent<Button>();


        One.onClick.AddListener(OneA);
        Two.onClick.AddListener(TwoA);
        Three.onClick.AddListener(ThreeA);
        Four.onClick.AddListener(FourA);
        Five.onClick.AddListener(FiveA);
        Six.onClick.AddListener(SixA);
        Seven.onClick.AddListener(SevenA);
        Eight.onClick.AddListener(EightA);
    }

    private void OneA() // Confit De Canardw
    {
        isCorrect = false;

        CorrectCounter = CorrectCounter + 0;

        UITwo.SetActive(true);
        UIOne.SetActive(false);

        Percental.text = Correct.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void TwoA() // Mee Siam
    {
        isCorrect = true;

        CorrectCounter = CorrectCounter + 1;

        Total = Total + Correct;
        UITwo.SetActive(true);
        UIOne.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }    
    
    private void ThreeA() // 17Nov1928
    {
        isCorrect = true;

        CorrectCounter = CorrectCounter + 1;

        Total = Total + Correct;
        UIThree.SetActive(true);
        UITwo.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void FourA() // 17Nov1982
    {
        isCorrect = false;

        CorrectCounter = CorrectCounter + 0;

        UIThree.SetActive(true);
        UITwo.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void FiveA() // Makan Mana
    {
        isCorrect = true;

        CorrectCounter = CorrectCounter + 1;

        Total = Total + Correct;
        UIFour.SetActive(true);
        UIThree.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void SixA() // Makan Apa
    {
        isCorrect = false;

        CorrectCounter = CorrectCounter + 0;

        UIFour.SetActive(true);
        UIThree.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void SevenA() // 3
    {
        isCorrect = true;

        CorrectCounter = CorrectCounter + 1;

        Total = Total + Correct;
        UITotal.SetActive(true);
        UIFour.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }

    private void EightA() // 4
    {
        isCorrect = false;

        CorrectCounter = CorrectCounter + 0;

        UITotal.SetActive(true);
        UIFour.SetActive(false);

        Percental.text = Total.ToString();
        CorretAnwser.text = CorrectCounter.ToString();
    }
}