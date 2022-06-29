using Cinemachine;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeeRun
{
    public class GameManager : MonoBehaviour
    {
        static private GameManager instance;

        static public GameManager Instance => instance;

        [SerializeField] private CinemachineVirtualCamera followCamera;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float startDelay;

        public event Action<CollectibleType> OnCollected;
        public event Action OnGameStart;
        public event Action<bool> OnGameEnd;

        public int CoinCount { get; private set; }
        public int FlowerCount { get; private set; }
        public GameState State { get; private set; } = GameState.Loading;

        private void Awake()
        {
            instance = this;
        }

        private async void Start()
        {
            await LoadAndStartGame();
        }

        private async Task LoadAndStartGame()
        {
            // Load level
            await LevelManager.Instance.LoadAsync("level_01", true);

            // Play music
            AudioManager.Instance.PlayMusic("music_game");

            // Wait start delay
            await Task.Delay((int)(startDelay * 1000));

            // Start run!
            UserManager.Instance.OnGameStarted();
            State = GameState.Started;
            OnGameStart?.Invoke();
            playerController.OnDeath += OnPlayerDeath;
            playerController.StartRun();
        }

        public void Collect(CollectibleType type, int amount = 1)
        {
            switch (type)
            {
                case CollectibleType.Coin:
                    AudioManager.Instance.PlaySound("collect_coin");
                    CoinCount += amount;
                    break;
                case CollectibleType.Flower:
                    AudioManager.Instance.PlaySound("collect_flower");
                    FlowerCount += amount;
                    break;
            }
            OnCollected?.Invoke(type);
        }

        private void OnPlayerDeath(ObstacleBehaviour behaviour)
        {
            if (State != GameState.Started) return;
            State = GameState.Ending;

            OnGameEnd?.Invoke(false);
        }

        public void ReachHive(Hive hive)
        {
            if (State != GameState.Started) return;
            State = GameState.Ending;
            FlowerCount = UnityEngine.Random.Range(0, 10); // temp
            followCamera.gameObject.SetActive(false);
            hive.StartEndAnimation(FlowerCount, playerController, OnEndAnimationComplete);
        }

        private void OnEndAnimationComplete()
        {
            float mult = GetCoinMultiplicator(FlowerCount);
            int newCount = Mathf.CeilToInt(CoinCount * mult);
            Collect(CollectibleType.Coin, newCount - CoinCount);

            OnGameEnd?.Invoke(true);
        }

        public void GameOver(bool won, GameEndAction endAction)
        {
            if (State != GameState.Ending) return;
            State = GameState.Over;

            Debug.Log($"GameManager - GameOver() won={won} endAction={endAction}");

            switch (endAction)
            {
                case GameEndAction.Exit:
                    UserManager.Instance.OnGameComplete(won, 1, CoinCount);
                    LevelManager.Instance.Load("home");
                    break;
                case GameEndAction.Retry:
                    UserManager.Instance.OnGameComplete(won, 1, CoinCount);
                    LevelManager.Instance.Restart();
                    break;
                case GameEndAction.Continue:
                    State = GameState.Started;
                    OnGameStart?.Invoke();
                    playerController.Continue();
                    break;
            }
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
            OnCollected = null;
            OnGameEnd = null;
            OnGameStart = null;
        }

        static public float GetCoinMultiplicator(int step)
        {
            return Mathf.Min(1f + step / 10f, 2f);
        }
    }

    [Serializable]
    public enum GameState
    {
        Loading,
        Ready,
        Started,
        Ending,
        Over
    }

    [Serializable]
    public enum GameEndAction
    {
        Exit,
        Retry,
        Continue
    }
}
