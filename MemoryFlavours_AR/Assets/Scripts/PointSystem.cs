using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    /// <summary>
    /// Count and check questions
    /// </summary>
    private int stageNum = 1;

    /// <summary>
    /// Count number of points earned
    /// </summary>
    private int countPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrataIngredients()
    {
        if (stageNum <= 4)
        {
            countPoints += 1;
            stageNum += 1;
        }
        
    }

    public void KuehIngredients()
    {
        if (stageNum <= 4)
        {
            countPoints += 2;
            stageNum += 1;
        }
    }

    public void MooncakeIngredients()
    {
        if (stageNum <= 4)
        {
            countPoints += 3;
            stageNum += 1;
        }
    }

    public void ReplayGame()
    {
        stageNum = 1;
        countPoints = 0;
    }
}
