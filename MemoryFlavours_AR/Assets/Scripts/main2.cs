using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class main2 : MonoBehaviour
{

    public GameObject CurrentCube;
    public GameObject LastCube;
    public TextMeshProUGUI Text;
    public int Level;
    public bool Done;

    public GameOver GameOverScreen;
    int maxPlatform = 0;

    // Use this for initialization
    void Start()
    {
        newBlock();
    }

    private void newBlock()
    {
        
        if (LastCube != null)
        {
            CurrentCube.transform.position = new Vector3(Mathf.Round(CurrentCube.transform.position.x),
             CurrentCube.transform.position.y,
             Mathf.Round(CurrentCube.transform.position.z));
            CurrentCube.transform.localScale = new Vector3(LastCube.transform.localScale.x - Mathf.Abs(CurrentCube.transform.position.x - LastCube.transform.position.x),
                                                           LastCube.transform.localScale.y,
                                                           LastCube.transform.localScale.z - Mathf.Abs(CurrentCube.transform.position.z - LastCube.transform.position.z));
            CurrentCube.transform.position = Vector3.Lerp(CurrentCube.transform.position, LastCube.transform.position, 0.5f) + Vector3.up * 5f;
            if (CurrentCube.transform.localScale.x <= 0f ||
               CurrentCube.transform.localScale.z <= 0f)
            {
                Done = true;
                GameOverScreen.Setup(maxPlatform);
                Text.gameObject.SetActive(true);
                Text.text = "Your Score is " + Level;
                return;
                

            }

            GetComponent<AudioSource>().Play();

        }

        LastCube = CurrentCube;
        CurrentCube = Instantiate(LastCube);
        CurrentCube.name = Level + "";
        CurrentCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((Level / 100f) % 1f, 1f, 1f));
        Level++;
        Camera.main.transform.position = CurrentCube.transform.position + new Vector3(100, 70f, -100);
        Camera.main.transform.LookAt(CurrentCube.transform.position + Vector3.down * 30f);

       
    }

    // Update is called once per frame
    void Update()
    {
        if (Done)
            return;

        var time = Mathf.Abs(Time.realtimeSinceStartup % 2f - 1f);

        var pos1 = LastCube.transform.position + Vector3.up * 10f;
        var pos2 = pos1 + ((Level % 2 == 0) ? Vector3.left : Vector3.forward) * 120;

        if (Level % 2 == 0)
            CurrentCube.transform.position = Vector3.Lerp(pos2, pos1, time);
        else
            CurrentCube.transform.position = Vector3.Lerp(pos1, pos2, time);


        if (Input.GetMouseButtonDown(0))
            newBlock();




    }

   
}
