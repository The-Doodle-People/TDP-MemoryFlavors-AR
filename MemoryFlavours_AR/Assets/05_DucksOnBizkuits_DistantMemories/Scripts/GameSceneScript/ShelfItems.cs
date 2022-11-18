using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfItems : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject> usedPrefabs;

    // remove items within this array from shelf
    public List<int> itemIndex;
    
    /*#if UNITY_EDITOR

    [Header("Editor Only")]
    [Range(0, 1f)] public float wireSphereRadius = 0.2f;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform itemPos in transform)
        {
            Gizmos.DrawWireSphere(itemPos.position, wireSphereRadius);
        }
    }

    #endif*/

    public void InitShelf()
    {
        Debug.Log("working!");
    }
}
