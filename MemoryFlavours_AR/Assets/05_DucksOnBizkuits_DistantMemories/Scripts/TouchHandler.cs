/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description:
 */

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    [HideInInspector] public Camera camera;

    #region ChaoHaoVar

    [HideInInspector] public GameUI gameUI;
    [HideInInspector] public ShelfItems shelfItems;
    [HideInInspector] public QuizGenerator quizGenerator;
    [HideInInspector] public GameManager gameManager;
    public bool holdingItem;

    private bool firstCall = true;
    
    #endregion

    #region LucasVar

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // because of this line, DO NOT put it under DoNotDestroy
        camera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
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
        var layerMasking = LayerMask.GetMask("Ignore Raycast");
        
        // Guard clause. If the raycast doesnt hit anything, it does do anything
        if (!(Physics.Raycast(ray, out hitInfo, layerMasking))) return;

        // if I hit something with a collider, I do something
        if (hitInfo.collider)
        {
            //Do something
            Debug.Log("TouchDetected " + hitInfo.collider.name);
            EditItem(hitInfo);
            PlaceItem(hitInfo);
        }
    }

    #region ChaoHao

    // get sibling index, add to list, remove
    private void EditItem(RaycastHit hitInfo)
    {
        var targetTag = hitInfo.collider.transform.tag;

        // remember to comment out for dev build
        if (gameManager.sceneIndex != 2) return;
        
        // if the tag of the item is within the array of "all ingredients", continue with code)
        // only run if user is not holding on an item
        if (System.Array.Exists(quizGenerator.allIngredients, element => element == targetTag) && !holdingItem)
        {
            // if object does not have a parent (Instantiated Obj) return;
            if (!hitInfo.collider.transform.parent) return;
            holdingItem = true;
            var handObject = camera.transform.GetChild(1);
            Debug.Log("TouchDetected " + hitInfo.collider.tag);
            // future code? If UI function to change position of the shelf, might need this!
            shelfItems.itemIndex.Add(hitInfo.collider.transform.parent.GetSiblingIndex());
            Destroy(hitInfo.collider.gameObject);

            // switch off all the items except for targeted object
            for (var i = 0; i < handObject.childCount; i++)
            {
                var child = handObject.GetChild(i).gameObject;
                var active = child.CompareTag(targetTag);
                child.SetActive(active);
                if (active) Debug.Log(child.name);
            }
            gameUI.SetCurrentObject(handObject);
            gameUI.BtnActive();
            if (firstCall)
            {
                firstCall = false;
                FindObjectOfType<ScoreCounter>().timerStart = true;
            }
        }
    }

    private void PlaceItem(RaycastHit hitInfo)
    {
        // remember to comment out for dev build
        if (gameManager.sceneIndex != 2) return;
        
        // if its not a bowl and if not holding anything, stop
        var target = hitInfo.collider.transform;
        if (target.tag.ToLower() != "bowl" || !holdingItem) return;
        holdingItem = false;
        
        
        Debug.Log(gameUI.objectInHand.transform.GetChild(0));
        Rigidbody item;
        var pos = target.position;
        item = Instantiate(gameUI.objectInHand.transform.GetChild(0).GetComponent<Rigidbody>(),
            new Vector3(pos.x, pos.y + 0.5f, pos.z), Quaternion.identity);
        item.isKinematic = false;
        gameUI.objectInHand.SetActive(false);
        gameUI.BtnDeactive();
    }

    #endregion

    #region Lucas

    #endregion
}