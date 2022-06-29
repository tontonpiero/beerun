using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeeRun
{
    public class WinUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text textCoinsAmount;

        private int totalCoins = 0;

        public void Show()
        {
            gameObject.SetActive(true);

            totalCoins = GameManager.Instance.CoinCount;
            StartCounterAnim();
        }

        private void StartCounterAnim()
        {
            UpdateCounter(0);
            Hashtable ht = iTween.Hash("from", 0, "to", totalCoins, "time", 1f, "delay", 0.5f, "onupdate", "UpdateCounter");
            iTween.ValueTo(gameObject, ht);
        }

        private void UpdateCounter(int value)
        {
            textCoinsAmount.text = value.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClickDoubleReward()
        {
            Hide();

            // TODO: show ad
            OnAdShown(true);
        }

        public void OnClickSkip()
        {
            Hide();
            OnComplete();
        }

        private void OnAdShown(bool success)
        {
            if (success)
            {
                GameManager.Instance.Collect(CollectibleType.Coin, totalCoins);
            }
            OnComplete();
        }

        private void OnComplete()
        {
            GameManager.Instance.GameOver(true, GameEndAction.Exit);
        }
    }
}
