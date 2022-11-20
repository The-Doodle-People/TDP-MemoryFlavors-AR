/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.IO;
using UnityEngine;

namespace VFX
{
    /// <summary>
    /// Component to load a 3D texture. 
    /// </summary>
    public class VolumeTextureLoader : MonoBehaviour
    {
        [Header("File Settings")]
        public string FilePath = "SampleData/volume";
        public bool LoadOnAwake = true;
        public FilePathMode FilePathMode = FilePathMode.RESOURCES;

        [Header("Texture Size")]
        public int TextureWidth = 32;
        public int TextureHeight = 32;
        public int TextureDepth = 32;

        [Header("Visual Settings")] 
        public float MinValue;
        public float MaxValue = 1;
        public Color[] Colors =
        {
            Color.red,
            Color.green,
            Color.blue
        };
        
        Texture3D mTexture3D;

        void Awake()
        {
            if (LoadOnAwake)
                LoadTextureData();
        }

        private void OnDestroy()
        {
            if (mTexture3D)
            {
                Destroy(mTexture3D);
                mTexture3D = null;
            }
        }

        public void LoadTextureData()
        {
            var width = TextureWidth;
            var height = TextureHeight;
            var depth = TextureDepth;
            byte[] bytes = null;
            
            switch(FilePathMode)
            {
                case FilePathMode.ABSOLUTE_PATH:
                {
                    bytes = File.ReadAllBytes(FilePath);
                    break;
                }
                case FilePathMode.PERSISTENT_DATA_PATH:
                {
                    bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, FilePath));
                    break;
                }
                case FilePathMode.RESOURCES:
                {
                    bytes = LoadFromResources(FilePath);
                    break;
                }
            }
            
            if (bytes == null)
            {
                Debug.LogError("Failed to load texture data.");
                return;
            }

            mTexture3D = new Texture3D(width, height, depth, TextureFormat.RGB24, false);
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var index = height * width * z + width * y + x;
                        var value = System.BitConverter.ToSingle(bytes, 4 * index);
                        var color = ValueToColor(value);
                        mTexture3D.SetPixel(x, y, z, color);
                    }
                }
            }
            
            mTexture3D.Apply();

            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var aRenderer in renderers)
                aRenderer.sharedMaterial.mainTexture = mTexture3D;
        }
        
        byte[] LoadFromResources(string resourceFileName)
        {
            var asset = Resources.Load(resourceFileName) as TextAsset;
            if (asset == null)
            {
                Debug.Log("Could not load resource " + resourceFileName);
                return null;
            }
            return asset.bytes;
        }

        Color ValueToColor(float value)
        {
            if (Colors == null || Colors.Length == 0 || MaxValue.Equals(MinValue))
                return Color.gray;

            if (Colors.Length == 1)
                return Colors[0];

            // With 2 or more colors, interpolate
            var numSegments = Colors.Length - 1;
            var segmentLength = 1.0f / numSegments;
            
            var clampedValue = Mathf.Clamp01((value - MinValue) / (MaxValue - MinValue));
            var index = (int)Mathf.Floor(clampedValue * numSegments);
            var color0 = Colors[index];
            var color1 = (index < Colors.Length - 1) ? Colors[index + 1] : Colors[index];
            var lerpValue = (clampedValue - (index * segmentLength)) / segmentLength;
            return Color.Lerp(color0, color1, lerpValue);
        }
    }
}
