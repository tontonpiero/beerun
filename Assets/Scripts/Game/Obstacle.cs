using System;
using UnityEngine;

namespace BeeRun
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleBehaviour behaviour;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.HitObstacle(behaviour);
            }
        }
    }

    [Serializable]
    public enum ObstacleBehaviour
    {
        Damage,
        Kill,
        Web
    }
}
