using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/musttryfood1432/");
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100087950293911");
    }
}
