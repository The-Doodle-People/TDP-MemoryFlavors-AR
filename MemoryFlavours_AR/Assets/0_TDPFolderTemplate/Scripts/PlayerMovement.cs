using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody;

    public ScoreController game;

    //movement speed in units per second
    private float movementSpeed = 500f;

    private float xBound = 380f;

    // Start is called before the first frame update
    void Start()
    {
        if (game.gameOver == false)
        {
            myBody = GetComponent<Rigidbody2D>();
        }
            //have to find it because rigid body is private
    }

    // FixedUpdate is called once per frame and its reccomended with the rigid body

    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");

        //if(h > 0)
        //{
            //myBody.velocity = Vector2.right * movementSpeed;
        //}
        //else if(h < 0)
        //{
            //myBody.velocity = Vector2.left * movementSpeed;
        //}
        //else
        //{
            //myBody.velocity = Vector2.zero;
        //}
    }

    //if the player press the left button, the player will move left
    public void MoveLeft()
    {
        if (game.gameOver == false)
        {
            myBody.velocity = Vector2.left * movementSpeed;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }

    }

    //if the player press the right button, the player will move right
    public void MoveRight()
    {
        if (game.gameOver == false)
        {
            myBody.velocity = Vector2.right * movementSpeed;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }
            
    }

    //if the player is not pressing any button, the player will not move
    public void StopMoving()
    {

        myBody.velocity = Vector2.zero;

    }

}
