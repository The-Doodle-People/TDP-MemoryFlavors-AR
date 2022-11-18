/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description:
 */

using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private static Camera camera;
    #region ChaoHaoVar

    public ShelfItems shelfItems;
    public QuizGenerator quizGenerator;
    
    #endregion

    #region LucasVar

    

    #endregion
    
    // Start is called before the first frame update
    private void Start()
    {
        // because of this line, DO NOT put it under DoNotDestroy
        camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTouchPress()
    {
        // reads the finger position on screen (Vector2(x,y))
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // uses the direction that is pointing towards the 3D object as z axis
        rayPosition.z = camera.nearClipPlane;

        // like a raycast, shoots a ray at the object
        Ray ray = camera.ScreenPointToRay(rayPosition);
        // prep out data
        RaycastHit hitInfo;

        // Guard clause. If the raycast doesnt hit anything, it does do anything
        if (!(Physics.Raycast(ray, out hitInfo))) return;

        // if I hit something with a collider, I do something
        if (hitInfo.collider)
        {
            //Do something
            Debug.Log("TouchDetected " + hitInfo.collider.name);
        }
    }
    
    #region ChaoHao
    
        // get sibling index, add to list, remove
        
    
    #endregion
    
    #region Lucas
    
    
    
    #endregion
}
