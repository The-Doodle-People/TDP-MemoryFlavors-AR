using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace hardartcore.CasualGUI
{
    public class FadeItem : MonoBehaviour
    {
        public float startAnimationAfter = 0.3f;
        public float animationDuration = 0.3f;
        public CanvasGroup canvasGroup;

        private void OnEnable()
        {
            SetBackgroundColorAlpha();
            StartCoroutine(FadeInAfter());
        }

        private void OnDisable()
        {
            SetBackgroundColorAlpha();
        }

        private IEnumerator FadeInAfter()
        {
            yield return new WaitForSeconds(startAnimationAfter);
            canvasGroup.DOFade(1f, animationDuration);
        }

        private void SetBackgroundColorAlpha()
        {
            canvasGroup.alpha = 0;
        }

    }
}