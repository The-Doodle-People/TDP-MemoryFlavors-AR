using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerMovementManager : MonoBehaviour
{

    public GameObject leftButton;
    public GameObject rightButton;
    private bool leftClick;

    public TextMeshProUGUI leftText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeftClick()
    {
        Debug.Log("Left button clicked");
        leftClick = true;
        leftText.text = "Left";
        
    }
}
