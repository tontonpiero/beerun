using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class TurnEffector : MonoBehaviour
    {
        public Transform Direction;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.Turn(Direction.position, Direction.rotation);
                Destroy(gameObject);
            }
        }
    }
}
