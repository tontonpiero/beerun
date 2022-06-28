using UnityEngine;
using UnityEngine.UI;

namespace BeeRun
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private GameObject loadingUI;
        [SerializeField] private GameObject loadedUI;

        private float progress = 0f;

        private void Start()
        {
            loadingUI.SetActive(true);
            loadedUI.SetActive(false);
            UpdateProgressBar(0f);
        }

        private void Update()
        {
            if (progress < 1f)
            {
                progress += Time.deltaTime;
                UpdateProgressBar(progress);
                if (progress >= 1f)
                {
                    loadingUI.SetActive(false);
                    loadedUI.SetActive(true);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LevelManager.Instance.Load("home");
                }
            }
        }

        private void UpdateProgressBar(float progress)
        {
            progressImage.fillAmount = Mathf.Clamp01(progress);
        }
    }
}
