using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Seeker : MonoBehaviour, IDragHandler , IPointerDownHandler
{
    [SerializeField]   
    private VideoPlayer clipPlayer;

    private Image progress;

    public Camera ARCam;

    private void Awake()
    {
        clipPlayer = clipPlayer.GetComponent<VideoPlayer>();
        progress = GetComponent<Image>();
    }

    private void Update()
    {
        if (clipPlayer.frameCount > 0)
        {
            progress.fillAmount = (float)clipPlayer.frame / (float)clipPlayer.frameCount;
            
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(progress.rectTransform, eventData.position, ARCam, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin,
                progress.rectTransform.rect.xMax, localPoint.x);

            SkipToPercent(pct);
        }
    }

    private void SkipToPercent(float pct)
    {
        var frame = clipPlayer.frameCount * pct;
        clipPlayer.frame = (long)frame;
    }
}

/* Credits www.youtube.com/watch?v=9LwOmMzOrp4 */
