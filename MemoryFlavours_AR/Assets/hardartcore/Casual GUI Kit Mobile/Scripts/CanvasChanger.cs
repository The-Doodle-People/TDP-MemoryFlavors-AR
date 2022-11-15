using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace hardartcore.CasualGUI
{
    public class CanvasChanger : MonoBehaviour
    {

        private WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);
        public void ShowCanvas(CanvasGroup canvasGroup)
        {
            StartCoroutine(ProcessCanvasGroup(canvasGroup, 0f, 1f, true));
        }

        public void HideCanvas(CanvasGroup canvasGroup)
        {
            StartCoroutine(ProcessCanvasGroup(canvasGroup, 1f, 0f, false));
        }

        private IEnumerator ProcessCanvasGroup(CanvasGroup canvasGroup, float initAlpha, float alpha, bool enable)
        {
            canvasGroup.alpha = initAlpha;
            if (enable) canvasGroup.gameObject.SetActive(true);
            yield return waitForSeconds;
            canvasGroup.DOFade(alpha, 0.3f);
            yield return waitForSeconds;
            canvasGroup.gameObject.SetActive(enable);
        }
    }
}
