using UnityEngine;

namespace BeeRun
{
    public class EndEffector : MonoBehaviour
    {
        [SerializeField] private Hive hive;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                GameManager.Instance.ReachHive(hive);
                Destroy(gameObject);
            }
        }
    }
}
