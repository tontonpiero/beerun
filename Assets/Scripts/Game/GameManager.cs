using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeeRun
{
    public class GameManager : MonoBehaviour
    {
        static private GameManager instance;

        static public GameManager Instance => instance;

        [SerializeField] private PlayerController playerController;
        [SerializeField] private float startDelay;

        public event Action<CollectibleType> OnCollected;

        public int CoinCount { get; private set; }
        public int FlowerCount { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            AudioManager.Instance.PlayMusic("music_game");

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(startDelay);
            playerController.StartRun();
            UserManager.Instance.OnGameStarted();
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

        public void GameOver(bool won)
        {
            UserManager.Instance.OnGameComplete(won, 1, CoinCount);
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
            OnCollected = null;
        }
    }
}
