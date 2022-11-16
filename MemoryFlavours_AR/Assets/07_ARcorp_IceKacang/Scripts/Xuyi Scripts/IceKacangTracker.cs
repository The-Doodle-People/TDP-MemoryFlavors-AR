using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKacangTracker : MonoBehaviour
{
    public GameObject iceKacang;
    public Animator iceAnim;
    public Animator ingreAnim;
    public ParticleSystem snow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowFood()
    {
        iceAnim.SetBool("isIce", true);
        snow.Play();

    }
    public void HideFood()
    {
        iceAnim.SetBool("isIce", false);
    }

    public void ShowIngre()
    {
        ingreAnim.SetBool("isIngre", true);
    }

    public void HideIngre()
    {
        ingreAnim.SetBool("isIngre", false);
    }
}
