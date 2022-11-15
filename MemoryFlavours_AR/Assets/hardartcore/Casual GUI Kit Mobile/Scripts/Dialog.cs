//This asset was uploaded by https://unityassetcollection.com

using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace hardartcore.CasualGUI
{

    public class Dialog : MonoBehaviour
    {
        public float animDuration = 0.2f;
        public GameObject dialogContent;

        public void ShowDialog()
        {
            dialogContent.SetActive(false);
            gameObject.SetActive(true);
            dialogContent.transform.localScale = Vector3.zero;
            dialogContent.SetActive(true);
            dialogContent.transform.DOScale(Vector3.one, animDuration);
        }

        public void HideDialog()
        {
            dialogContent.transform.DOScale(Vector3.zero, animDuration);
            StartCoroutine(HideDialogAfterTime());
        }

        private IEnumerator HideDialogAfterTime()
        {
            yield return new WaitForSeconds(animDuration);
            gameObject.SetActive(false);
        }
    }
}
