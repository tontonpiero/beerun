using BeautifulTransitions.Scripts.Transitions.Components;
using TMPro;
using UnityEngine;

namespace BeeRun
{
    public class GameFlowerCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text textCounter;
        [SerializeField] private TransitionBase transition;
        [SerializeField] private TransitionBase loseTransition;

        private void Start()
        {
            GameManager.Instance.OnCollected += OnCollectedSomething;
            UpdateCounter();
        }

        private void OnCollectedSomething(CollectibleType type, int amount)
        {
            if (type == CollectibleType.Flower)
            {
                if (amount < 0f) loseTransition.TransitionIn();
                transition.TransitionIn();
                UpdateCounter();
            }
        }

        private void UpdateCounter()
        {
            textCounter.text = $"{GameManager.Instance.FlowerCount}";
        }
    }
}
