/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

namespace VFX
{
    [System.Serializable]
    public struct DynamicScalarProperty
    {
        public string name;
        public float startValue;
        public float endValue;
        public float delay;
        public float duration;
    }
}