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
                switch (behaviour)
                {
                    case ObstacleBehaviour.Damage: controller.TakeDamage(); break;
                    case ObstacleBehaviour.Kill: controller.Kill(); break;
                }
            }
        }
    }

    [Serializable]
    public enum ObstacleBehaviour
    {
        Damage,
        Kill
    }
}
