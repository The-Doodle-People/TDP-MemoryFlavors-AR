/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour
{
    void Awake()
    {
#if !(UNITY_IOS || UNITY_EDITOR_OSX)
        gameObject.SetActive(false);
#endif
    }
}
