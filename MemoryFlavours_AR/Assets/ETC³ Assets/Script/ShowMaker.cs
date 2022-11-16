using Mapbox.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMaker : MonoBehaviour
{
    private SpawnOnMap k;
    private GameObject Marker;

    private void Awake()
    {
        k = k.GetComponent<SpawnOnMap>();
    }

    public void DisableMaker()
    {
        k._markerPrefab = Marker;

        DestroyImmediate(Marker, true);
    }
}
