/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// Handles the UI on top of the VuMark content.
/// </summary>
public class VuMarkContentUI : MonoBehaviour
{
    CanvasGroup mCanvasGroup;
    Vector2 mFadeRange;
    Transform mCentralAnchorPointTransform;

    void Awake()
    {
        mCanvasGroup = GetComponentInChildren<CanvasGroup>();
        mFadeRange = VuforiaRuntimeUtilities.IsPlayMode() ? new Vector2(0.5f, 0.6f) : new Vector2(0.9f, 1.0f);

        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
    }

    void Update()
    {
        // Every frame, check target distance from camera and set alpha value of VuMark info canvas
        UpdateCanvasFadeAmount();
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
    }

    void OnVuforiaStarted()
    {
        mCentralAnchorPointTransform = VuforiaBehaviour.Instance.transform;
    }

    void UpdateCanvasFadeAmount()
    {
        if (mCentralAnchorPointTransform != null)
        {
            var distance = Vector3.Distance(mCentralAnchorPointTransform.position, transform.position);
            mCanvasGroup.alpha = 1 - Mathf.InverseLerp(mFadeRange.x, mFadeRange.y, distance);
        }
    }

    /// <summary>
    /// Called by DefaultObserverEventHandler.OnTargetFound.
    /// </summary>
    public void SetVuMarkInfoCanvas(VuMarkBehaviour vuMarkBehaviour)
    {
        var vuMarkDataType = VuMarkHandler.GetNumericVuMarkDescription(vuMarkBehaviour);
        var vuMarkInstanceTexture = VuMarkHandler.RetrieveStoredTextureForVuMarkTarget(vuMarkBehaviour);

        var vuMarkId = VuMarkUtilities.GetVuMarkId(vuMarkBehaviour);
        var vuMarkDesc = VuMarkUtilities.GetVuMarkDataType(vuMarkBehaviour);

        var canvasText = vuMarkBehaviour.GetComponentInChildren<Text>();
        var canvasImage = vuMarkBehaviour.GetComponentsInChildren<UnityEngine.UI.Image>()[2];
        var rect = new Rect(0, 0, vuMarkInstanceTexture.width, vuMarkInstanceTexture.height);

        canvasText.text =
            "<color=yellow>VuMark Instance Id: </color>" +
            "\n" + vuMarkId + " - " + vuMarkDesc +
            "\n\n<color=yellow>VuMark Type: </color>" +
            "\n" + vuMarkDataType;

        if (vuMarkInstanceTexture.width == 0 || vuMarkInstanceTexture.height == 0)
            canvasImage.sprite = null;
        else
            canvasImage.sprite = Sprite.Create(vuMarkInstanceTexture, rect, new Vector2(0.5f, 0.5f));
    }
}
