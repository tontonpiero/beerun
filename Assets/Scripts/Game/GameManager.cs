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
            playerController.StartRun();
        }

        public void Collect(CollectibleType type)
        {
            switch (type)
            {
                case CollectibleType.Coin:
                    AudioManager.Instance.PlaySound("collect_coin");
                    CoinCount++;
                    break;
                case CollectibleType.Flower:
                    AudioManager.Instance.PlaySound("collect_flower");
                    FlowerCount++;
                    break;
            }
            OnCollected?.Invoke(type);
        }

        public void ReachHive(Hive hive)
        {
            State = GameState.Ending;
            FlowerCount = UnityEngine.Random.Range(0, 10); // temp
            followCamera.gameObject.SetActive(false);
            hive.StartEndAnimation(FlowerCount, playerController, OnEndAnimationComplete);
        }

        private void OnEndAnimationComplete()
        {
            float mult = GetCoinMultiplicator(FlowerCount);
            CoinCount = Mathf.CeilToInt(CoinCount * mult);
            OnCollected?.Invoke(CollectibleType.Coin);
        }

        public void GameOver(bool won)
        {
            State = GameState.Over;
            UserManager.Instance.OnGameComplete(won, 1, CoinCount);
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
            OnCollected = null;
        }

        static public float GetCoinMultiplicator(int step)
        {
            return Mathf.Min(1f + step / 10f, 2f);
        }
    }

    public enum GameState
    {
        Loading,
        Ready,
        Started,
        Ending,
        Over
    }
}
