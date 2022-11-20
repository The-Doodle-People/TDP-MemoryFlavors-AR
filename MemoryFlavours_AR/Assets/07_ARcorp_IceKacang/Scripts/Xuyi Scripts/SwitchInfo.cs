using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInfo : MonoBehaviour
{
    public StarParticles star;
    public GameObject chendolInfo;
    public GameObject jellyInfo;
    public GameObject seedInfo;
    public GameObject cornInfo;
    public GameObject redBeanInfo;
    public GameObject syrupsInfo;
    public GameObject milkInfo;
    public GameObject sugarInfo;
    public GameObject bckBtn;

    public void ShowChendol()
    {
        if (chendolInfo.activeSelf !=true)
        {
            Back();
            chendolInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[0].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowJelly()
    {
        if (jellyInfo.activeSelf != true)
        {
            Back();
            jellyInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[1].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowSeed()
    {
        if (seedInfo.activeSelf != true)
        {
            Back();
            seedInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[2].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowCorn()
    {
        if (cornInfo.activeSelf != true)
        {
            Back();
            cornInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[3].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowRed()
    {
        if (redBeanInfo.activeSelf != true)
        {
            Back();
            redBeanInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[4].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowSyrup()
    {
        if (syrupsInfo.activeSelf != true)
        {
            Back();
            syrupsInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[5].Play();
        }
        else
        {
            Back();
        }
    }

    public void ShowMilk()
    {
        if (milkInfo.activeSelf != true)
        {
            Back();
            milkInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[6].Play();
        }
        else
        {
            Back();
        }
        
    }

    public void ShowSugar()
    {
        if (sugarInfo.activeSelf != true)
        {
            Back();
            sugarInfo.SetActive(true);
            bckBtn.SetActive(true);
            star.starParticles[7].Play();
        }
        else
        {
            Back();
        }
        
    }

    public void Back()
    {
        chendolInfo.SetActive(false);
        seedInfo.SetActive(false);
        jellyInfo.SetActive(false);
        cornInfo.SetActive(false);
        redBeanInfo.SetActive(false);
        syrupsInfo.SetActive(false);
        milkInfo.SetActive(false);
        sugarInfo.SetActive(false);
        bckBtn.SetActive(false);
        star.StopParticles();
    }
}
