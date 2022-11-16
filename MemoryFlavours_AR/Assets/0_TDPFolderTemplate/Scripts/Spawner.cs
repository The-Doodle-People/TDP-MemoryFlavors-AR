using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Spawner : MonoBehaviour
{

    public GameObject dimsums;

    public float xBound = 480f;
    public float yBound = 2280f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());

    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        if(Random.value < .6f)
        {
            Instantiate(dimsums,new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity);
        }
        
        StartCoroutine(SpawnRandomGameObject());

    }

    
}
