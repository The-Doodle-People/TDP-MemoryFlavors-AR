/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

namespace VFX
{
    /// <summary>
    /// Utility class for Vuforia Observers.
    /// </summary>
    public static class VuforiaObserverUtil
    {
        /// <summary>
        /// Returns the 3D size of a Vuforia Observer.
        /// </summary>
        /// <param name="observer">Given observer of any type</param>
        /// <returns>Size of the given observer</returns>
        public static Vector3 GetTargetSize(ObserverBehaviour observer)
        {
            switch (observer)
            {
                case ImageTargetBehaviour imageTarget:
                    return imageTarget.GetSize();
                case CylinderTargetBehaviour cylinderTarget:
                {
                    var vertSize = cylinderTarget.SideLength;
                    var horizSize = Mathf.Max(cylinderTarget.BottomDiameter, cylinderTarget.TopDiameter);
                    return new Vector3(horizSize, vertSize, horizSize);
                }
                case ModelTargetBehaviour modelTarget:
                    return modelTarget.GetBoundingBox().size;
                case AreaTargetBehaviour areaTarget:
                    return areaTarget.GetBoundingBox().size;
                case VuMarkBehaviour vuMark:
                    return vuMark.GetSize();
                default:
                    return Vector3.one;
            }
        }
    }
}
