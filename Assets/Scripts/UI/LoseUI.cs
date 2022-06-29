using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class LoseUI : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClickContinue()
        {
            Hide();
            GameManager.Instance.GameOver(false, GameEndAction.Continue);
        }

        public void OnClickRetry()
        {
            Hide();
            GameManager.Instance.GameOver(false, GameEndAction.Retry);
        }
    }
}
