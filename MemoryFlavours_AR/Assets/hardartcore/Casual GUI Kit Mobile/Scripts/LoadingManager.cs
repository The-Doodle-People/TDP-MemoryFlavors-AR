//This asset was uploaded by https://unityassetcollection.com

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace hardartcore.CasualGUI
{
    public class LoadingManager : MonoBehaviour
    {

        public static LoadingManager Instance { get; private set; }

        public float fadeDuration = 1f;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(Instance);
                    Instance = this;
                    DontDestroyOnLoad(this);
                }
            }
        }

        public void LoadScene(string level)
        {
            StartFade(level);
        }

        private void StartFade(string level)
        {
            StartCoroutine(Fade(level));
        }

        private IEnumerator Fade(string level)
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.gameObject.SetActive(true);

            var halfDuration = fadeDuration / 2.0f;

            canvasGroup.DOFade(1f, halfDuration);
            yield return new WaitForSeconds(fadeDuration);

            SceneManager.LoadScene(level);

            canvasGroup.DOFade(0f, halfDuration);
            yield return new WaitForSeconds(fadeDuration);
            canvasGroup.gameObject.SetActive(false);

        }
    }
}