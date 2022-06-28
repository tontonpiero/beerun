using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController controller;
        private Vector3 origin;

        private float horizontalFactor = 6f;
        private float verticalFactor = 8f;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (controller.IsDead) return;
            if (Input.GetMouseButtonDown(0))
            {
                origin = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 delta = (Input.mousePosition - origin) / Screen.width;
                controller.SetHorizontal(delta.x * horizontalFactor);
                controller.SetVertical(delta.y * verticalFactor);
            }
        }
    }
}
