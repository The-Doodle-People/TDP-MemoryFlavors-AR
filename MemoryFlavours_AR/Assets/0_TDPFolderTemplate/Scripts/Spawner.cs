using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject dimsums;

    

    private float xBound = 480f;
    private float yBound = 2280f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());

    }

    IEnumerator SpawnRandomGameObject()
    {
        ///for every 1 to 2 seconds, the Dim Sums will fall within the x and y bounds.
        yield return new WaitForSeconds(Random.Range(1, 2));

        if(Random.value < .6f)
        {
            Instantiate(dimsums,new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity);
        }
        
        StartCoroutine(SpawnRandomGameObject());

    }

    
}
