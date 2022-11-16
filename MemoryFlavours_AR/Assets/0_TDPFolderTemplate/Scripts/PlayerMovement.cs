using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody;

    //movement speed in units per second
    private float movementSpeed = 500f;

    private float xBound;

    // Start is called before the first frame update
    void Start()
    {
        //have to find it because rigid body is private
        myBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per frame and its reccomended with the rigid body

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if(h > 0)
        {
            myBody.velocity = Vector2.right * movementSpeed;
        }
        else if(h < 0)
        {
            myBody.velocity = Vector2.left * movementSpeed;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }
    }
}
