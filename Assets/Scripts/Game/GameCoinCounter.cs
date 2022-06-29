using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeeRun
{
    public class GameCoinCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text textCounter;

        private void Start()
        {
            GameManager.Instance.OnCollected += OnCollectedSomething;
            UpdateCounter();
        }

        private void OnCollectedSomething(CollectibleType type, int amount)
        {
            if (type == CollectibleType.Coin) UpdateCounter();
        }

        private void UpdateCounter()
        {
            textCounter.text = $"+{GameManager.Instance.CoinCount}";
        }
    }
}
