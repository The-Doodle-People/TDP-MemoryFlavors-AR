using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using BitStrap;
using System.Drawing.Printing;

public class ImgSlide : MonoBehaviour
{
    [Header("Image")]
    public Image NewImg;

    [Header("ScrollBar")]
    public Scrollbar bar;

    private float ScrollToImg;

    private void Awake()
    {
        bar = bar.GetComponent<Scrollbar>();
    }

    public void total()
    {
        float FillAmount = bar.value + ScrollToImg;

        NewImg.fillAmount = FillAmount;
    }
}
