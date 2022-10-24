/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections;
using System.Linq;
using UnityEngine;
using Vuforia;

public class VuMarkUI : MonoBehaviour
{
    Camera mVuforiaCamera;
    VuMarkBehaviour mClosestVuMark;
    PanelShowHide mNearestVuMarkScreenPanel;

    void Start()
    {
        mVuforiaCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        mNearestVuMarkScreenPanel = FindObjectOfType<PanelShowHide>();
    }

    void Update()
    {
        UpdateClosestTarget();
    }

    void UpdateClosestTarget()
    {
        if (!VuforiaApplication.Instance.IsRunning || mVuforiaCamera == null)
            return;

        var currentVuMark = mClosestVuMark;
        mClosestVuMark = null;
        var closestDistance = Mathf.Infinity;

        foreach (var vuMarkBehaviour in VuforiaBehaviour.Instance.World.GetTrackedObserverBehaviours<VuMarkBehaviour>())
        {
            var distance = Vector3.Distance(vuMarkBehaviour.transform.position, mVuforiaCamera.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                mClosestVuMark = vuMarkBehaviour;
            }
        }

        if (mClosestVuMark != null && currentVuMark != mClosestVuMark)
        {
            var vuMarkId = VuMarkUtilities.GetVuMarkId(mClosestVuMark);
            var vuMarkDataType = VuMarkUtilities.GetVuMarkDataType(mClosestVuMark);
            var vuMarkImageTexture = VuMarkHandler.RetrieveStoredTextureForVuMarkTarget(mClosestVuMark);

            var rect = new Rect(0, 0, vuMarkImageTexture.width, vuMarkImageTexture.height);
            var vuMarkImage = Sprite.Create(vuMarkImageTexture, rect, new Vector2(0.5f, 0.5f));

            var vuMarkDesc = VuMarkHandler.GetNumericVuMarkDescription(mClosestVuMark);
            StartCoroutine(ShowPanelAfter(0.5f, vuMarkId, vuMarkDataType, vuMarkDesc, vuMarkImage));
        }
        else if (mClosestVuMark == null && mNearestVuMarkScreenPanel != null)
            mNearestVuMarkScreenPanel.ResetShowTrigger();
    }

    IEnumerator ShowPanelAfter(float seconds, string vuMarkId, string vuMarkDataType, string vuMarkDesc, Sprite vuMarkImage)
    {
        yield return new WaitForSeconds(seconds);

        if (mNearestVuMarkScreenPanel != null)
            mNearestVuMarkScreenPanel.Show(vuMarkId, vuMarkDataType, vuMarkDesc, vuMarkImage);
    }
}
