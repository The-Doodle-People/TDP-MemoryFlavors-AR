/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    public class ObjectSwitch : MonoBehaviour
    {
        public void EnableChild(int index)
        {
            if (index < 0 || index >= transform.childCount)
                return;

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(i == index);
            }
        }
    }
}