using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ImgSwipe : MonoBehaviour
{
    [Header("Image Set")]
    public Image one;
    public Image two;
    public Image three;
    public Image four;
    public Image five;
    public Image six;
    public Image seven;
    public Image eight;

    [Header("Next Button")]
    public Button next;

    [Header("Previous Button")]
    public Button previous; 


    private void Awake()
    {
        Button nextBTN = next.GetComponent<Button>();

        nextBTN.onClick.AddListener(imagerotate);

        Button previousBTN = previous.GetComponent<Button>();

        previousBTN.onClick.AddListener(previousimage);

    }

    private void imagerotate()
    {
        if (one.fillAmount == 1)
        {
            two.fillAmount = 1;
            one.fillAmount = 0;
        }
        else if (two.fillAmount == 1)
        {
            three.fillAmount = 1;
            two.fillAmount = 0;
        }
        else if (three.fillAmount == 1)
        {
            four.fillAmount = 1;
            three.fillAmount = 0;
        }
        else if (four.fillAmount == 1)
        {
            five.fillAmount = 1;
            four.fillAmount = 0;
        }
        else if (five.fillAmount == 1)
        {
            six.fillAmount = 1;
            five.fillAmount = 0;
        }
        else if (six.fillAmount == 1)
        {
            seven.fillAmount = 1;
            six.fillAmount = 0;
        }
        else if (seven.fillAmount == 1)
        {
            eight.fillAmount = 1;
            seven.fillAmount = 0;
        }
    }

    private void previousimage()
    {
        if (eight.fillAmount == 1)
        {
            seven.fillAmount = 1;
            eight.fillAmount = 0;
        }
        else if (seven.fillAmount == 1)
        {
            six.fillAmount = 1;
            seven.fillAmount = 0;
        }
        else if (six.fillAmount == 1)
        {
            five.fillAmount = 1;
            six.fillAmount = 0;
        }
        else if (five.fillAmount == 1)
        {
            four.fillAmount = 1;
            five.fillAmount = 0;
        }
        else if (four.fillAmount == 1)
        {
            three.fillAmount = 1;
            four.fillAmount = 0;
        }
        else if (three.fillAmount == 1)
        {
            two.fillAmount = 1;
            three.fillAmount = 0;
        }
        else if (two.fillAmount == 1)
        {
            one.fillAmount = 1;
            two.fillAmount = 0;
        }
    }
}
