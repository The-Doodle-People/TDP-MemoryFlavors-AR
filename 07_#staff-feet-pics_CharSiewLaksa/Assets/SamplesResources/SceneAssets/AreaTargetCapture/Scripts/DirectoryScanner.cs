/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DirectoryScanner
{
    const string DATA_FOLDER_NAME = "Data";
    const string OCCLUSION_POST_FIX = "_occlusion.3dt";
    const string EXTENSION_FORMAT_XML = "*.xml";
    const string EXTENSION_FORMAT_DAT = "*.dat";
    const string EXTENSION_FORMAT_3DT = "*.3dt";
    const string EXTENSION_FORMAT_GLB = "*.glb";
    
    internal static List<DatasetUnit> GetDataSetsInFolder()
    {
        // Clear previous file list
        var dataSetUnits = new List<DatasetUnit>();
        // Get the base dir (SD card on Android, or App /Documents folder on iOS)
        var storageRoot = GetStorageRoot("AreaTargetData");
        var scanDir = new DirectoryInfo(storageRoot);
        
        if (!scanDir.Exists)
        {
            scanDir.Create();
            return null;
        }
                
        var dirs = scanDir.GetDirectories();
        foreach (var directoryInfo in dirs)
        {
            var datasetFileXML = FindFileWithExtension(directoryInfo.FullName, EXTENSION_FORMAT_XML);
            var datasetFileDat = FindFileWithExtension(directoryInfo.FullName, EXTENSION_FORMAT_DAT);
            var datasetFileOcclusionModel = ScanFor3dtFile(directoryInfo.FullName, directoryInfo.Name);
            var datasetFileGLB = FindFileWithExtension(directoryInfo.FullName, EXTENSION_FORMAT_GLB);
            // Only display datasets that have both an .xml and .dat file
            if (datasetFileDat != null && datasetFileXML != null && (datasetFileOcclusionModel != null || datasetFileGLB != null))
                // Keep track of full path by directory name
                dataSetUnits.Add(new DatasetUnit()
                {
                    Name = directoryInfo.Name, Path = directoryInfo.FullName
                });
            else 
                Debug.LogError("Could not find dataset files in directory " + directoryInfo.Name);
        }
        return dataSetUnits;
    }
        
    static string GetStorageRoot(string folderName)
    {
        if (Application.isEditor)
            return Path.Combine(Application.dataPath, DATA_FOLDER_NAME, folderName);
        
        // On Android, Application.persistentDataPath should look like: '/sdcard/Android/data/com.myorg.myapp/files'
        // On iOS, Application.persistentDataPath points to the 'Documents' folder of the App
        return Path.Combine(Application.persistentDataPath, folderName);
    }
            
    internal static string ScanFor3dtFile(string fileFullName, string fileName)
    {
        // Searching for the [[TARGET_NAME]]_occlusion.3dt
        var datasetFileOcclusionModel = FindFile(fileFullName, fileName + OCCLUSION_POST_FIX);
        if (!string.IsNullOrEmpty(datasetFileOcclusionModel))
            return datasetFileOcclusionModel;
        // Scan through all 3dts and select _occlusion.3dt if possible
        var allFilesWithExtension = FindFilesWithExtension(fileFullName, EXTENSION_FORMAT_3DT);
        if (allFilesWithExtension != null && allFilesWithExtension.Length > 0)
        {
            for (int i = 0; i < allFilesWithExtension.Length; i++)
                if (allFilesWithExtension[i].Contains(OCCLUSION_POST_FIX))
                    return allFilesWithExtension[i];
        }
        else
            return string.Empty;
        return allFilesWithExtension[0];
    }
            
    internal static string FindFileWithExtension(string dirPath, string extension)
    {
        if (!Directory.Exists(dirPath))
            return null;
        
        var files = new DirectoryInfo(dirPath).GetFiles(extension, SearchOption.AllDirectories);
        if (files.Length == 0)
            return null;
        return files[0].Name;
    }
            
    static string FindFile(string dirPath, string fileName)
    {
        if (!Directory.Exists(dirPath))
            return null;
                 
        var files = new DirectoryInfo(dirPath).GetFiles();
        for (int i = 0; i < files.Length; i++)
            if (files[i].Name == fileName)
                return files[i].Name;
                    
        return null;
    }
            
    static string[] FindFilesWithExtension(string fileFullName, string extensionFormat3Dt) 
    {
        if (!Directory.Exists(fileFullName))
            return null;
        
        var files = new DirectoryInfo(fileFullName).GetFiles(extensionFormat3Dt, SearchOption.AllDirectories);
        if (files.Length == 0)
            return null;
        var filePaths = new string[files.Length];
        for (int i = 0; i < filePaths.Length; i++)
            filePaths[i] = files[i].Name;
                 
        return filePaths;
    }
}