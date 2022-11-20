/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using Vuforia;

public static class VuMarkUtilities
{
    public static string GetVuMarkId(VuMarkBehaviour vuMarkTarget)
    {
        switch (vuMarkTarget.InstanceId.DataType)
        {
            case InstanceIdType.BYTE:
                return vuMarkTarget.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return vuMarkTarget.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return vuMarkTarget.InstanceId.NumericValue.ToString();
        }
        return string.Empty;
    }

    public static string GetVuMarkDataType(VuMarkBehaviour vuMarkTarget)
    {
        switch (vuMarkTarget.InstanceId.DataType)
        {
            case InstanceIdType.BYTE:
                return "Bytes";
            case InstanceIdType.STRING:
                return "String";
            case InstanceIdType.NUMERIC:
                return "Numeric";
        }
        return string.Empty;
    }

    /// <summary>
    /// Generates a texture from VuMark instance image. Does not flips Y axis.
    /// </summary>
    /// <param name="vuMarkTarget"></param>
    /// <returns></returns>
    public static Texture2D GenerateTextureFromVuMarkInstanceImage(VuMarkBehaviour vuMarkTarget)
    {
        Debug.Log("<color=cyan>SaveImageAsTexture() called.</color>");

        if (vuMarkTarget.InstanceImage == null)
        {
            Debug.Log("VuMark Instance Image is null.");
            return null;
        }
        Debug.Log(vuMarkTarget.InstanceImage.Width + ", " + vuMarkTarget.InstanceImage.Height);

        var texture = new Texture2D(vuMarkTarget.InstanceImage.Width, vuMarkTarget.InstanceImage.Height, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp
        };
        // do not flip Y. Alternatively, flipY can be ignored and object(canvas) itself can be flipped.
        vuMarkTarget.InstanceImage.CopyToTexture(texture, false);
        return texture;
    }


}
