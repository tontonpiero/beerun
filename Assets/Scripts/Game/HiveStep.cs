using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeeRun
{
    public class HiveStep : MonoBehaviour
    {
        [Range(0, 10)]
        [SerializeField] private int step = 0;

        [SerializeField] private GameObject reachedObject;
        [SerializeField] private GameObject reachedMaxObject;

        /*private void OnValidate()
        {
            TMP_Text txt = FindObjectsOfType<TMP_Text>().FirstOrDefault(t => t.name.Contains("Mult"));
            float mult = GetMultiplicator(step);
            txt.text = mult.ToString().Replace('.', ',');

            reachedObject = FindObjectsOfType<Image>(true).FirstOrDefault(t => t.name.Contains("Reach")).gameObject;
        }*/

        public void Highlight(bool isMax = false)
        {
            reachedObject.SetActive(true);
            reachedMaxObject.SetActive(isMax);

            AudioManager.Instance.PlaySound("step_up");
            if (isMax)
            {
                AudioManager.Instance.PlaySound("step_max");
            }
        }
    }
}
