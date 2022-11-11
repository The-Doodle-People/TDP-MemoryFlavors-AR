using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    public GameObject[] item;
    public int index;
    public void change()
    {
        item[index].SetActive(false);
        index++;
        int length = item.Length;
        if (index >= length)
        {
            index = 0;
        }

        item[index].SetActive(true);
        
    }   
}
