using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BeeRun
{
    public class TouchTrigger : MonoBehaviour
    {
        public UnityEvent OnTouched;

        private void OnMouseDown()
        {
            OnTouched?.Invoke();
        }
    }
}
