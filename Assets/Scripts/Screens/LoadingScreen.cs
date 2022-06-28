using System.Threading.Tasks;
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
            LoadGame();
        }

        private async Task LoadGame()
        {
            SetProgress(0f);
            await UserManager.Instance.Initialize();
            SetProgress(0.1f);

            UserManager.Instance.OnAppLaunched();

            // fake progress
            while (progress < 1f)
            {
                SetProgress(progress + Time.deltaTime);
                await Task.Yield();
            }
            SetProgress(1f);

            OnLoadingComplete();
        }

        private void OnLoadingComplete()
        {
            loadingUI.SetActive(false);
            loadedUI.SetActive(true);
        }

        private void Update()
        {
            if (progress >= 1f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LevelManager.Instance.Load("home");
                }
            }
        }

        private void SetProgress(float progress)
        {
            this.progress = progress;
            progressImage.fillAmount = Mathf.Clamp01(progress);
        }
    }
}
