using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeeRun
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nextLevelText;

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            nextLevelText.text = $"Level {UserManager.Instance.NextLevel}";
        }
    }
}
