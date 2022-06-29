using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private WinUI winUI;
        [SerializeField] private LoseUI loseUI;
        [SerializeField] private GameObject overlay;

        private void Start()
        {
            overlay.SetActive(false);
            winUI.Hide();
            loseUI.Hide();

            GameManager.Instance.OnGameStart += OnGameStart;
            GameManager.Instance.OnGameEnd += OnGameEnd;
        }

        private void OnGameStart()
        {
            overlay.SetActive(false);
            winUI.Hide();
            loseUI.Hide();
        }

        private void OnGameEnd(bool won)
        {
            overlay.SetActive(true);
            Show(won);
        }

        public void Show(bool won)
        {
            if (won)
            {
                winUI.Show();
            }
            else
            {
                loseUI.Show();
            }
        }
    }
}
