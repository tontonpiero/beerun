using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeeRun
{
    public class GameManager : MonoBehaviour
    {
        static private GameManager instance;

        static public GameManager Instance => instance;

        [SerializeField] private PlayerController playerController;

        public event Action<CollectibleType> OnCollected;

        public int CoinCount { get; private set; }
        public int FlowerCount { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            AudioManager.Instance.PlayMusic("music_01");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySound("click_01");
            }
            if (Input.GetMouseButtonDown(1))
            {
                LevelManager.Instance.Restart();
            }
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

        private void OnDestroy()
        {
            if (instance == this) instance = null;
            OnCollected = null;
        }
    }
}
