using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject scene1, scene2;

    int whichAvatarIsOn = 1;

    void Start()
    {
        scene1.gameObject.SetActive(true);
        scene2.gameObject.SetActive(false);
    }

    public void SwitchAvatar()
    {
        switch (whichAvatarIsOn)
        {

            case 1:

                whichAvatarIsOn = 2;

                scene1.gameObject.SetActive(false);
                scene2.gameObject.SetActive(true);
                break;

            case 2:

                whichAvatarIsOn = 1;

                scene1.gameObject.SetActive(true);
                scene2.gameObject.SetActive(false);
                break;
        }
    }
}