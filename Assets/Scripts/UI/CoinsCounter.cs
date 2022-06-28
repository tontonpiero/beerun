using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeeRun
{
    public class CoinsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text counterText;

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            counterText.text = UserManager.Instance.Coins.ToString();
        }
    }
}
