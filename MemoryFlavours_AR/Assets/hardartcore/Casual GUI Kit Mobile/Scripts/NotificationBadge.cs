//This asset was uploaded by https://unityassetcollection.com


using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace hardartcore.CasualGUI
{
    public class NotificationBadge : MonoBehaviour
    {

        public float showAfter = 2f;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            StartCoroutine(StartAfter());
        }

        private IEnumerator StartAfter()
        {
            yield return new WaitForSeconds(showAfter);
            transform.DOScale(1f, 0.3f);
        }

    }
}