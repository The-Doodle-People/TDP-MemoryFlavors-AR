using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject dimsums;

    public ScoreController game;

    public GameObject dimSumObject;

    public GameObject parentCanvas;

    private float xBound = 500f;
    private float yBound = 2280f;
    // Start is called before the first frame update
    void Start()
    {
        //start summoning the dimsums
        StartCoroutine(SpawnRandomGameObject());

    }

    IEnumerator SpawnRandomGameObject()
    {


        //float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(1000, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(10000, 0)).x);
        //Debug.Log(spawnX);
        //Vector2 spawnPosition = new Vector2(spawnX, 1142f);
        //Debug.Log(spawnPosition);
        //if(Random.value < .6f)
        //{
        //    Instantiate(dimsums,new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity);
        //}

        // Set Instantiated object to dimSumObject
        dimSumObject = Instantiate(dimsums, new Vector2(Random.Range(-xBound + 600, xBound + 500), yBound), Quaternion.identity);
        // Set dimSumObject as a child of the canvas
        dimSumObject.transform.SetParent(parentCanvas.transform);

        if (game.gameOver == false)
        {
            ///for every 1 to 2 seconds, the Dim Sums will fall within the x and y bounds.
            yield return new WaitForSeconds(Random.Range(1, 2));
            StartCoroutine(SpawnRandomGameObject());
        }
        else
        {
            Debug.Log("Stop Coroutine");
            StopCoroutine(SpawnRandomGameObject());
        }


    }

    private void Update()
    {

    }


}
