/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// A utility class for matrix operations.
    /// </summary>
    public static class MatrixUtil
    {
        public static Matrix4x4 MatrixFromArray(float[] array)
        {
            var matrix = new Matrix4x4();
            matrix.m00 = array[0];
            matrix.m10 = array[1];
            matrix.m20 = array[2];
            matrix.m30 = array[3];
            matrix.m01 = array[4];
            matrix.m11 = array[5];
            matrix.m21 = array[6];
            matrix.m31 = array[7];
            matrix.m02 = array[8];
            matrix.m12 = array[9];
            matrix.m22 = array[10];
            matrix.m32 = array[11];
            matrix.m03 = array[12];
            matrix.m13 = array[13];
            matrix.m23 = array[14];
            matrix.m33 = array[15];
            return matrix;
        }

        /// <summary>
        /// Computes the transformation matrix from one Transform reference frame
        /// to another Transform reference frame.
        /// </summary>
        /// <param name="from">The Transform from which the calculation happens</param>
        /// <param name="to">The Transform to which the calculation happens</param>
        /// <returns>The matrix difference between the two Transforms</returns>
        public static Matrix4x4 GetFromToMatrix(Transform from, Transform to)
        {
            var m1 = from ? from.localToWorldMatrix : Matrix4x4.identity;
            var m2 = to ? to.worldToLocalMatrix : Matrix4x4.identity;
            return m2 * m1;
        }

        /// <summary>
        /// Extracts the rotation component from a transform matrix
        /// </summary>
        /// <param name="matrix">Matrix from which we get the Quaternion</param>
        /// <returns>Rotation read from the matrix</returns>
        public static Quaternion GetRotation(Matrix4x4 matrix)
        {
            return Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
        }

        /// <summary>
        /// Extracts the translation (position) component from a transform matrix
        /// </summary>
        /// <param name="matrix">Matrix from which we get the Position</param>
        /// <returns>Position read from the matrix</returns>
        public static Vector3 GetPosition(Matrix4x4 matrix)
        {
            var x = matrix.m03;
            var y = matrix.m13;
            var z = matrix.m23;
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Extracts the scale component from a transform matrix
        /// </summary>
        /// <param name="matrix">Matrix from which we get the Scale</param>
        /// <returns>Scale read from the matrix</returns>
        public static Vector3 GetScale(Matrix4x4 matrix)
        {
            // Note: we need to swap Y and Z because the GLTF uses a Right-handed coordinate system
            var x = Mathf.Sqrt(matrix.m00 * matrix.m00 + matrix.m01 * matrix.m01 + matrix.m02 * matrix.m02);
            var z = Mathf.Sqrt(matrix.m10 * matrix.m10 + matrix.m11 * matrix.m11 + matrix.m12 * matrix.m12);
            var y = Mathf.Sqrt(matrix.m20 * matrix.m20 + matrix.m21 * matrix.m21 + matrix.m22 * matrix.m22);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Extracts the horizontal field of view (in radians)
        /// from a given projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">Projection Matrix from which the FoV is extracted</param>
        /// <returns>FoV in radians</returns>
        public static float GetHorizontalFovRadians(Matrix4x4 projectionMatrix)
        {
            return 2 * Mathf.Atan(1.0f / projectionMatrix[0]);
        }

        /// <summary>
        /// Extracts the horizontal field of view (in degrees)
        /// from a given projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">Projection Matrix from which the FoV is extracted</param>
        /// <returns>FoV in Degrees</returns>
        public static float GetHorizontalFovDegrees(Matrix4x4 projectionMatrix)
        {
            return GetHorizontalFovRadians(projectionMatrix) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Extracts the vertical field of view (in radians)
        /// from a given projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">Projection Matrix from which the FoV is extracted</param>
        /// <returns>FoV in radians</returns>
        public static float GetVerticalFovRadians(Matrix4x4 projectionMatrix)
        {
            return 2 * Mathf.Atan(1.0f / projectionMatrix[5]);
        }

        /// <summary>
        /// Extracts the vertical field of view (in degrees)
        /// from a given projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">Projection Matrix from which the FoV is extracted</param>
        /// <returns>Fov in Degrees</returns>
        public static float GetVerticalFovDegrees(Matrix4x4 projectionMatrix)
        {
            return GetVerticalFovRadians(projectionMatrix) * Mathf.Rad2Deg;
        }
    }
}