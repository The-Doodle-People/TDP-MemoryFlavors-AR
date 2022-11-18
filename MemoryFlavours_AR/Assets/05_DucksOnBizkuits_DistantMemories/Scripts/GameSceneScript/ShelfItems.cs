using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfItems : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject> usedPrefabs;

    public GameObject planeFinder;
    
    // remove items within this array from shelf
    public List<int> itemIndex;

    public void InitShelf()
    {
        planeFinder.SetActive(false);
        FindObjectOfType<TouchHandler>().shelfItems = GetComponent<ShelfItems>();
        transform.rotation = Quaternion.Euler((Camera.main.transform.rotation.eulerAngles.y + 180) * Vector3.up);

    }
}
