using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticles : MonoBehaviour
{
    public ParticleSystem[] starParticles;


    public void StopParticles()
    {
        for (int i = 0; i <= 7; i++)
        {
            starParticles[i].Stop();
        }
    }
}
