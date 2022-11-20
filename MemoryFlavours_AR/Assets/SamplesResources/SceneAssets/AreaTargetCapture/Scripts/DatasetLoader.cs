/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.IO;
using UnityEngine;
using Vuforia;

public class DatasetLoader : MonoBehaviour
{
    public Material RenderingMaterial;
    public bool IsColliderOn;
    
    bool mIsOcclusionOn = true;
    DatasetUnit mLoadedDataset;
    GameObject mModel;
    AreaTargetBehaviour mAreaTargetBehaviour;
    
    const string EXTENSION_FORMAT_XML = "*.xml";
    const string EXTENSION_FORMAT_GLB = "*.glb";

    internal void TriggerAreaTargetLoading(DatasetUnit datasetUnit)
    {
        if (mLoadedDataset != null)
        {
            var loadedDatasetName = mLoadedDataset.Name;
            UnloadAreaTarget();
            // If it is the same dataset, then we don't load it again.
            if (loadedDatasetName.Equals(datasetUnit.Name))
                return;
        }
        LoadAreaTarget(datasetUnit);
    }

    public void UnloadAreaTarget()
    {
        mLoadedDataset = null;
        if (mAreaTargetBehaviour != null)
        {
            mAreaTargetBehaviour.enabled = false;
            Destroy(mAreaTargetBehaviour.gameObject);
            if(mModel != null)
                Destroy(mModel);
        }
    }

    void LoadAreaTarget(DatasetUnit mDataSetUnit)
    {
        var pathToTheXML = Path.Combine(mDataSetUnit.Path, mDataSetUnit.Name + EXTENSION_FORMAT_XML.Remove(0, 1));
        mAreaTargetBehaviour = VuforiaBehaviour.Instance.ObserverFactory.CreateAreaTarget(pathToTheXML, mDataSetUnit.Name, false);
        if (mAreaTargetBehaviour != null)
        {
            var augmentationFile = DirectoryScanner.ScanFor3dtFile(mDataSetUnit.Path, mDataSetUnit.Name);
            if (augmentationFile == null)
                augmentationFile = DirectoryScanner.FindFileWithExtension(mDataSetUnit.Path, EXTENSION_FORMAT_GLB);
            var datasetFullPath = Path.Combine(mDataSetUnit.Path, augmentationFile);
            datasetFullPath = datasetFullPath.Replace("\\", "/");
            mModel = LoadDatasetModel(mAreaTargetBehaviour, datasetFullPath);
            mLoadedDataset = mDataSetUnit;
            mModel.transform.SetParent(mAreaTargetBehaviour.transform);
            mModel.transform.position = Vector3.zero;
            mModel.transform.rotation = Quaternion.identity;
        }
        else
            Debug.Log("Target hasn't been found!");
    }
    
    GameObject LoadDatasetModel(AreaTargetBehaviour areaTarget, string pathToFile)
    {
        GameObject model = null;
            
        var eventHandler = areaTarget.GetComponent<DefaultAreaTargetEventHandler>();
        if (!eventHandler)
            areaTarget.gameObject.AddComponent<DefaultAreaTargetEventHandler>();
    
        model = LoadMeshSetGameObject(pathToFile, areaTarget.TargetName, areaTarget.gameObject.transform);
        return model;
    }
    
    GameObject LoadMeshSetGameObject(string path, string areaTargetName, Transform parentTransform)
    {
        return MeshSetModelCreator.LoadMeshSet(areaTargetName, path, parentTransform, StorageType.ABSOLUTE,
            IsColliderOn, mIsOcclusionOn, RenderingMaterial);
    }
}
