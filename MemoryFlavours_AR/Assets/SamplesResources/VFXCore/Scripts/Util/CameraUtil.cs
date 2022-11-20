/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// A utility class for Camera-related operations.
    /// </summary>
    public static class CameraUtil
    {
        /// <summary>
        /// Extracts the horizontal Field of View (in radians)
        /// from the actual camera projection matrix.
        /// </summary>
        /// <param name="camera">Currently used Camera</param>
        /// <returns>FoV in radians</returns>
        public static float GetHorizontalFovRadians(Camera camera)
        {
            return camera != null ? MatrixUtil.GetHorizontalFovRadians(camera.projectionMatrix) : 0;
        }

        /// <summary>
        /// Extracts the vertical Field of View (in radians)
        /// from the actual camera projection matrix.
        /// </summary>
        /// <param name="camera">Currently used Camera</param>
        /// <returns>FoV in radians</returns>
        public static float GetVerticalFovRadians(Camera camera)
        {
            return camera != null ? MatrixUtil.GetVerticalFovRadians(camera.projectionMatrix) : 0;
        }
    }
}