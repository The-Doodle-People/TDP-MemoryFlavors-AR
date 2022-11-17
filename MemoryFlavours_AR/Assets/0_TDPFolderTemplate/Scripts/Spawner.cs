using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject dimsums;

    public ScoreController game;


    private float xBound = 480f;
    private float yBound = 2280f;
    // Start is called before the first frame update
    void Start()
    {
        //start summoning the dimsums
        StartCoroutine(SpawnRandomGameObject());

    }

    IEnumerator SpawnRandomGameObject()
    {
        
        

        //if(Random.value < .6f)
        //{
        //    Instantiate(dimsums,new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity);
        //}
        Instantiate(dimsums, new Vector2(Random.Range(-xBound, xBound), yBound), Quaternion.identity);

        if (game.gameOver == false)
        {
            ///for every 1 to 2 seconds, the Dim Sums will fall within the x and y bounds.
            yield return new WaitForSeconds(Random.Range(1, 2));
            StartCoroutine(SpawnRandomGameObject());
        }
        else
        {
            StopCoroutine(SpawnRandomGameObject());
        }


    }

    private void Update()
    {

    }


}
