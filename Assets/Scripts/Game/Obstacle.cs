using System;
using UnityEngine;

namespace BeeRun
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleBehaviour behaviour;
        [SerializeField] private GameObject fxPrefab;
        [SerializeField] private bool destroyOnTrigger = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerController controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.HitObstacle(behaviour);

                if (fxPrefab != null)
                {
                    Instantiate(fxPrefab, transform.position, transform.rotation);
                }
                if (destroyOnTrigger) Destroy(gameObject);
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
