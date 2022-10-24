/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

public class SimpleNavigator : MonoBehaviour
{
    public VFXScenes TargetScene;
    
    public void Navigate()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)TargetScene);
    }
}