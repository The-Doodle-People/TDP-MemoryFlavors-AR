using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [HideInInspector] public ShelfItems shelfItems;
    [HideInInspector] public TouchHandler touchHandler;

    public GameObject button;
    public GameObject objectInHand;
    
    private void Start()
    {
        shelfItems = FindObjectOfType<ShelfItems>();
        touchHandler = FindObjectOfType<TouchHandler>();
        touchHandler.gameUI = this;
    }

    public void SetCurrentObject(Transform handObject)
    {
        for (var i = 0; i < handObject.childCount; i++)
        {
            // if I'm not active skip me
            if (!handObject.GetChild(i).gameObject.activeSelf) continue;
            objectInHand = handObject.GetChild(i).gameObject;
            break;
        }
    }

    public void DropItem()
    {
        // turn off holding item related code
        objectInHand.SetActive(false);
        touchHandler.holdingItem = false;

        Instantiate(objectInHand, touchHandler.camera.transform.position, Quaternion.identity);
        BtnDeactive();
    }

    public void BtnActive()
    {
        button.SetActive(true);
    }
    
    public void BtnDeactive()
    {
        button.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Return()
    {
        FindObjectOfType<GameManager>().sceneIndex = 0;
    }
}
