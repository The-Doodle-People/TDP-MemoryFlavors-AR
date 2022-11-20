/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct DynamicColor
    {
        public string name;
        public Color startColor;
        public Color endColor;
        public float delay;
        public float duration;
    }
}