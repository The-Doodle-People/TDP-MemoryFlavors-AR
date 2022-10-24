/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatasetLoaderUI : MonoBehaviour
{
    public DatasetLoader Loader;
    public GameObject DatasetButtonPrefab;
    public Transform Panel;
    
    readonly Color32 SELECTED_COLOR = new Color32(78, 168, 68, 255);
    readonly Color32 UNSELECTED_COLOR = new Color32(74, 74, 74, 255);
    
    List<GameObject> mButtons = new List<GameObject>();
    int mActiveDataset = -1;
    
    public void LoadDataSetsAndCreateButtons()
    {
        ClearOldButtons();
        var datasetUnits = DirectoryScanner.GetDataSetsInFolder();
        for (int i = 0; i < datasetUnits.Count; i++)
        {
            var newButton = Instantiate(DatasetButtonPrefab, Panel);
            var index = i;
            var newButtonComponent = newButton.GetComponent<Button>();
            newButtonComponent.onClick.AddListener(() => Loader.TriggerAreaTargetLoading(datasetUnits[index]));
            newButtonComponent.onClick.AddListener(() => UpdateUI(index));
            newButton.transform.GetChild(0).GetComponent<Text>().text = datasetUnits[index].Name;
            mButtons.Add(newButton);
            
            // if it is the activated dataset we need to clear the UI
            if (i == mActiveDataset)
                mButtons[i].GetComponent<Image>().color = SELECTED_COLOR;
        }
    }

    void ClearOldButtons()
    {
        if (mButtons.Count > 0)
            for (int i = 0; i < mButtons.Count; i++)
                Destroy(mButtons[i]);
        mButtons = new List<GameObject>();
    }

    public void UpdateUI(int index)
    {
        // Do nothing if we already uninitialized
        if (mActiveDataset == -1 && index == -1)
            return;

        // Change the active button to inactive
        if (mActiveDataset == index && index != -1)
        {
            mButtons[index].GetComponent<Image>().color = UNSELECTED_COLOR;
            mActiveDataset = -1;
            return;
        }
        
        // Change the one button active and all others inactive
        for (int i = 0; i < mButtons.Count; i++)
        {
            if (i == index)
                mButtons[i].GetComponent<Image>().color = SELECTED_COLOR;
            else
                mButtons[i].GetComponent<Image>().color = UNSELECTED_COLOR;
        }

        mActiveDataset = index;
    }
}
