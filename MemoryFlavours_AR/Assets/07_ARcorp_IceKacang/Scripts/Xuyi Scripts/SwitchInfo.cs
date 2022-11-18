using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInfo : MonoBehaviour
{

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
            chendolInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            chendolInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowJelly()
    {
        if (jellyInfo.activeSelf != true)
        {
            jellyInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            jellyInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowSeed()
    {
        if (seedInfo.activeSelf != true)
        {
            seedInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            seedInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowCorn()
    {
        if (cornInfo.activeSelf != true)
        {
            cornInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            cornInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowRed()
    {
        if (redBeanInfo.activeSelf != true)
        {
            redBeanInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            redBeanInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowSyrup()
    {
        if (syrupsInfo.activeSelf != true)
        {
            syrupsInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            syrupsInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
    }

    public void ShowMilk()
    {
        if (milkInfo.activeSelf != true)
        {
            milkInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            milkInfo.SetActive(false);
            bckBtn.SetActive(false);
        }
        
    }

    public void ShowSugar()
    {
        if (sugarInfo.activeSelf != true)
        {
            sugarInfo.SetActive(true);
            bckBtn.SetActive(true);
        }
        else
        {
            sugarInfo.SetActive(false);
            bckBtn.SetActive(false);
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
    }
}
