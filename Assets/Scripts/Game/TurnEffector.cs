using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class TurnEffector : MonoBehaviour
    {
        public float Angle = 0f;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.Turn(Angle);
                Destroy(gameObject);
            }
        }
    }
}
