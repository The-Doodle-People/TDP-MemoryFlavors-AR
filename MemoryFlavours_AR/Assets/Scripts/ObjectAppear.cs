using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppear : MonoBehaviour
{
    public GameObject item;
    public void appear()
    {
        item.SetActive(true);
    }

    public void disappear()
    {
        item.SetActive(false);
    }
}
