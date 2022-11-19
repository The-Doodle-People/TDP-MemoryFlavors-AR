using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f,0f, -touch.deltaPosition.x * rotateSpeed);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
