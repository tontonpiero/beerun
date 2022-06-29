using BeautifulTransitions.Scripts.Transitions.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeeRun
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        static private LevelManager instance;

        static public LevelManager Instance => instance;

        [SerializeField] private List<Level> levels;
        [SerializeField] private TransitionBase fadeTransition;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            fadeTransition.TransitionIn();
        }

        public void Load(string name, bool additive = false)
        {
            Task.Run(() => LoadAsync(name, additive));
        }

        public async Task LoadAsync(string name, bool additive = false)
        {
            Level level = levels.FirstOrDefault(l => l.Name == name);
            Debug.Log($"LevelManager - Load() {level}");
            if (level != null)
            {
                if (!SceneManager.GetSceneByName(level.Scene).isLoaded)
                {
                    await LoadSceneAsync(level.Scene, additive);
                }
            }
        }

        private async Task LoadSceneAsync(string sceneName, bool additive)
        {
            if (!additive)
            {
                fadeTransition.TransitionOut();
                float delay = fadeTransition.TransitionOutConfig.Delay + fadeTransition.TransitionOutConfig.Duration;
                await Task.Delay((int)(delay * 1000));
            }

            // Load scene
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            while (!op.isDone)
            {
                await Task.Yield();
            }
        }

        public void Restart()
        {
            Task.Run(() => RestartAsync());
        }

        public async Task RestartAsync()
        {
            await LoadSceneAsync(SceneManager.GetActiveScene().name, false);
        }
    }
}
