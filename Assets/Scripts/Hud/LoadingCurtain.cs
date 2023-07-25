using System.Collections;
using UnityEngine;

namespace Hud
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private float _curtainAlphaFade = 0.03f;
        [SerializeField] private float _timeToFade = 0.03f;
        public CanvasGroup Curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= _curtainAlphaFade;
                yield return new WaitForSeconds(_timeToFade);
            }

            gameObject.SetActive(false);
        }
    }
}