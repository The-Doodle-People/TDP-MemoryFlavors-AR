using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTopping : MonoBehaviour
{
    public GameObject corn;
    public GameObject redBean;
    public GameObject durian;
    public GameObject milk;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectCorn()
    {
        /// If corn is active , hide it
        if (corn.activeSelf)
        {
            corn.SetActive(false);
        }
        /// Else if corn is no
        else
        {
            corn.SetActive(true);
        }
       
    }
    public void SelectRedBean()
    {
        /// If corn is active , hide it
        if (redBean.activeSelf)
        {
            redBean.SetActive(false);
        }
        /// Else if corn is not active , enable it
        else
        {
            redBean.SetActive(true);
        }
        
    }

    public void SelectMilk()
    {
        /// If corn is active , hide it
        if (milk.activeSelf)
        {
            milk.SetActive(false);
        }
        /// Else if corn is not active , enable it
        else
        {
            milk.SetActive(true);
        }
        
    }
    public void SelectDurian()
    {
        /// If corn is active , hide it
        if (durian.activeSelf)
        {
            durian.SetActive(false);
            Debug.Log("durian hidden");
        }
        /// Else if corn is not active , enable it
        else
        {
            durian.SetActive(true);
            Debug.Log("durian shown");
        }

    }
}
