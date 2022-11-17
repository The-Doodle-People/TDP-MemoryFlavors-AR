using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collector : MonoBehaviour
{

    

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "DimSum")
        {
            Destroy(target.gameObject);

        }
    }

    
    
}
