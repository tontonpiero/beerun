using UnityEngine;

namespace BeeRun
{
    public class Rotation : MonoBehaviour
    {

        [SerializeField]
        protected Vector3 values;

        public float Speed = 1f;

        void Update()
        {
            transform.Rotate(values * Time.deltaTime * Speed);
        }

        public void SetSpeed(float speed)
        {
            this.Speed = speed;
        }
    }
}